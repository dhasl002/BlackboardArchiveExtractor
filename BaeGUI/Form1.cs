using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace WindowsFormsApplication2
{
    public partial class Bae : Form
    {
        public Bae()
        {
            InitializeComponent();
        }
        bool fileexist = false;
        bool pathexist = false;
        OpenFileDialog ofd = new OpenFileDialog();
        private void button1_Click(object sender, EventArgs e)
        {
            ofd.Filter = "ZIP|*.zip";
           if(ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.SafeFileName;
                fileexist = true;
            }
        }

        SaveFileDialog sfd = new SaveFileDialog();
        private void button2_Click(object sender, EventArgs e)
        {
            sfd.Filter = "folder|*.";
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = sfd.FileName;
                pathexist = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (fileexist && pathexist)
            {
                ZipFile.ExtractToDirectory(ofd.FileName, sfd.FileName);
                System.Diagnostics.Process.Start("explorer.exe", sfd.FileName);
            }

        }
     }
   }
