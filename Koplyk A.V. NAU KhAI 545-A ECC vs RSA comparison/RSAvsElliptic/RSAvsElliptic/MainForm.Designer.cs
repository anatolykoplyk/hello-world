using System.ComponentModel;
using System.Windows.Forms;

namespace RSAvsElliptic
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.VirifyECC = new System.Windows.Forms.Button();
            this.richTextBoxECCParams = new System.Windows.Forms.RichTextBox();
            this.SignECC = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.DecryptECC = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.EncryptECC = new System.Windows.Forms.Button();
            this.comboBoxECCKeySize = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ClearLog = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Verify = new System.Windows.Forms.Button();
            this.Sign = new System.Windows.Forms.Button();
            this.Decrypt = new System.Windows.Forms.Button();
            this.Encrypt = new System.Windows.Forms.Button();
            this.richTextBoxRSAparams = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.KeySizeRSA = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openFile = new System.Windows.Forms.Button();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KeySizeRSA)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(264, 19);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(227, 348);
            this.richTextBoxLog.TabIndex = 0;
            this.richTextBoxLog.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(778, 434);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.ClearLog);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.richTextBoxLog);
            this.tabPage1.Controls.Add(this.openFile);
            this.tabPage1.Controls.Add(this.textBoxFilePath);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(770, 408);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.VirifyECC);
            this.groupBox2.Controls.Add(this.richTextBoxECCParams);
            this.groupBox2.Controls.Add(this.SignECC);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.DecryptECC);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.EncryptECC);
            this.groupBox2.Controls.Add(this.comboBoxECCKeySize);
            this.groupBox2.Location = new System.Drawing.Point(497, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 365);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Elliptic curves";
            // 
            // VirifyECC
            // 
            this.VirifyECC.Location = new System.Drawing.Point(193, 188);
            this.VirifyECC.Name = "VirifyECC";
            this.VirifyECC.Size = new System.Drawing.Size(58, 38);
            this.VirifyECC.TabIndex = 10;
            this.VirifyECC.Text = "Verify DS";
            this.VirifyECC.UseVisualStyleBackColor = true;
            this.VirifyECC.Click += new System.EventHandler(this.VirifyECC_Click);
            // 
            // richTextBoxECCParams
            // 
            this.richTextBoxECCParams.Location = new System.Drawing.Point(6, 85);
            this.richTextBoxECCParams.Name = "richTextBoxECCParams";
            this.richTextBoxECCParams.Size = new System.Drawing.Size(176, 273);
            this.richTextBoxECCParams.TabIndex = 12;
            this.richTextBoxECCParams.Text = "";
            // 
            // SignECC
            // 
            this.SignECC.Location = new System.Drawing.Point(193, 154);
            this.SignECC.Name = "SignECC";
            this.SignECC.Size = new System.Drawing.Size(58, 28);
            this.SignECC.TabIndex = 8;
            this.SignECC.Text = "Sign";
            this.SignECC.UseVisualStyleBackColor = true;
            this.SignECC.Click += new System.EventHandler(this.SignECC_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Encryption parameters";
            // 
            // DecryptECC
            // 
            this.DecryptECC.Location = new System.Drawing.Point(193, 120);
            this.DecryptECC.Name = "DecryptECC";
            this.DecryptECC.Size = new System.Drawing.Size(58, 28);
            this.DecryptECC.TabIndex = 7;
            this.DecryptECC.Text = "Decrypt";
            this.DecryptECC.UseVisualStyleBackColor = true;
            this.DecryptECC.Click += new System.EventHandler(this.DecryptECC_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Key Size";
            // 
            // EncryptECC
            // 
            this.EncryptECC.Location = new System.Drawing.Point(193, 86);
            this.EncryptECC.Name = "EncryptECC";
            this.EncryptECC.Size = new System.Drawing.Size(58, 28);
            this.EncryptECC.TabIndex = 6;
            this.EncryptECC.Text = "Encrypt";
            this.EncryptECC.UseVisualStyleBackColor = true;
            this.EncryptECC.Click += new System.EventHandler(this.EncryptECC_Click);
            // 
            // comboBoxECCKeySize
            // 
            this.comboBoxECCKeySize.FormattingEnabled = true;
            this.comboBoxECCKeySize.Items.AddRange(new object[] {
            "256",
            "384",
            "521"});
            this.comboBoxECCKeySize.Location = new System.Drawing.Point(138, 16);
            this.comboBoxECCKeySize.Name = "comboBoxECCKeySize";
            this.comboBoxECCKeySize.Size = new System.Drawing.Size(121, 21);
            this.comboBoxECCKeySize.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(261, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Operation Log";
            // 
            // ClearLog
            // 
            this.ClearLog.Location = new System.Drawing.Point(376, 377);
            this.ClearLog.Name = "ClearLog";
            this.ClearLog.Size = new System.Drawing.Size(95, 25);
            this.ClearLog.TabIndex = 10;
            this.ClearLog.Text = "Clear Logs";
            this.ClearLog.UseVisualStyleBackColor = true;
            this.ClearLog.Click += new System.EventHandler(this.ClearLog_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Verify);
            this.groupBox1.Controls.Add(this.Sign);
            this.groupBox1.Controls.Add(this.Decrypt);
            this.groupBox1.Controls.Add(this.Encrypt);
            this.groupBox1.Controls.Add(this.richTextBoxRSAparams);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.KeySizeRSA);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 364);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RSA";
            // 
            // Verify
            // 
            this.Verify.Location = new System.Drawing.Point(188, 192);
            this.Verify.Name = "Verify";
            this.Verify.Size = new System.Drawing.Size(58, 38);
            this.Verify.TabIndex = 10;
            this.Verify.Text = "Verify DS";
            this.Verify.UseVisualStyleBackColor = true;
            this.Verify.Click += new System.EventHandler(this.Verify_Click);
            // 
            // Sign
            // 
            this.Sign.Location = new System.Drawing.Point(188, 158);
            this.Sign.Name = "Sign";
            this.Sign.Size = new System.Drawing.Size(58, 28);
            this.Sign.TabIndex = 8;
            this.Sign.Text = "Sign";
            this.Sign.UseVisualStyleBackColor = true;
            this.Sign.Click += new System.EventHandler(this.Sign_Click);
            // 
            // Decrypt
            // 
            this.Decrypt.Location = new System.Drawing.Point(188, 124);
            this.Decrypt.Name = "Decrypt";
            this.Decrypt.Size = new System.Drawing.Size(58, 28);
            this.Decrypt.TabIndex = 7;
            this.Decrypt.Text = "Decrypt";
            this.Decrypt.UseVisualStyleBackColor = true;
            this.Decrypt.Click += new System.EventHandler(this.Decrypt_Click);
            // 
            // Encrypt
            // 
            this.Encrypt.Location = new System.Drawing.Point(188, 90);
            this.Encrypt.Name = "Encrypt";
            this.Encrypt.Size = new System.Drawing.Size(58, 28);
            this.Encrypt.TabIndex = 6;
            this.Encrypt.Text = "Encrypt";
            this.Encrypt.UseVisualStyleBackColor = true;
            this.Encrypt.Click += new System.EventHandler(this.Encrypt_Click);
            // 
            // richTextBoxRSAparams
            // 
            this.richTextBoxRSAparams.Location = new System.Drawing.Point(6, 88);
            this.richTextBoxRSAparams.Name = "richTextBoxRSAparams";
            this.richTextBoxRSAparams.Size = new System.Drawing.Size(176, 273);
            this.richTextBoxRSAparams.TabIndex = 5;
            this.richTextBoxRSAparams.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "384";
            // 
            // KeySizeRSA
            // 
            this.KeySizeRSA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.KeySizeRSA.LargeChange = 128;
            this.KeySizeRSA.Location = new System.Drawing.Point(54, 12);
            this.KeySizeRSA.Maximum = 16384;
            this.KeySizeRSA.Minimum = 384;
            this.KeySizeRSA.Name = "KeySizeRSA";
            this.KeySizeRSA.Size = new System.Drawing.Size(193, 45);
            this.KeySizeRSA.SmallChange = 8;
            this.KeySizeRSA.TabIndex = 1;
            this.KeySizeRSA.TickFrequency = 8;
            this.KeySizeRSA.Value = 1024;
            this.KeySizeRSA.ValueChanged += new System.EventHandler(this.KeySizeRSA_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Encryption parameters";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Key Size";
            // 
            // openFile
            // 
            this.openFile.Location = new System.Drawing.Point(264, 377);
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(83, 28);
            this.openFile.TabIndex = 3;
            this.openFile.Text = "File to Encrypt";
            this.openFile.UseVisualStyleBackColor = true;
            this.openFile.Click += new System.EventHandler(this.openFile_Click);
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Location = new System.Drawing.Point(12, 382);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(246, 20);
            this.textBoxFilePath.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(770, 408);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 451);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "RSA versus Elliptic Curves Сryptography";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KeySizeRSA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private RichTextBox richTextBoxLog;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private GroupBox groupBox1;
        private TrackBar KeySizeRSA;
        private Label label1;
        private Label label2;
        private Button openFile;
        private OpenFileDialog openFileDialog1;
        private TextBox textBoxFilePath;
        private RichTextBox richTextBoxRSAparams;
        private Label label3;
        private Button Sign;
        private Button Decrypt;
        private Button Encrypt;
        private Button ClearLog;
        private Label label4;
        private Button Verify;
        private GroupBox groupBox2;
        private Label label5;
        private ComboBox comboBoxECCKeySize;
        private Button VirifyECC;
        private RichTextBox richTextBoxECCParams;
        private Button SignECC;
        private Label label6;
        private Button DecryptECC;
        private Button EncryptECC;
    }
}

