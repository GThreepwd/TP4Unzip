using System;
using System.ComponentModel;
using System.IO;
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
      txt_OutputDirectory.Text = Settings.Default.OutputFolder;
      txt_ConnHost.Text = Settings.Default.ConnHost;

      try
      {
        num_ConnPort.Value = Settings.Default.ConnPort; // Default
      }
      catch
      {
        // Value out of range ..
        num_ConnPort.Value = 80; // Default
      }

      txt_JsFileName.Text = Settings.Default.JsFileName;
      txt_JsVariableName.Text = Settings.Default.JsVariableName;

      ckb_CreateJson.Checked = Settings.Default.CreateJson;
      ckb_CreateXml.Checked = Settings.Default.CreateXml;
      ckb_CreateSegmentInfo.Checked = Settings.Default.CreateSegmentInfo;

      cmd_FileNameTp4Browse.Click += OnFileNameTp4BrowseClick;
      cmd_OutputDirectoryBrowse.Click += OnOutputFolderBrowseClick;
      cmd_Extract.Click += OnExtractClick;
      FormClosing += OnClosing;
    }

    private void OnClosing(object sender, FormClosingEventArgs e)
    {
      SaveSettings();
    }

    private void SaveSettings()
    {
      Settings.Default.FileNameTp4 = txt_FileNameTp4.Text.Trim();

      Settings.Default.OutputFolder = txt_OutputDirectory.Text.Trim();

      Settings.Default.ConnHost = txt_ConnHost.Text.Trim();
      Settings.Default.ConnPort = (int)num_ConnPort.Value;

      Settings.Default.JsFileName = txt_JsFileName.Text.Trim();
      Settings.Default.JsVariableName = txt_JsVariableName.Text.Trim();

      Settings.Default.CreateJson = ckb_CreateJson.Checked;
      Settings.Default.CreateXml = ckb_CreateXml.Checked;
      Settings.Default.CreateSegmentInfo = ckb_CreateSegmentInfo.Checked;

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

    private void OnExtractClick(object sender, EventArgs e)
    {
      SaveSettings();

      txt_Info.Text = "";

      cmd_Extract.Enabled = false;

      try
      {
        var lFileNameTp4 = txt_FileNameTp4.Text.Trim();
        var lOutputDirectory = txt_OutputDirectory.Text.Trim();

        // Folder => Filename without extension
        if(lOutputDirectory == "$")
          lOutputDirectory = "";

        var lTp4File = new TP4File(lFileNameTp4, lOutputDirectory);

        lTp4File.Extract();

        // Javascrip: config.js
        var lConfig = string.Format(Resources.Config, txt_ConnHost.Text, num_ConnPort.Value.ToString());

        if(string.IsNullOrWhiteSpace(lOutputDirectory))
          lOutputDirectory = Path.GetDirectoryName(lFileNameTp4) + @"\" + Path.GetFileNameWithoutExtension(lFileNameTp4);

        var lFileNameConfig = string.Format(@"{0}\config.js", lOutputDirectory);

        File.WriteAllText(lFileNameConfig, lConfig);
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
