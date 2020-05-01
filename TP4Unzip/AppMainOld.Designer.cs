namespace TP4Unzip
{
  partial class AppMainOld
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
      this.grp_Xml = new System.Windows.Forms.GroupBox();
      this.txt_XmlStyleSheetElement = new System.Windows.Forms.TextBox();
      this.ckb_XSL = new System.Windows.Forms.CheckBox();
      this.lab_OutputDirectory = new System.Windows.Forms.Label();
      this.txt_OutputDirectory = new System.Windows.Forms.TextBox();
      this.cmd_OutputDirectoryBrowse = new System.Windows.Forms.Button();
      this.FolderDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.lab_FileNameXsltHtml = new System.Windows.Forms.Label();
      this.txt_FileNameXsltHtml = new System.Windows.Forms.TextBox();
      this.cmd_FileNameXsltHtmlBrowse = new System.Windows.Forms.Button();
      this.FileNameXsltDialog = new System.Windows.Forms.OpenFileDialog();
      this.grp_Json = new System.Windows.Forms.GroupBox();
      this.ckb_CreateJsonJs = new System.Windows.Forms.CheckBox();
      this.ckb_CreateJson = new System.Windows.Forms.CheckBox();
      this.grp_Xml.SuspendLayout();
      this.grp_Json.SuspendLayout();
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
      this.cmd_Extract.Location = new System.Drawing.Point(21, 255);
      this.cmd_Extract.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.cmd_Extract.Name = "cmd_Extract";
      this.cmd_Extract.Size = new System.Drawing.Size(301, 60);
      this.cmd_Extract.TabIndex = 9;
      this.cmd_Extract.Text = "Extrahieren";
      this.cmd_Extract.UseVisualStyleBackColor = true;
      // 
      // txt_Info
      // 
      this.txt_Info.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_Info.Location = new System.Drawing.Point(21, 385);
      this.txt_Info.Multiline = true;
      this.txt_Info.Name = "txt_Info";
      this.txt_Info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txt_Info.Size = new System.Drawing.Size(1134, 478);
      this.txt_Info.TabIndex = 13;
      // 
      // lab_Info
      // 
      this.lab_Info.AutoSize = true;
      this.lab_Info.Location = new System.Drawing.Point(17, 346);
      this.lab_Info.Name = "lab_Info";
      this.lab_Info.Size = new System.Drawing.Size(170, 20);
      this.lab_Info.TabIndex = 12;
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
      // grp_Xml
      // 
      this.grp_Xml.Controls.Add(this.txt_XmlStyleSheetElement);
      this.grp_Xml.Controls.Add(this.ckb_XSL);
      this.grp_Xml.Location = new System.Drawing.Point(750, 255);
      this.grp_Xml.Name = "grp_Xml";
      this.grp_Xml.Size = new System.Drawing.Size(405, 111);
      this.grp_Xml.TabIndex = 11;
      this.grp_Xml.TabStop = false;
      this.grp_Xml.Text = "XML";
      // 
      // txt_XmlStyleSheetElement
      // 
      this.txt_XmlStyleSheetElement.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_XmlStyleSheetElement.Location = new System.Drawing.Point(14, 61);
      this.txt_XmlStyleSheetElement.Name = "txt_XmlStyleSheetElement";
      this.txt_XmlStyleSheetElement.Size = new System.Drawing.Size(377, 30);
      this.txt_XmlStyleSheetElement.TabIndex = 2;
      this.txt_XmlStyleSheetElement.Text = "./DefaultStyle.xslt";
      // 
      // ckb_XSL
      // 
      this.ckb_XSL.AutoSize = true;
      this.ckb_XSL.Checked = true;
      this.ckb_XSL.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckb_XSL.Location = new System.Drawing.Point(14, 31);
      this.ckb_XSL.Name = "ckb_XSL";
      this.ckb_XSL.Size = new System.Drawing.Size(228, 24);
      this.ckb_XSL.TabIndex = 1;
      this.ckb_XSL.Text = "XSL Stylesheet hinzufügen";
      this.ckb_XSL.UseVisualStyleBackColor = true;
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
      this.txt_OutputDirectory.TabIndex = 0;
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
      // lab_FileNameXsltHtml
      // 
      this.lab_FileNameXsltHtml.AutoSize = true;
      this.lab_FileNameXsltHtml.Location = new System.Drawing.Point(17, 172);
      this.lab_FileNameXsltHtml.Name = "lab_FileNameXsltHtml";
      this.lab_FileNameXsltHtml.Size = new System.Drawing.Size(237, 20);
      this.lab_FileNameXsltHtml.TabIndex = 6;
      this.lab_FileNameXsltHtml.Text = "HTML-Transformation (Optional)";
      // 
      // txt_FileNameXsltHtml
      // 
      this.txt_FileNameXsltHtml.Location = new System.Drawing.Point(21, 199);
      this.txt_FileNameXsltHtml.Name = "txt_FileNameXsltHtml";
      this.txt_FileNameXsltHtml.Size = new System.Drawing.Size(958, 26);
      this.txt_FileNameXsltHtml.TabIndex = 7;
      // 
      // cmd_FileNameXsltHtmlBrowse
      // 
      this.cmd_FileNameXsltHtmlBrowse.Location = new System.Drawing.Point(995, 196);
      this.cmd_FileNameXsltHtmlBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.cmd_FileNameXsltHtmlBrowse.Name = "cmd_FileNameXsltHtmlBrowse";
      this.cmd_FileNameXsltHtmlBrowse.Size = new System.Drawing.Size(160, 34);
      this.cmd_FileNameXsltHtmlBrowse.TabIndex = 8;
      this.cmd_FileNameXsltHtmlBrowse.Text = "Durchsuchen ...";
      this.cmd_FileNameXsltHtmlBrowse.UseVisualStyleBackColor = true;
      // 
      // FileNameXsltDialog
      // 
      this.FileNameXsltDialog.Filter = "XSLT-Dateien (*.xsl;*.xslt)|*.xsl;*.xslt|Alle Dateien (*.*)|*.*";
      // 
      // grp_Json
      // 
      this.grp_Json.Controls.Add(this.ckb_CreateJsonJs);
      this.grp_Json.Controls.Add(this.ckb_CreateJson);
      this.grp_Json.Location = new System.Drawing.Point(337, 255);
      this.grp_Json.Name = "grp_Json";
      this.grp_Json.Size = new System.Drawing.Size(407, 111);
      this.grp_Json.TabIndex = 10;
      this.grp_Json.TabStop = false;
      this.grp_Json.Text = "JSON";
      // 
      // ckb_CreateJsonJs
      // 
      this.ckb_CreateJsonJs.AutoSize = true;
      this.ckb_CreateJsonJs.Checked = true;
      this.ckb_CreateJsonJs.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckb_CreateJsonJs.Location = new System.Drawing.Point(18, 66);
      this.ckb_CreateJsonJs.Name = "ckb_CreateJsonJs";
      this.ckb_CreateJsonJs.Size = new System.Drawing.Size(365, 24);
      this.ckb_CreateJsonJs.TabIndex = 1;
      this.ckb_CreateJsonJs.Text = "JSON-Script Variable generieren (./js/project.js)";
      this.ckb_CreateJsonJs.UseVisualStyleBackColor = true;
      // 
      // ckb_CreateJson
      // 
      this.ckb_CreateJson.AutoSize = true;
      this.ckb_CreateJson.Checked = true;
      this.ckb_CreateJson.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckb_CreateJson.Location = new System.Drawing.Point(18, 36);
      this.ckb_CreateJson.Name = "ckb_CreateJson";
      this.ckb_CreateJson.Size = new System.Drawing.Size(331, 24);
      this.ckb_CreateJson.TabIndex = 0;
      this.ckb_CreateJson.Text = "JSON-Dateien generieren (inkl. ./Proj.json)";
      this.ckb_CreateJson.UseVisualStyleBackColor = true;
      // 
      // AppMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1178, 890);
      this.Controls.Add(this.grp_Json);
      this.Controls.Add(this.lab_FileNameXsltHtml);
      this.Controls.Add(this.txt_FileNameXsltHtml);
      this.Controls.Add(this.cmd_FileNameXsltHtmlBrowse);
      this.Controls.Add(this.lab_OutputDirectory);
      this.Controls.Add(this.txt_OutputDirectory);
      this.Controls.Add(this.cmd_OutputDirectoryBrowse);
      this.Controls.Add(this.grp_Xml);
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
      this.grp_Xml.ResumeLayout(false);
      this.grp_Xml.PerformLayout();
      this.grp_Json.ResumeLayout(false);
      this.grp_Json.PerformLayout();
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
    private System.Windows.Forms.GroupBox grp_Xml;
    private System.Windows.Forms.Label lab_OutputDirectory;
    private System.Windows.Forms.TextBox txt_OutputDirectory;
    private System.Windows.Forms.Button cmd_OutputDirectoryBrowse;
    private System.Windows.Forms.FolderBrowserDialog FolderDialog;
    private System.Windows.Forms.TextBox txt_XmlStyleSheetElement;
    private System.Windows.Forms.CheckBox ckb_XSL;
    private System.Windows.Forms.Label lab_FileNameXsltHtml;
    private System.Windows.Forms.TextBox txt_FileNameXsltHtml;
    private System.Windows.Forms.Button cmd_FileNameXsltHtmlBrowse;
    private System.Windows.Forms.OpenFileDialog FileNameXsltDialog;
    private System.Windows.Forms.GroupBox grp_Json;
    private System.Windows.Forms.CheckBox ckb_CreateJson;
    private System.Windows.Forms.CheckBox ckb_CreateJsonJs;
  }
}

