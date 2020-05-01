namespace TP4Unzip
{
  partial class AppMain
  {
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      if(disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppMain));
      this.cmd_FileNameTp4Browse = new System.Windows.Forms.Button();
      this.FileNameTp4Dialog = new System.Windows.Forms.OpenFileDialog();
      this.txt_FileNameTp4 = new System.Windows.Forms.TextBox();
      this.cmd_Extract = new System.Windows.Forms.Button();
      this.txt_Info = new System.Windows.Forms.TextBox();
      this.lab_Info = new System.Windows.Forms.Label();
      this.lab_FileNameTp4 = new System.Windows.Forms.Label();
      this.lab_OutputDirectory = new System.Windows.Forms.Label();
      this.txt_OutputDirectory = new System.Windows.Forms.TextBox();
      this.cmd_OutputDirectoryBrowse = new System.Windows.Forms.Button();
      this.FolderDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.grp_Connection = new System.Windows.Forms.GroupBox();
      this.num_ConnPort = new System.Windows.Forms.NumericUpDown();
      this.lab_ConnPort = new System.Windows.Forms.Label();
      this.lab_ConnHost = new System.Windows.Forms.Label();
      this.txt_ConnHost = new System.Windows.Forms.TextBox();
      this.grp_Javsscript = new System.Windows.Forms.GroupBox();
      this.txt_JsVariableName = new System.Windows.Forms.TextBox();
      this.lab_JsVariableName = new System.Windows.Forms.Label();
      this.lab_JsFileName = new System.Windows.Forms.Label();
      this.txt_JsFileName = new System.Windows.Forms.TextBox();
      this.grp_Development = new System.Windows.Forms.GroupBox();
      this.ckb_CreateXml = new System.Windows.Forms.CheckBox();
      this.ckb_CreateJson = new System.Windows.Forms.CheckBox();
      this.ckb_CreateSegmentInfo = new System.Windows.Forms.CheckBox();
      this.grp_Connection.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.num_ConnPort)).BeginInit();
      this.grp_Javsscript.SuspendLayout();
      this.grp_Development.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmd_FileNameTp4Browse
      // 
      this.cmd_FileNameTp4Browse.Location = new System.Drawing.Point(995, 47);
      this.cmd_FileNameTp4Browse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.cmd_FileNameTp4Browse.Name = "cmd_FileNameTp4Browse";
      this.cmd_FileNameTp4Browse.Size = new System.Drawing.Size(160, 34);
      this.cmd_FileNameTp4Browse.TabIndex = 2;
      this.cmd_FileNameTp4Browse.Text = "Durchsuchen ...";
      this.cmd_FileNameTp4Browse.UseVisualStyleBackColor = true;
      // 
      // FileNameTp4Dialog
      // 
      this.FileNameTp4Dialog.Filter = "TP4-Dateien (*.tp4)|*.tp4|Alle Dateien (*.*)|*.*";
      // 
      // txt_FileNameTp4
      // 
      this.txt_FileNameTp4.Location = new System.Drawing.Point(21, 50);
      this.txt_FileNameTp4.Name = "txt_FileNameTp4";
      this.txt_FileNameTp4.Size = new System.Drawing.Size(958, 26);
      this.txt_FileNameTp4.TabIndex = 1;
      // 
      // cmd_Extract
      // 
      this.cmd_Extract.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmd_Extract.Location = new System.Drawing.Point(21, 175);
      this.cmd_Extract.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.cmd_Extract.Name = "cmd_Extract";
      this.cmd_Extract.Size = new System.Drawing.Size(301, 99);
      this.cmd_Extract.TabIndex = 6;
      this.cmd_Extract.Text = "Extrahieren";
      this.cmd_Extract.UseVisualStyleBackColor = true;
      // 
      // txt_Info
      // 
      this.txt_Info.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_Info.Location = new System.Drawing.Point(21, 500);
      this.txt_Info.Multiline = true;
      this.txt_Info.Name = "txt_Info";
      this.txt_Info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txt_Info.Size = new System.Drawing.Size(1134, 363);
      this.txt_Info.TabIndex = 11;
      // 
      // lab_Info
      // 
      this.lab_Info.AutoSize = true;
      this.lab_Info.Location = new System.Drawing.Point(17, 463);
      this.lab_Info.Name = "lab_Info";
      this.lab_Info.Size = new System.Drawing.Size(170, 20);
      this.lab_Info.TabIndex = 10;
      this.lab_Info.Text = "Ausgabeinformationen";
      // 
      // lab_FileNameTp4
      // 
      this.lab_FileNameTp4.AutoSize = true;
      this.lab_FileNameTp4.Location = new System.Drawing.Point(17, 23);
      this.lab_FileNameTp4.Name = "lab_FileNameTp4";
      this.lab_FileNameTp4.Size = new System.Drawing.Size(80, 20);
      this.lab_FileNameTp4.TabIndex = 0;
      this.lab_FileNameTp4.Text = "TP4-Datei";
      // 
      // lab_OutputDirectory
      // 
      this.lab_OutputDirectory.AutoSize = true;
      this.lab_OutputDirectory.Location = new System.Drawing.Point(17, 98);
      this.lab_OutputDirectory.Name = "lab_OutputDirectory";
      this.lab_OutputDirectory.Size = new System.Drawing.Size(406, 20);
      this.lab_OutputDirectory.TabIndex = 3;
      this.lab_OutputDirectory.Text = "Ausgabeverzeichniss ($ = Dateiname ohne Erweiterung)";
      // 
      // txt_OutputDirectory
      // 
      this.txt_OutputDirectory.Location = new System.Drawing.Point(21, 125);
      this.txt_OutputDirectory.Name = "txt_OutputDirectory";
      this.txt_OutputDirectory.Size = new System.Drawing.Size(958, 26);
      this.txt_OutputDirectory.TabIndex = 4;
      this.txt_OutputDirectory.Text = "$";
      // 
      // cmd_OutputDirectoryBrowse
      // 
      this.cmd_OutputDirectoryBrowse.Location = new System.Drawing.Point(995, 122);
      this.cmd_OutputDirectoryBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.cmd_OutputDirectoryBrowse.Name = "cmd_OutputDirectoryBrowse";
      this.cmd_OutputDirectoryBrowse.Size = new System.Drawing.Size(160, 34);
      this.cmd_OutputDirectoryBrowse.TabIndex = 5;
      this.cmd_OutputDirectoryBrowse.Text = "Durchsuchen ...";
      this.cmd_OutputDirectoryBrowse.UseVisualStyleBackColor = true;
      // 
      // grp_Connection
      // 
      this.grp_Connection.Controls.Add(this.num_ConnPort);
      this.grp_Connection.Controls.Add(this.lab_ConnPort);
      this.grp_Connection.Controls.Add(this.lab_ConnHost);
      this.grp_Connection.Controls.Add(this.txt_ConnHost);
      this.grp_Connection.Location = new System.Drawing.Point(343, 167);
      this.grp_Connection.Name = "grp_Connection";
      this.grp_Connection.Size = new System.Drawing.Size(812, 107);
      this.grp_Connection.TabIndex = 7;
      this.grp_Connection.TabStop = false;
      this.grp_Connection.Text = "Verbindung";
      // 
      // num_ConnPort
      // 
      this.num_ConnPort.Location = new System.Drawing.Point(75, 69);
      this.num_ConnPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
      this.num_ConnPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.num_ConnPort.Name = "num_ConnPort";
      this.num_ConnPort.Size = new System.Drawing.Size(77, 26);
      this.num_ConnPort.TabIndex = 3;
      this.num_ConnPort.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
      // 
      // lab_ConnPort
      // 
      this.lab_ConnPort.AutoSize = true;
      this.lab_ConnPort.Location = new System.Drawing.Point(15, 71);
      this.lab_ConnPort.Name = "lab_ConnPort";
      this.lab_ConnPort.Size = new System.Drawing.Size(38, 20);
      this.lab_ConnPort.TabIndex = 2;
      this.lab_ConnPort.Text = "Port";
      // 
      // lab_ConnHost
      // 
      this.lab_ConnHost.AutoSize = true;
      this.lab_ConnHost.Location = new System.Drawing.Point(15, 35);
      this.lab_ConnHost.Name = "lab_ConnHost";
      this.lab_ConnHost.Size = new System.Drawing.Size(43, 20);
      this.lab_ConnHost.TabIndex = 0;
      this.lab_ConnHost.Text = "Host";
      // 
      // txt_ConnHost
      // 
      this.txt_ConnHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_ConnHost.Location = new System.Drawing.Point(75, 28);
      this.txt_ConnHost.Name = "txt_ConnHost";
      this.txt_ConnHost.Size = new System.Drawing.Size(723, 30);
      this.txt_ConnHost.TabIndex = 1;
      this.txt_ConnHost.Text = "127.0.0.1";
      // 
      // grp_Javsscript
      // 
      this.grp_Javsscript.Controls.Add(this.txt_JsVariableName);
      this.grp_Javsscript.Controls.Add(this.lab_JsVariableName);
      this.grp_Javsscript.Controls.Add(this.lab_JsFileName);
      this.grp_Javsscript.Controls.Add(this.txt_JsFileName);
      this.grp_Javsscript.Location = new System.Drawing.Point(343, 293);
      this.grp_Javsscript.Name = "grp_Javsscript";
      this.grp_Javsscript.Size = new System.Drawing.Size(812, 114);
      this.grp_Javsscript.TabIndex = 9;
      this.grp_Javsscript.TabStop = false;
      this.grp_Javsscript.Text = "JSON / Javascript: Projekt";
      // 
      // txt_JsVariableName
      // 
      this.txt_JsVariableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_JsVariableName.Location = new System.Drawing.Point(148, 68);
      this.txt_JsVariableName.Name = "txt_JsVariableName";
      this.txt_JsVariableName.Size = new System.Drawing.Size(650, 30);
      this.txt_JsVariableName.TabIndex = 3;
      this.txt_JsVariableName.Text = "Proj";
      // 
      // lab_JsVariableName
      // 
      this.lab_JsVariableName.AutoSize = true;
      this.lab_JsVariableName.Location = new System.Drawing.Point(15, 75);
      this.lab_JsVariableName.Name = "lab_JsVariableName";
      this.lab_JsVariableName.Size = new System.Drawing.Size(116, 20);
      this.lab_JsVariableName.TabIndex = 2;
      this.lab_JsVariableName.Text = "Variablenname";
      // 
      // lab_JsFileName
      // 
      this.lab_JsFileName.AutoSize = true;
      this.lab_JsFileName.Location = new System.Drawing.Point(15, 35);
      this.lab_JsFileName.Name = "lab_JsFileName";
      this.lab_JsFileName.Size = new System.Drawing.Size(87, 20);
      this.lab_JsFileName.TabIndex = 0;
      this.lab_JsFileName.Text = "Dateiname";
      // 
      // txt_JsFileName
      // 
      this.txt_JsFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_JsFileName.Location = new System.Drawing.Point(148, 28);
      this.txt_JsFileName.Name = "txt_JsFileName";
      this.txt_JsFileName.Size = new System.Drawing.Size(650, 30);
      this.txt_JsFileName.TabIndex = 1;
      this.txt_JsFileName.Text = "project.js";
      // 
      // grp_Development
      // 
      this.grp_Development.Controls.Add(this.ckb_CreateSegmentInfo);
      this.grp_Development.Controls.Add(this.ckb_CreateXml);
      this.grp_Development.Controls.Add(this.ckb_CreateJson);
      this.grp_Development.Location = new System.Drawing.Point(21, 293);
      this.grp_Development.Name = "grp_Development";
      this.grp_Development.Size = new System.Drawing.Size(301, 157);
      this.grp_Development.TabIndex = 8;
      this.grp_Development.TabStop = false;
      this.grp_Development.Text = "Entwicklung";
      // 
      // ckb_CreateXml
      // 
      this.ckb_CreateXml.AutoSize = true;
      this.ckb_CreateXml.Checked = true;
      this.ckb_CreateXml.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckb_CreateXml.Location = new System.Drawing.Point(18, 74);
      this.ckb_CreateXml.Name = "ckb_CreateXml";
      this.ckb_CreateXml.Size = new System.Drawing.Size(209, 24);
      this.ckb_CreateXml.TabIndex = 1;
      this.ckb_CreateXml.Text = "XML-Dateien generieren";
      this.ckb_CreateXml.UseVisualStyleBackColor = true;
      // 
      // ckb_CreateJson
      // 
      this.ckb_CreateJson.AutoSize = true;
      this.ckb_CreateJson.Checked = true;
      this.ckb_CreateJson.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckb_CreateJson.Location = new System.Drawing.Point(18, 34);
      this.ckb_CreateJson.Name = "ckb_CreateJson";
      this.ckb_CreateJson.Size = new System.Drawing.Size(218, 24);
      this.ckb_CreateJson.TabIndex = 0;
      this.ckb_CreateJson.Text = "JSON-Dateien generieren";
      this.ckb_CreateJson.UseVisualStyleBackColor = true;
      // 
      // ckb_CreateSegmentInfo
      // 
      this.ckb_CreateSegmentInfo.AutoSize = true;
      this.ckb_CreateSegmentInfo.Checked = true;
      this.ckb_CreateSegmentInfo.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckb_CreateSegmentInfo.Location = new System.Drawing.Point(18, 114);
      this.ckb_CreateSegmentInfo.Name = "ckb_CreateSegmentInfo";
      this.ckb_CreateSegmentInfo.Size = new System.Drawing.Size(221, 24);
      this.ckb_CreateSegmentInfo.TabIndex = 2;
      this.ckb_CreateSegmentInfo.Text = "Segement Info generieren";
      this.ckb_CreateSegmentInfo.UseVisualStyleBackColor = true;
      // 
      // AppMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1178, 890);
      this.Controls.Add(this.grp_Development);
      this.Controls.Add(this.grp_Javsscript);
      this.Controls.Add(this.grp_Connection);
      this.Controls.Add(this.lab_OutputDirectory);
      this.Controls.Add(this.txt_OutputDirectory);
      this.Controls.Add(this.cmd_OutputDirectoryBrowse);
      this.Controls.Add(this.lab_FileNameTp4);
      this.Controls.Add(this.lab_Info);
      this.Controls.Add(this.txt_Info);
      this.Controls.Add(this.cmd_Extract);
      this.Controls.Add(this.txt_FileNameTp4);
      this.Controls.Add(this.cmd_FileNameTp4Browse);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.MaximizeBox = false;
      this.Name = "AppMain";
      this.Text = "TP4Unzip";
      this.grp_Connection.ResumeLayout(false);
      this.grp_Connection.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.num_ConnPort)).EndInit();
      this.grp_Javsscript.ResumeLayout(false);
      this.grp_Javsscript.PerformLayout();
      this.grp_Development.ResumeLayout(false);
      this.grp_Development.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmd_FileNameTp4Browse;
    private System.Windows.Forms.OpenFileDialog FileNameTp4Dialog;
    private System.Windows.Forms.TextBox txt_FileNameTp4;
    private System.Windows.Forms.Button cmd_Extract;
    private System.Windows.Forms.TextBox txt_Info;
    private System.Windows.Forms.Label lab_Info;
    private System.Windows.Forms.Label lab_FileNameTp4;
    private System.Windows.Forms.Label lab_OutputDirectory;
    private System.Windows.Forms.TextBox txt_OutputDirectory;
    private System.Windows.Forms.Button cmd_OutputDirectoryBrowse;
    private System.Windows.Forms.FolderBrowserDialog FolderDialog;
    private System.Windows.Forms.GroupBox grp_Connection;
    private System.Windows.Forms.NumericUpDown num_ConnPort;
    private System.Windows.Forms.Label lab_ConnPort;
    private System.Windows.Forms.Label lab_ConnHost;
    private System.Windows.Forms.TextBox txt_ConnHost;
    private System.Windows.Forms.GroupBox grp_Javsscript;
    private System.Windows.Forms.TextBox txt_JsVariableName;
    private System.Windows.Forms.Label lab_JsVariableName;
    private System.Windows.Forms.Label lab_JsFileName;
    private System.Windows.Forms.TextBox txt_JsFileName;
    private System.Windows.Forms.GroupBox grp_Development;
    private System.Windows.Forms.CheckBox ckb_CreateXml;
    private System.Windows.Forms.CheckBox ckb_CreateJson;
    private System.Windows.Forms.CheckBox ckb_CreateSegmentInfo;
  }
}

