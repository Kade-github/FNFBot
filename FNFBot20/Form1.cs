using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FNFBot20
{
    public partial class Form1 : Form
    {
        public static List<Thread> currentThreads = new List<Thread>();
        public Bot bot { get; set; }

        public static RichTextBox Console { get; set; }
        public static Label WatchTime { get; set; }
        
        public static Label Offset { get; set; }
        
        public static Panel PanelField { get; set; }

        public static bool Rendering = true;

        public static bool LightShow = false;

        public static int SectionSee = 1;
        
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            bot = new Bot();
            Console = _rchConsole;
            Offset = _label2;
            WatchTime = _label1;
            PanelField = _pnlPlayField;
            _checkBox1.Checked = true;
        }


        public static void WriteToConsole(string text)
        {
            Console.Text += "[" + DateTime.Now.ToShortTimeString() + "] " + text + "\n";
        }
        
        private void ClickButton1(object sender, EventArgs e)
        {
            foreach(Thread t in currentThreads)
                t.Abort();
            bot.kBot.StopHooks();
            Environment.Exit(0);
        }


        private void TextBoxDirectoryEnter(object sender, EventArgs e)
        {
            if (_txtBoxDir.Text == "FNF Game Directory (ex: C:/Users/user/Documents/FNF)")
                _txtBoxDir.Text = string.Empty;
        }

        private void TextBoxDirectoryLeave(object sender, EventArgs e)
        {
            if (_txtBoxDir.Text == string.Empty)
                _txtBoxDir.Text = "FNF Game Directory (ex: C:/Users/user/Documents/FNF)";
        }

        private void ClickButton2(object sender, EventArgs e)
        {
            if (!Directory.Exists(_txtBoxDir.Text))
            {
                Form1.WriteToConsole("Directory does not exist");
                return;
            }
            
            Form1.WriteToConsole("Directory found! Retrieving data...");


            try
            {
                foreach (string s in Directory.GetDirectories($@"{_txtBoxDir.Text}\assets\data"))
                {
                    // linq magic
                    // in simple terms, convert a list of files into a TreeNode[],
                    // then make a new TreeNode with the children of the one we made
                    var children = Directory.GetFiles(s).Select(child => new TreeNode(_leadingPath(child)));
                    _treSngSelect.Nodes.Add(new TreeNode(_leadingPath(s), children.ToArray()));
                }
            }
            catch (Exception ee)
            {
                WriteToConsole("Failed to retrieve data.\n" + ee);
            }
        }
        
        private string _leadingPath(string path) => path.Split('\\').Last();
        
        private void SongSelectNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                WriteToConsole("Selecting " + _treSngSelect.SelectedNode.Text);

                bot.Load(_txtBoxDir.Text +
                         $@"\assets\data\{_treSngSelect.SelectedNode.Parent?.Text}\{_treSngSelect.SelectedNode.Text}");
            }
            catch (Exception ee)
            {
                WriteToConsole("Failed to select map.\n" + e);
                WriteToConsole($"Exception => {ee}");
            }
        }

        private void PanelTopMouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0); 
        }

        private void LblVerMouseDown(object sender, MouseEventArgs e)
        {
             ReleaseCapture();
             SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0); 
        }

        private void PanelLogoMouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0); 
        }

        private void Label1MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0); 
        }

        private void CheckBox1CheckChanged(object sender, EventArgs e)
        {
            Rendering = _checkBox1.Checked;
            PanelField.Controls.Clear();
        }

    }
}