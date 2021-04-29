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
            this._treSngSelect = new System.Windows.Forms.TreeView();
            this._pnlLogo = new System.Windows.Forms.Panel();
            this._pnlTop = new System.Windows.Forms.Panel();
            this._label2 = new System.Windows.Forms.Label();
            this._label1 = new System.Windows.Forms.Label();
            this._button1 = new System.Windows.Forms.Button();
            this._lblVer = new System.Windows.Forms.Label();
            this._txtBoxDir = new System.Windows.Forms.TextBox();
            this._button2 = new System.Windows.Forms.Button();
            this._pnlPlayField = new System.Windows.Forms.Panel();
            this._rchConsole = new System.Windows.Forms.RichTextBox();
            this._checkBox1 = new System.Windows.Forms.CheckBox();
            this._pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // treSngSelect
            // 
            this._treSngSelect.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (19)))), ((int) (((byte) (15)))), ((int) (((byte) (64)))));
            this._treSngSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._treSngSelect.ForeColor = System.Drawing.Color.White;
            this._treSngSelect.Location = new System.Drawing.Point(0, 347);
            this._treSngSelect.Name = "treSngSelect";
            this._treSngSelect.Size = new System.Drawing.Size(519, 107);
            this._treSngSelect.TabIndex = 0;
            this._treSngSelect.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.SongSelectNodeMouseDoubleClick);
            // 
            // pnlLogo
            // 
            this._pnlLogo.BackgroundImage = global::FNFBot20.Properties.Resources.FNFBotLogo;
            this._pnlLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._pnlLogo.Location = new System.Drawing.Point(3, 6);
            this._pnlLogo.Name = "pnlLogo";
            this._pnlLogo.Size = new System.Drawing.Size(173, 43);
            this._pnlLogo.TabIndex = 1;
            this._pnlLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelLogoMouseDown);
            // 
            // pnlTop
            // 
            this._pnlTop.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (48)))), ((int) (((byte) (51)))), ((int) (((byte) (107)))));
            this._pnlTop.Controls.Add(this._label2);
            this._pnlTop.Controls.Add(this._label1);
            this._pnlTop.Controls.Add(this._button1);
            this._pnlTop.Controls.Add(this._lblVer);
            this._pnlTop.Controls.Add(this._pnlLogo);
            this._pnlTop.Location = new System.Drawing.Point(0, 0);
            this._pnlTop.Name = "pnlTop";
            this._pnlTop.Size = new System.Drawing.Size(519, 52);
            this._pnlTop.TabIndex = 2;
            this._pnlTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelTopMouseDown);
            // 
            // label2
            // 
            this._label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this._label2.ForeColor = System.Drawing.Color.White;
            this._label2.Location = new System.Drawing.Point(297, 6);
            this._label2.Name = "label2";
            this._label2.Size = new System.Drawing.Size(194, 21);
            this._label2.TabIndex = 5;
            this._label2.Text = "Offset: 25";
            // 
            // label1
            // 
            this._label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this._label1.ForeColor = System.Drawing.Color.White;
            this._label1.Location = new System.Drawing.Point(297, 28);
            this._label1.Name = "label1";
            this._label1.Size = new System.Drawing.Size(194, 21);
            this._label1.TabIndex = 4;
            this._label1.Text = "Time: 0";
            this._label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label1MouseDown);
            // 
            // button1
            // 
            this._button1.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (235)))), ((int) (((byte) (77)))), ((int) (((byte) (75)))));
            this._button1.FlatAppearance.BorderSize = 0;
            this._button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._button1.ForeColor = System.Drawing.Color.White;
            this._button1.Location = new System.Drawing.Point(497, 0);
            this._button1.Name = "button1";
            this._button1.Size = new System.Drawing.Size(22, 52);
            this._button1.TabIndex = 3;
            this._button1.Text = "X";
            this._button1.UseVisualStyleBackColor = false;
            this._button1.Click += new System.EventHandler(this.ClickButton1);
            // 
            // lblVer
            // 
            this._lblVer.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this._lblVer.ForeColor = System.Drawing.Color.White;
            this._lblVer.Location = new System.Drawing.Point(177, 14);
            this._lblVer.Name = "lblVer";
            this._lblVer.Size = new System.Drawing.Size(149, 35);
            this._lblVer.TabIndex = 2;
            this._lblVer.Text = "Rewrite";
            this._lblVer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LblVerMouseDown);
            // 
            // txtbxDir
            // 
            this._txtBoxDir.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (48)))), ((int) (((byte) (51)))), ((int) (((byte) (107)))));
            this._txtBoxDir.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._txtBoxDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this._txtBoxDir.ForeColor = System.Drawing.Color.White;
            this._txtBoxDir.Location = new System.Drawing.Point(3, 325);
            this._txtBoxDir.Name = "txtbxDir";
            this._txtBoxDir.Size = new System.Drawing.Size(443, 16);
            this._txtBoxDir.TabIndex = 3;
            this._txtBoxDir.Text = "FNF Game Directory (ex: C:/Users/user/Documents/FNF)";
            this._txtBoxDir.Enter += new System.EventHandler(this.TextBoxDirectoryEnter);
            this._txtBoxDir.Leave += new System.EventHandler(this.TextBoxDirectoryLeave);
            // 
            // button2
            // 
            this._button2.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (48)))), ((int) (((byte) (51)))), ((int) (((byte) (107)))));
            this._button2.FlatAppearance.BorderSize = 0;
            this._button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this._button2.ForeColor = System.Drawing.Color.White;
            this._button2.Location = new System.Drawing.Point(448, 325);
            this._button2.Name = "button2";
            this._button2.Size = new System.Drawing.Size(67, 16);
            this._button2.TabIndex = 4;
            this._button2.Text = "Check Dir";
            this._button2.UseVisualStyleBackColor = false;
            this._button2.Click += new System.EventHandler(this.ClickButton2);
            // 
            // pnlPlayField
            // 
            this._pnlPlayField.Location = new System.Drawing.Point(0, 70);
            this._pnlPlayField.Name = "pnlPlayField";
            this._pnlPlayField.Size = new System.Drawing.Size(213, 252);
            this._pnlPlayField.TabIndex = 5;
            // 
            // rchConsole
            // 
            this._rchConsole.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (19)))), ((int) (((byte) (15)))), ((int) (((byte) (64)))));
            this._rchConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._rchConsole.ForeColor = System.Drawing.Color.White;
            this._rchConsole.Location = new System.Drawing.Point(216, 52);
            this._rchConsole.Name = "rchConsole";
            this._rchConsole.ReadOnly = true;
            this._rchConsole.Size = new System.Drawing.Size(299, 269);
            this._rchConsole.TabIndex = 6;
            this._rchConsole.Text = "";
            // 
            // checkBox1
            // 
            this._checkBox1.ForeColor = System.Drawing.Color.White;
            this._checkBox1.Location = new System.Drawing.Point(3, 52);
            this._checkBox1.Name = "checkBox1";
            this._checkBox1.Size = new System.Drawing.Size(93, 17);
            this._checkBox1.TabIndex = 7;
            this._checkBox1.Text = "Render Notes";
            this._checkBox1.UseVisualStyleBackColor = true;
            this._checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1CheckChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (19)))), ((int) (((byte) (15)))), ((int) (((byte) (64)))));
            this.ClientSize = new System.Drawing.Size(520, 450);
            this.Controls.Add(this._checkBox1);
            this.Controls.Add(this._rchConsole);
            this.Controls.Add(this._pnlPlayField);
            this.Controls.Add(this._button2);
            this.Controls.Add(this._txtBoxDir);
            this.Controls.Add(this._pnlTop);
            this.Controls.Add(this._treSngSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this._pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label _label2;

        private System.Windows.Forms.CheckBox _checkBox1;

        private System.Windows.Forms.Label _label1;

        private System.Windows.Forms.RichTextBox _rchConsole;

        private System.Windows.Forms.Panel _pnlPlayField;

        private System.Windows.Forms.Button _button2;

        private System.Windows.Forms.TextBox _txtBoxDir;

        private System.Windows.Forms.Button _button1;

        private System.Windows.Forms.Label _lblVer;

        private System.Windows.Forms.Panel _pnlTop;

        private System.Windows.Forms.Panel _pnlLogo;


        private System.Windows.Forms.TreeView _treSngSelect;
        #endregion
    }
}