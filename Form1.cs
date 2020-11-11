using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Western2UTF8Converter
{
    public partial class Form1 : Form
    {
        private string inputText;
        private string fileName;
        private string fileFullName;
        private string filePath;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select File...";
            ofd.Filter = "All files(*.*) | *.*";

            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                fileName = ofd.SafeFileName;
                fileFullName = ofd.FileName;
                filePath = fileFullName.Replace(fileName, "");

                tbxFilePath.Text = fileFullName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            inputText = File.ReadAllText(fileFullName, Encoding.GetEncoding(1252));

            Encoding wind1252 = Encoding.GetEncoding(1252);
            Encoding utf8 = Encoding.UTF8;
            byte[] wind1252Bytes = wind1252.GetBytes(inputText);
            byte[] utf8Bytes = Encoding.Convert(wind1252, utf8, wind1252Bytes);
            string utf8String = Encoding.UTF8.GetString(utf8Bytes);

            

            MessageBox.Show("Convert Complete." + Environment.NewLine + 
                "----------------------------------" + Environment.NewLine 
                + inputText);

            if (MessageBox.Show("Do you want to overwrite file?", "Caution", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (StreamWriter outputFile = new StreamWriter(fileFullName))
                {
                    outputFile.Write(utf8String);
                }
                MessageBox.Show("File Saved.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Save Cancelled!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
