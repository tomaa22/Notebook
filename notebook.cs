using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;


    public partial class notebook : Form
    {
        string current_filename = "";
        bool wasChanged = false;
        string sign;

        public notebook()
        {
            InitializeComponent();
        }

        private void btnExit(object sender, EventArgs e)
        {
            Close();
        }

        void SaveChanges(object sender, EventArgs e)
        {
            if (MessageBox.Show("Хотите сохранить изменения?", "Блокнот", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                btnSave(sender, e);
            }
        }

        private void btnNew(object sender, EventArgs e)
        {
            if (wasChanged)
            {
                SaveChanges(sender, e);
            }
            textBoxField.Text = "";
            current_filename = "";
            wasChanged = false;
        }

        private void btnOpen(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (wasChanged)
                {
                    SaveChanges(sender, e);
                }
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                textBoxField.Text = sr.ReadToEnd();
                sr.Close();
                current_filename = openFileDialog1.FileName;
                wasChanged = false;
            }
        }

        void SaveToFile(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            sw.WriteLine(textBoxField.Text);
            sw.WriteLine(sign);
            sw.Close();
            wasChanged = false;
        }

        private void btnSave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(current_filename))
            {
                btnSaveF(sender, e);
                return;
            }
            SaveToFile(current_filename);
        }

        private void btnSaveF(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SaveToFile(saveFileDialog1.FileName);
                current_filename = saveFileDialog1.FileName;
            }
        }

        private void notebook_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader("settings.txt");
                textBoxField.Font = new Font(textBoxField.Font.FontFamily, int.Parse(sr.ReadLine()));
                sign = "";
                while (!sr.EndOfStream)
                {
                    sign += "\r\n" + sr.ReadLine();
                }
                sr.Close();
            }
            catch { }

            fontDialog1.Font = textBoxField.Font;
            colorDialog1.Color = textBoxField.ForeColor;
            Clipboard.Clear();
        }

        private void textBoxField_TextChanged(object sender, EventArgs e)
        {
            wasChanged = true;
        }

        private void notebook_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (wasChanged)
            {
                SaveChanges(sender, e);
            }
        }

        private void btnShrift(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxField.Font = fontDialog1.Font;
            }
        }

        private void btnColor(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxField.ForeColor = colorDialog1.Color;
            }
        }

        private void btnAll(object sender, EventArgs e)
        {
            textBoxField.SelectAll();
        }

        private void btnCopy(object sender, EventArgs e)
        {
            textBoxField.Copy();
        }

        private void btnCut(object sender, EventArgs e)
        {
            textBoxField.Cut();
        }

        private void btnPaste(object sender, EventArgs e)
        {
            textBoxField.Paste();
        }

    }
     
