namespace NätverksKlient
{
    partial class Klient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tbxIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxport = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.btnTaEmot = new System.Windows.Forms.Button();
            this.tbxLogg = new System.Windows.Forms.TextBox();
            this.tbxMeddelande = new System.Windows.Forms.TextBox();
            this.btnSkicka = new System.Windows.Forms.Button();
            this.tbxNamn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbxAnvändare = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tbxIP
            // 
            this.tbxIP.Location = new System.Drawing.Point(174, 87);
            this.tbxIP.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxIP.Name = "tbxIP";
            this.tbxIP.Size = new System.Drawing.Size(196, 31);
            this.tbxIP.TabIndex = 0;
            this.tbxIP.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 98);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 150);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // tbxport
            // 
            this.tbxport.Enabled = false;
            this.tbxport.Location = new System.Drawing.Point(174, 150);
            this.tbxport.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxport.Name = "tbxport";
            this.tbxport.Size = new System.Drawing.Size(196, 31);
            this.tbxport.TabIndex = 3;
            this.tbxport.Text = "12345";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(26, 242);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(206, 79);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSendFile
            // 
            this.btnSendFile.Location = new System.Drawing.Point(312, 242);
            this.btnSendFile.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(232, 79);
            this.btnSendFile.TabIndex = 5;
            this.btnSendFile.Text = "Send File";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // btnTaEmot
            // 
            this.btnTaEmot.Enabled = false;
            this.btnTaEmot.Location = new System.Drawing.Point(26, 406);
            this.btnTaEmot.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnTaEmot.Name = "btnTaEmot";
            this.btnTaEmot.Size = new System.Drawing.Size(206, 69);
            this.btnTaEmot.TabIndex = 6;
            this.btnTaEmot.Text = "Ta Emot";
            this.btnTaEmot.UseVisualStyleBackColor = true;
            this.btnTaEmot.Click += new System.EventHandler(this.btnTaEmot_Click);
            // 
            // tbxLogg
            // 
            this.tbxLogg.Location = new System.Drawing.Point(794, 25);
            this.tbxLogg.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxLogg.Multiline = true;
            this.tbxLogg.Name = "tbxLogg";
            this.tbxLogg.Size = new System.Drawing.Size(658, 252);
            this.tbxLogg.TabIndex = 7;
            // 
            // tbxMeddelande
            // 
            this.tbxMeddelande.Location = new System.Drawing.Point(794, 363);
            this.tbxMeddelande.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxMeddelande.Name = "tbxMeddelande";
            this.tbxMeddelande.Size = new System.Drawing.Size(492, 31);
            this.tbxMeddelande.TabIndex = 8;
            // 
            // btnSkicka
            // 
            this.btnSkicka.Location = new System.Drawing.Point(1304, 363);
            this.btnSkicka.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSkicka.Name = "btnSkicka";
            this.btnSkicka.Size = new System.Drawing.Size(152, 112);
            this.btnSkicka.TabIndex = 9;
            this.btnSkicka.Text = "Skicka";
            this.btnSkicka.UseVisualStyleBackColor = true;
            this.btnSkicka.Click += new System.EventHandler(this.btnSkicka_Click);
            // 
            // tbxNamn
            // 
            this.tbxNamn.Location = new System.Drawing.Point(532, 85);
            this.tbxNamn.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbxNamn.Name = "tbxNamn";
            this.tbxNamn.Size = new System.Drawing.Size(196, 31);
            this.tbxNamn.TabIndex = 10;
            this.tbxNamn.Text = "Daniel";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(412, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 25);
            this.label3.TabIndex = 11;
            this.label3.Text = "Namn";
            // 
            // lbxAnvändare
            // 
            this.lbxAnvändare.FormattingEnabled = true;
            this.lbxAnvändare.ItemHeight = 25;
            this.lbxAnvändare.Location = new System.Drawing.Point(1494, 25);
            this.lbxAnvändare.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.lbxAnvändare.Name = "lbxAnvändare";
            this.lbxAnvändare.Size = new System.Drawing.Size(242, 279);
            this.lbxAnvändare.TabIndex = 12;
            this.lbxAnvändare.SelectedIndexChanged += new System.EventHandler(this.lbxAnvändare_SelectedIndexChanged);
            // 
            // Klient
            // 
            this.AcceptButton = this.btnSkicka;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1816, 587);
            this.Controls.Add(this.lbxAnvändare);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxNamn);
            this.Controls.Add(this.btnSkicka);
            this.Controls.Add(this.tbxMeddelande);
            this.Controls.Add(this.tbxLogg);
            this.Controls.Add(this.btnTaEmot);
            this.Controls.Add(this.btnSendFile);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tbxport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxIP);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Klient";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Klient_FormClosing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Klient_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox tbxIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxport;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.Button btnTaEmot;
        private System.Windows.Forms.TextBox tbxLogg;
        private System.Windows.Forms.TextBox tbxMeddelande;
        private System.Windows.Forms.Button btnSkicka;
        private System.Windows.Forms.TextBox tbxNamn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbxAnvändare;
    }
}

