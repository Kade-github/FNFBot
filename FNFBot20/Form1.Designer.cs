namespace FNFBot20
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treSngSelect = new System.Windows.Forms.TreeView();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblVer = new System.Windows.Forms.Label();
            this.txtbxDir = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.pnlPlayField = new System.Windows.Forms.Panel();
            this.rchConsole = new System.Windows.Forms.RichTextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // treSngSelect
            // 
            this.treSngSelect.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (19)))), ((int) (((byte) (15)))), ((int) (((byte) (64)))));
            this.treSngSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treSngSelect.ForeColor = System.Drawing.Color.White;
            this.treSngSelect.Location = new System.Drawing.Point(0, 347);
            this.treSngSelect.Name = "treSngSelect";
            this.treSngSelect.Size = new System.Drawing.Size(519, 107);
            this.treSngSelect.TabIndex = 0;
            this.treSngSelect.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treSngSelect_NodeMouseDoubleClick);
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackgroundImage = global::FNFBot20.Properties.Resources.FNFBotLogo;
            this.pnlLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLogo.Location = new System.Drawing.Point(3, 6);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(173, 43);
            this.pnlLogo.TabIndex = 1;
            this.pnlLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlLogo_MouseDown);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (48)))), ((int) (((byte) (51)))), ((int) (((byte) (107)))));
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.button1);
            this.pnlTop.Controls.Add(this.lblVer);
            this.pnlTop.Controls.Add(this.pnlLogo);
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(519, 52);
            this.pnlTop.TabIndex = 2;
            this.pnlTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseDown);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(297, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Offset: 25";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(297, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Time: 0";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (235)))), ((int) (((byte) (77)))), ((int) (((byte) (75)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(497, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(22, 52);
            this.button1.TabIndex = 3;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblVer
            // 
            this.lblVer.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblVer.ForeColor = System.Drawing.Color.White;
            this.lblVer.Location = new System.Drawing.Point(177, 14);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(149, 35);
            this.lblVer.TabIndex = 2;
            this.lblVer.Text = "Rewrite";
            this.lblVer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblVer_MouseDown);
            // 
            // txtbxDir
            // 
            this.txtbxDir.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (48)))), ((int) (((byte) (51)))), ((int) (((byte) (107)))));
            this.txtbxDir.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbxDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.txtbxDir.ForeColor = System.Drawing.Color.White;
            this.txtbxDir.Location = new System.Drawing.Point(3, 325);
            this.txtbxDir.Name = "txtbxDir";
            this.txtbxDir.Size = new System.Drawing.Size(443, 16);
            this.txtbxDir.TabIndex = 3;
            this.txtbxDir.Text = "FNF Game Directory (ex: C:/Users/user/Documents/FNF)";
            this.txtbxDir.Enter += new System.EventHandler(this.txtbxDir_Enter);
            this.txtbxDir.Leave += new System.EventHandler(this.txtbxDir_Leave);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (48)))), ((int) (((byte) (51)))), ((int) (((byte) (107)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(448, 325);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 16);
            this.button2.TabIndex = 4;
            this.button2.Text = "Check Dir";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pnlPlayField
            // 
            this.pnlPlayField.Location = new System.Drawing.Point(0, 70);
            this.pnlPlayField.Name = "pnlPlayField";
            this.pnlPlayField.Size = new System.Drawing.Size(213, 252);
            this.pnlPlayField.TabIndex = 5;
            // 
            // rchConsole
            // 
            this.rchConsole.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (19)))), ((int) (((byte) (15)))), ((int) (((byte) (64)))));
            this.rchConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rchConsole.ForeColor = System.Drawing.Color.White;
            this.rchConsole.Location = new System.Drawing.Point(216, 52);
            this.rchConsole.Name = "rchConsole";
            this.rchConsole.ReadOnly = true;
            this.rchConsole.Size = new System.Drawing.Size(299, 269);
            this.rchConsole.TabIndex = 6;
            this.rchConsole.Text = "";
            // 
            // checkBox1
            // 
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(3, 52);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(93, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Render Notes";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (19)))), ((int) (((byte) (15)))), ((int) (((byte) (64)))));
            this.ClientSize = new System.Drawing.Size(520, 450);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.rchConsole);
            this.Controls.Add(this.pnlPlayField);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtbxDir);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.treSngSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.CheckBox checkBox1;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.RichTextBox rchConsole;

        private System.Windows.Forms.Panel pnlPlayField;

        private System.Windows.Forms.Button button2;

        private System.Windows.Forms.TextBox txtbxDir;

        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.Label lblVer;

        private System.Windows.Forms.Panel pnlTop;

        private System.Windows.Forms.Panel pnlLogo;

        private System.Windows.Forms.TreeView treSngSelect;

        #endregion
    }
}