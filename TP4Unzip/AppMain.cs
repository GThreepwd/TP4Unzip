using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

using TP4Unzip.Properties;

namespace TP4Unzip
{
  public partial class AppMain : Form
  {
    public AppMain()
    {
      InitializeComponent();

      EventService.Instance.OnMessage += OnMessage;

      txt_FileNameTp4.Text = Settings.Default.FileNameTp4;

      if(string.IsNullOrWhiteSpace(Settings.Default.OutputFolder))
        txt_OutputDirectory.Text = "$";
      else
        txt_OutputDirectory.Text = Settings.Default.OutputFolder;

      txt_FileNameXsltHtml.Text = Settings.Default.FileNameXsltHtml;

      txt_XmlStyleSheetElement.Text = Settings.Default.XmlStyleSheetElement;

      cmd_FileNameTp4Browse.Click += OnFileNameTp4BrowseClick;
      cmd_OutputDirectoryBrowse.Click += OnOutputFolderBrowseClick;
      cmd_FileNameXsltHtmlBrowse.Click += OnFileNameXsltHtmlBrowseClick;
      cmd_Extract.Click += OnExtractClick;
      FormClosing += OnClosing;

      ckb_CreateJson.CheckedChanged += delegate { ckb_CreateJsonJs.Enabled = ckb_CreateJson.Checked; };
    }

    private void OnClosing(object sender, FormClosingEventArgs e)
    {
      Settings.Default.FileNameTp4 = txt_FileNameTp4.Text.Trim();

      Settings.Default.OutputFolder = txt_OutputDirectory.Text.Trim();

      Settings.Default.FileNameXsltHtml = txt_FileNameXsltHtml.Text.Trim();

      Settings.Default.XmlStyleSheetElement = txt_XmlStyleSheetElement.Text.Trim();

      Settings.Default.Save();
    }

    private void SetLogText(string text)
    {
      if(txt_Info.InvokeRequired)
        txt_Info.Invoke(new Action<string>(SetLogText), text);
      else
      {
        if(txt_Info.Lines.Length > 1000)
          txt_Info.Lines = txt_Info.Lines.Skip(txt_Info.Lines.Length - 1000).ToArray();

        txt_Info.AppendText(text + "\n");
        txt_Info.ScrollToCaret();
      }
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
    }

    private void OnFileNameTp4BrowseClick(object sender, EventArgs e)
    {
      var lResult = FileNameTp4Dialog.ShowDialog();

      if(lResult == DialogResult.OK)
      {
        txt_FileNameTp4.Text = FileNameTp4Dialog.FileName;

        Settings.Default.FileNameTp4 = FileNameTp4Dialog.FileName;

        Settings.Default.Save();
      }
    }

    private void OnOutputFolderBrowseClick(object sender, EventArgs e)
    {
      var lResult = FolderDialog.ShowDialog();

      if(lResult == DialogResult.OK)
      {
        txt_OutputDirectory.Text = FolderDialog.SelectedPath;

        Settings.Default.OutputFolder = FolderDialog.SelectedPath;

        Settings.Default.Save();
      }
    }

    private void OnFileNameXsltHtmlBrowseClick(object sender, EventArgs e)
    {
      var lResult = FileNameXsltDialog.ShowDialog();

      if(lResult == DialogResult.OK)
      {
        txt_FileNameXsltHtml.Text = FileNameXsltDialog.FileName;

        Settings.Default.FileNameXsltHtml = FileNameXsltDialog.FileName;

        Settings.Default.Save();
      }
    }

    private void OnExtractClick(object sender, EventArgs e)
    {
      txt_Info.Text = "";

      cmd_Extract.Enabled = false;

      try
      {
        var lOutputDirectory = txt_OutputDirectory.Text.Trim();

        // Folder => Filename without extension
        if(lOutputDirectory == "$")
          lOutputDirectory = "";

        var lTp4File = new TP4File(txt_FileNameTp4.Text.Trim(), lOutputDirectory)
        {
          XmlStyleSheetHtmlFileName = txt_FileNameXsltHtml.Text.Trim(),
          CreateJson = ckb_CreateJson.Checked,
          CreateJsonJs = ckb_CreateJsonJs.Checked
        };

        if(ckb_XSL.Checked)
          lTp4File.XmlStyleSheetElement = txt_XmlStyleSheetElement.Text.Trim();

        lTp4File.Extract();
      }
      catch(Exception ex)
      {
        MessageBox.Show(this, ex.Message, "TP4Unzip", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      cmd_Extract.Enabled = true;
    }

    private void OnMessage(object sender, MessageEventArgs e)
    {
      SetLogText(e.Text);
    }
  }
}
