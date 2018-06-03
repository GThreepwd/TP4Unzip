using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using ICSharpCode.SharpZipLib.GZip;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TP4Unzip
{
  public class TP4File
  {
    private const int BlockSize = 512;

    private const string DirectoryXml = "xml";
    private const string DirectoryImages = "images";
    private const string DirectoryFonts = "fonts";
    private const string DirectoryAudio = "audio";
    private const string DirectoryOthers = "others";

    private const string XmlHeaderNet = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";

    private Dictionary<uint, DataSegment> mDataSegments;
    private Dictionary<uint, InfoSegment> mInfoSegments;

    private FileStream mStream;
    private List<byte> mData;

    private List<string> mJsonList;

    private int mTotalErrors;

    public TP4File(string fileName) :
      this(fileName, null)
    {
    }

    public TP4File(string fileName, string outputDirectory)
    {
      if(fileName == null)
        throw new ArgumentNullException(nameof(fileName));

      // Initial Check
      try
      {
        File.Open(fileName, FileMode.Open, FileAccess.Read).Dispose();
      }
      catch(Exception ex)
      {
        EventService.CreateMsg(this, ex.Message);

        throw;
      }

      FileName = fileName;

      OutputDirectory = outputDirectory;

      if(string.IsNullOrWhiteSpace(OutputDirectory))
        OutputDirectory = Path.GetDirectoryName(fileName) + @"\" + Path.GetFileNameWithoutExtension(fileName);
    }

    public string FileName { get; private set; }

    public string OutputDirectory { get; set; } = ".\\";

    public string XmlStyleSheetElement { get; set; }

    public string XmlStyleSheetHtmlFileName { get; set; }

    public bool CreateJson { get; set; } = true;

    public bool CreateJsonJs { get; set; } = true;

    public static string ByteArrayToString(byte[] bytes)
    {
      var lSb = new StringBuilder(bytes.Length * 2);

      foreach(byte b in bytes)
        lSb.AppendFormat("{0:x2}", b);

      return lSb.ToString();
    }

    public void Extract()
    {
      mJsonList = new List<string>();

      var mKeys = new List<InfoSegment>();

      mTotalErrors = 0;

      mDataSegments = new Dictionary<uint, DataSegment>();
      mInfoSegments = new Dictionary<uint, InfoSegment>();

      try
      {
        if(!string.IsNullOrWhiteSpace(OutputDirectory))
          Directory.CreateDirectory(OutputDirectory);

        mStream = File.OpenRead(FileName);

        var lBytes = new byte[14];

        // Das Erste Segment beginnt nach 12 Bytes
        mStream.Seek(12, SeekOrigin.Begin);

        // Ein Segment hat immer die Grösse von 14 Bytes
        while(mStream.Read(lBytes, 0, 14) == 14)
        {
          var lSegment = Segment.Create(mStream.Position - 14, lBytes);

          if(lSegment is InfoSegment lInfoSegment)
          {
            if(lSegment is InfoSegment)
              mInfoSegments.Add(lSegment.SegmentNo, lInfoSegment);

            var lBuffer = new byte[BlockSize];

            mStream.Read(lBuffer, 0, BlockSize);

            lInfoSegment.AddInfoBlock(lBuffer);
          }
          else
          {
            mDataSegments.Add(lSegment.SegmentNo, (DataSegment)lSegment);

            // Springe zum nächsten Segment
            // Die Grösse eines Blocks beträgt jeweils 512 Bytes
            // Die im Block enthaltenen Daten (Bytes) können auch kleiner sein
            mStream.Seek(BlockSize, SeekOrigin.Current);
          }
        }

        EventService.CreateMsg(this, "Gefundene Info-Segmente: {0}, Aktuelle Position={1}", mInfoSegments.Count, mStream.Position);

        DebugInfoSegments();

        ProcessJunkData();

        if(CreateJson)
        {
          try
          {
            var lFileNameJson = string.Format(@"{0}\proj.json", OutputDirectory).ToLower();

            var lJsonProject = new JObject();

            var lJsonPages = new JArray();
            var lJsonPalettes = new JArray();

            foreach(var json in mJsonList)
            {
              var lJsonObj = JObject.Parse(json).SelectToken("", false);

              // Omit root object
              if((lJsonObj.First as JProperty)?.Name == "root")
                lJsonObj = lJsonObj.SelectToken("root", false);

              var lProperty = lJsonObj.First as JProperty;

              switch(lProperty?.Name)
              {
                case "versionInfo":
                  {
                    lJsonProject["project"] = lJsonObj;

                    // Add pages after Project-Info
                    if(lProperty?.Name == "versionInfo")
                      lJsonProject.Add(new JProperty("pages"));

                    break;
                  }
                case "page":
                  {
                    lJsonPages.Add(lJsonObj);

                    break;
                  }
                case "cm":
                  {
                    lJsonProject.Add(new JProperty("map", lJsonObj));

                    break;
                  }
                case "paletteData":
                  {
                    lJsonPalettes.Add(lJsonObj);

                    break;
                  }
                default:
                  {
                    lJsonProject.Merge(lJsonObj);
                    break;
                  }
              }
            }

            // All pages (Array)
            lJsonProject["pages"] = lJsonPages;

            // Palette Data
            lJsonProject["palettes"] = lJsonPalettes;

            var lJSonProjStr = lJsonProject.ToString();

            File.WriteAllText(lFileNameJson, lJSonProjStr);

            if(CreateJsonJs)
            {
              lJSonProjStr = "var Proj = " + lJSonProjStr;

              CreateResourceDirectory("scripts");

              lFileNameJson = string.Format(@"{0}\scripts\proj.js", OutputDirectory).ToLower();

              File.WriteAllText(lFileNameJson, lJSonProjStr);
            }
          }
          catch(Exception ex)
          {
            mTotalErrors++;

            EventService.CreateMsg(this, "Error: [Extract-CreateJson] Message={0}", ex.Message);
          }
        }
      }
      catch(Exception ex)
      {
        mTotalErrors++;

        EventService.CreateMsg(this, "Error: [Extract] Message={0}", ex.Message);
      }
      finally
      {
        mStream?.Dispose();
      }

      EventService.CreateMsg(this, "Fertig, Anzahl Fehler: {0}", mTotalErrors);
    }

    private void DebugInfoSegments()
    {
      try
      {
        var lFileName = string.Format(@"{0}\{1}", OutputDirectory, "SegmentInfo.txt");

        File.Delete(lFileName);

        var lMaxLenJunks = mInfoSegments.Values.Max(s => s.JunkCount).ToString().Length;
        var lMaxLenSize = mInfoSegments.Values.Max(s => s.Size).ToString().Length;

        foreach(var s in mInfoSegments.Values)
        {
          var lStr = string.Format("Info: SegmentNo=0x{0:X08}, DataSegmentNo=0x{1:X08}, DataJunks={2}, DataSize={3}, Name={4}",
            s.SegmentNo, s.DataSegmentNo, s.JunkCount.ToString().PadLeft(lMaxLenJunks, '0'), s.Size.ToString().PadLeft(lMaxLenSize, '0'), s.Name);

          File.AppendAllText(lFileName, lStr + Environment.NewLine);
        }
      }
      catch(Exception ex)
      {
        mTotalErrors++;

        EventService.CreateMsg(this, "Error: [DebugInfoSegments] Message={0}", ex.Message);
      }
    }

    private void ProcessJunkData()
    {
      foreach(var lSegment in mInfoSegments.Values)
      {
        try
        {
          var lDataSegment = mDataSegments[lSegment.DataSegmentNo];

          mData = new List<byte>();

          while(lDataSegment.Length > 0 || lDataSegment.SegmentNextValue > 0)
          {
            var lBuffer = new byte[lDataSegment.Length];

            mStream.Position = lDataSegment.Offset + 14;

            mStream.Read(lBuffer, 0, lDataSegment.Length);

            mData.AddRange(lBuffer);

            // Keine Weitere Segmente
            if(lDataSegment.SegmentNextValue == 0)
              break;

            // Nächstes Segment suchen ... (Next, Previous)
            lDataSegment = mDataSegments[lDataSegment.SegmentNextValue];
          }

          ProcessRessource(lSegment.Name);
        }
        catch(Exception ex)
        {
          mTotalErrors++;

          EventService.CreateMsg(this, "Error: [ProcessJunkData] Message={0}", ex.Message);
        }
      }
    }

    private void ProcessRessource(string originalName)
    {
      try
      {
        var lExt = Path.GetExtension(originalName);

        // <?xml version=\"1.0\"?>
        var lStr = Encoding.Default.GetString(mData.Take(21).ToArray());

        // Empty table.xma => RawXml -> Ignore
        if(lStr == "<?xml version=\"1.0\"?>")
          return;

        var lDirectory = DirectoryOthers;
        var lNewFileName = originalName;

        if(lExt == ".xma")
          lNewFileName = "$" + originalName.Replace(".xma", ".xml");

        lNewFileName = lNewFileName.ToLower();

        switch(lExt)
        {
          case ".raw":
          case ".xma":
          case ".xml": lDirectory = DirectoryXml; break;
          case ".jpg":
          case ".png": lDirectory = DirectoryImages; break;
          case ".ttf": lDirectory = DirectoryFonts; break;
          case ".wav":
          case ".mp3": lDirectory = DirectoryAudio; break;
        }

        CreateResourceDirectory(lDirectory);

        EventService.CreateMsg(this, "Erstelle Datei: {0}", lNewFileName);

        if(lExt == ".xma" || lExt == ".xml")
          ProcessGZipData(lDirectory, lNewFileName, lNewFileName == "$fnt.xml");
        else
          ProcessRawData(lDirectory, lNewFileName);

        Application.DoEvents();
      }
      catch(Exception ex)
      {
        mTotalErrors++;

        EventService.CreateMsg(this, "Error: [ProcessRessource] Message=" + ex.Message);
      }
    }

    private void ProcessRawData(string directory, string fileName)
    {
      try
      {
        var lFileName = string.Format(@"{0}\{1}\{2}", OutputDirectory, directory, fileName);

        File.WriteAllBytes(lFileName, mData.ToArray());
      }
      catch(Exception ex)
      {
        mTotalErrors++;

        EventService.CreateMsg(this, "Error: [ProcessRawData] Message={0}", ex.Message);
      }

      Application.DoEvents();
    }

    private void ProcessGZipData(string directory, string fileName, bool fontList)
    {
      try
      {
        var lFileNameXml = string.Format(@"{0}\{1}\{2}", OutputDirectory, directory, fileName).ToLower();

        var lMemStream = new MemoryStream();

        try
        {
          GZip.Decompress(new MemoryStream(mData.ToArray()), lMemStream, false);
        }
        catch(Exception ex)
        {
          mTotalErrors++;

          EventService.CreateMsg(this, "Error: [ProcessGZipData-Decompress] Message={0}", ex.Message);

          try
          {
            // Try save memory to file
            lFileNameXml = string.Format(@"{0}\{1}\{2}.corrupted{3}", OutputDirectory, directory, Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName)).ToLower();

            File.WriteAllText(lFileNameXml, Encoding.Default.GetString(lMemStream.ToArray()));
          }
          catch { }

          return;
        }

        Application.DoEvents();

        var lXml = Encoding.Default.GetString(lMemStream.ToArray());

        var lXmlDoc = new XmlDocument();

        try
        {
          lXmlDoc.LoadXml(lXml);

          try
          {
            // Linking XSLT to a Source XML Document:
            // Embed an XSLT style sheet inside the source XML document
            // Example: <?xml-stylesheet type="text/xsl" href="style.xsl"?>
            // When you run the source XML file in Internet Explorer, the transformation is applied automatically
            if(!string.IsNullOrWhiteSpace(XmlStyleSheetElement))
            {
              var lData = string.Format("type=\"text/xsl\" href=\"{0}\"", XmlStyleSheetElement);

              var lStylesheetInstruction = lXmlDoc.CreateProcessingInstruction("xml-stylesheet", lData);

              lXmlDoc.InsertAfter(lStylesheetInstruction, lXmlDoc.FirstChild);
            }
          }
          catch(Exception ex)
          {
            mTotalErrors++;

            EventService.CreateMsg(this, "Error: [ProcessGZipData-XmlStyleSheetElement] Message={0}", ex.Message);
          }
          
          lXmlDoc.Save(lFileNameXml);
        }
        catch(Exception ex)
        {
          mTotalErrors++;

          EventService.CreateMsg(this, "Error: [ProcessGZipData-LoadXml] Message={0}", ex.Message);

          File.WriteAllText(lFileNameXml, lXml);
        }

        // Import System Fonts!
        // %CommonProgramFiles(x86)%\AMXShare\G4SupportFiles\__system\graphics\fnt.xma
        if(fontList)
        {
          var lXmlDocSysFonts = new XmlDocument();

          lXmlDocSysFonts.LoadXml(Properties.Resources.SysFonts);

          var lTargetNode = lXmlDoc.DocumentElement.SelectSingleNode("/root/fontList");

          if(lTargetNode.ChildNodes.Count == 0)
          {
            foreach(XmlNode node in lXmlDocSysFonts.DocumentElement.SelectNodes("/root/fontList/font"))
            {
              var lNode = lXmlDoc.ImportNode(node, true);

              lTargetNode.AppendChild(lNode);
            }
          }
          else
          {
            XmlNode lRefChild = null;

            foreach(XmlNode node in lXmlDocSysFonts.DocumentElement.SelectNodes("/root/fontList/font"))
            {
              var lNode = lXmlDoc.ImportNode(node, true);

              // Order by Font-Index
              if(lRefChild == null)
                lRefChild = lTargetNode.InsertBefore(lNode, lTargetNode.FirstChild);
              else
                lRefChild = lTargetNode.InsertAfter(lNode, lRefChild);
            }
          }

          CreateResourceDirectory("xml");

          var lFileNameSysFont = string.Format(@"{0}\xml\$SysFnt.xml", OutputDirectory, directory).ToLower();

          lXmlDocSysFonts.Save(lFileNameSysFont);
        }

        if(CreateJson)
        {
          try
          {
            // Remove XmlDeclaration
            foreach(XmlNode node in lXmlDoc)
            {
              if(node.NodeType == XmlNodeType.XmlDeclaration)
                lXmlDoc.RemoveChild(node);
            }

            // Remove Stylesheet Instruction
            var lNode = lXmlDoc.SelectSingleNode("/processing-instruction('xml-stylesheet')");

            if(lNode != null)
              lXmlDoc.RemoveChild(lNode);

            JsonArrrayHelper(lXmlDoc);

            var lJson = JsonConvert.SerializeXmlNode(lXmlDoc, Newtonsoft.Json.Formatting.Indented, true);

            // <page type="page">
            // JSON.NET and Replacing @ Sign in XML to JSON converstion
            lJson = (Regex.Replace(lJson, "(?<=\")(@)(?!.*\":\\s )", string.Empty, RegexOptions.IgnoreCase));

            mJsonList.Add(lJson);

            CreateResourceDirectory("json");

            var lFileNameJson = string.Format(@"{0}\{1}\{2}.json", OutputDirectory, "json", Path.GetFileNameWithoutExtension(lFileNameXml)).ToLower();

            File.WriteAllText(lFileNameJson, lJson);
          }
          catch(Exception ex)
          {
            mTotalErrors++;

            EventService.CreateMsg(this, "Error: [ProcessGZipData-CreateJson] Message={0}", ex.Message);
          }
        }

        // HTML-Transformation
        if(!string.IsNullOrWhiteSpace(XmlStyleSheetHtmlFileName))
        {
          CreateResourceDirectory("html");

          // Create the XslTransform object and load the style sheet.
          var lXslt = new XslCompiledTransform();

          lXslt.Load(XmlStyleSheetHtmlFileName);

          // Load the file to transform.
          var lXmlSource = new XPathDocument(lFileNameXml);

          var lFileNameHtml = string.Format(@"{0}\{1}\{2}.html", OutputDirectory, "html", Path.GetFileNameWithoutExtension(lFileNameXml)).ToLower();

          var lSettings = new XmlWriterSettings
          {
            Indent = true,
            NewLineHandling = NewLineHandling.None,
            ConformanceLevel = ConformanceLevel.Fragment
          };

          using(var lStrWriter = new StringWriter())
          {
            // Create the writer.
            using(var lXmlWriter = XmlWriter.Create(lStrWriter, lSettings))
            {
              // Transform the file and send the output to the console.
              lXslt.Transform(lXmlSource, lXmlWriter);

              var lHtml = lStrWriter.ToString();

              // Remove: <?xml version="1.0" encoding="utf-16"?>
              lHtml = lHtml.Replace(XmlHeaderNet, "");

              File.WriteAllText(lFileNameHtml, lHtml);
            }
          }
        }
      }
      catch(Exception ex)
      {
        mTotalErrors++;

        EventService.CreateMsg(this, "Error: [ProcessGZipData] Message={0}", ex.Message);
      }
    }

    private void CreateResourceDirectory(string directory)
    {
      try
      {
        var lPath = OutputDirectory + @"\" + directory;

        if(!Directory.Exists(lPath))
          Directory.CreateDirectory(lPath);
      }
      catch(Exception ex)
      {
        mTotalErrors++;

        EventService.CreateMsg(this, "Error: [CreateResourceDirectory] Directory={0}, Message={1}", directory, ex.Message);
      }
    }

    private void JsonArrrayHelper(XmlDocument xmlDoc)
    {
      var lNamespace = "http://james.newtonking.com/projects/json";

      /*
      From Json.NET documentation: http://james.newtonking.com/projects/json/help/?topic=html/ConvertingJSONandXML.htm
      You can force a node to be rendered as an Array by adding the attribute json:Array='true' to the XML node you are converting to JSON.

      Also, you need to declare the json prefix namespace at the XML header xmlns:json='http://james.newtonking.com/projects/json' or 
      else you will get an XML error stating that the json prefix is not declared.

      The next example is provided by the documentation:
      xml = @"<person xmlns:json='http://james.newtonking.com/projects/json' id='1'>
            <name>Alan</name>
            <url>http://www.google.com</url>
            <role json:Array='true'>Admin</role>
          </person>";

      */

      xmlDoc.DocumentElement.SetAttribute("xmlns:json", lNamespace);

      var lElements = xmlDoc.SelectNodes("/rootresourceList/resource|/root/page/button|/root/tableList/tableEntry|/root/tableList/tableEntry/row");
      
      foreach(XmlElement element in lElements)
      {
        var lAttr = xmlDoc.CreateAttribute("Array", lNamespace);

        lAttr.Value = "true";

        element.Attributes.Append(lAttr);
      }

    }
  }
}
