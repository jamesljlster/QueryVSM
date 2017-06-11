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

namespace QueryVSM
{
    public partial class Form_StopWords : Form
    {
        List<String> stopWords;

        public Form_StopWords()
        {
            InitializeComponent();

            // Get stop words
            this.get_stop_words();

            // Print stop words
            this.print_stop_words();
        }

        private void insert_stop_word(String stopWord)
        {
            // Insert new stop word
            String tmp = stopWord.ToLower().Trim();
            if (!this.stopWords.Contains(tmp) && tmp.Length > 0)
            {
                this.stopWords.Add(tmp);
            }
        }

        private String[] read_words(String filePath)
        {
            StreamReader fReader = new StreamReader(filePath);
            char[] sepCh = new char[] { '\r', '\n', ' ' };
            return fReader.ReadToEnd().Split(sepCh);
        }

        private void clear_stop_words()
        {
            // Clear stop words
            this.stopWords.Clear();
        }
        
        private void get_stop_words()
        {
            // Get stop words
            this.stopWords = Program.vsm.get_stop_words();
        }

        private void print_stop_words()
        {
            // Sort
            this.stopWords.Sort();

            // Print
            stopWords_List.Items.Clear();
            for (int i = 0; i < stopWords.Count; i++)
            {
                stopWords_List.Items.Add(stopWords[i]);
            }
        }

        private void insert_Button_Click(object sender, EventArgs e)
        {
            if (add_Msg.Text.Length > 0)
            {
                // Insert new stop word
                this.insert_stop_word(add_Msg.Text);
                add_Msg.Text = "";
                this.print_stop_words();
            }
        }

        private void close_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void remove_Button_Click(object sender, EventArgs e)
        {
            if (stopWords_List.CheckedItems.Count > 0)
            {
                // Remove checked stop words
                for (int i = 0; i < stopWords_List.CheckedItems.Count; i++)
                {
                    this.stopWords.Remove((String)stopWords_List.CheckedItems[i]);
                }

                this.print_stop_words();
            }
        }

        private void import_Button_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String[] words;

                try
                {
                    words = this.read_words(openFileDialog.FileName);

                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to import stop words!", "Error");
                    return;
                }

                // Clear stop words
                this.stopWords.Clear();

                // Insert stop words
                for (int i = 0; i < words.Length; i++)
                {
                    this.insert_stop_word(words[i]);
                }

                // Update stop words list
                this.print_stop_words();
            }
        }

        private void append_Button_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String[] words;

                try
                {
                    words = this.read_words(openFileDialog.FileName);

                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to import stop words!", "Error");
                    return;
                }
                
                // Insert stop words
                for (int i = 0; i < words.Length; i++)
                {
                    this.insert_stop_word(words[i]);
                }

                // Update stop words list
                this.print_stop_words();
            }
        }

        private void export_Button_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter fWriter;

                // Open and write file
                try
                {
                    fWriter = new StreamWriter(saveFileDialog.FileName, false);
                    for (int i = 0; i < this.stopWords.Count; i++)
                    {
                        fWriter.WriteLine(stopWords[i]);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to export stop words!", "Error");
                    return;
                }
                
                fWriter.Flush();
                fWriter.Close();
            }
        }
    }
}
