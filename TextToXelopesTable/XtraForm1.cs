using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace TextToXelopesTable
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            //var path = @"\Users\aizik\Documents\Visual Studio 2015\Projects\BorodaTextToTable\TextToXelopesTable\bin\Debug\full7z - mlteast - ru.lem";
            InitializeComponent();
            // //= "/path/to/the/lemmatizer/file";
            //var stream = File.OpenRead(Application.StartupPath + @"\\full7z-mlteast-ru.lem");
            //var lemmatizer = new Lemmatizer(stream);
            //var result = lemmatizer.Lemmatize("Котейка");
        }
        List<string> paths = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((openFileDialog1.OpenFile()) != null)
                    {
                        string path = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
                        richTextBox1.AppendText(path);
                        richTextBox1.AppendText(Environment.NewLine);
                        paths.Add(path);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk.");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //gg
            paths.Clear();
            richTextBox1.Clear();
        }
    }
}