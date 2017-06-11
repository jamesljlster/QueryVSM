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
    public partial class MainForm : Form
    {
        bool titleFilter = false;
        bool authorFilter = false;
        bool abstractFilter = false;
        
        List<DataPrep.DocInfo> docList;
        String[] queryTerms = null;
        
        public MainForm()
        {
            InitializeComponent();

            titleFilter_Button.ForeColor = Color.Red;
            titleFilter_Button.Text = "Title: " + titleFilter.ToString();

            authorFilter_Button.ForeColor = Color.Red;
            authorFilter_Button.Text = "Author Information: " + authorFilter.ToString();

            abstractFilter_Button.ForeColor = Color.Red;
            abstractFilter_Button.Text = "Abstract: " + abstractFilter.ToString();
        }

        private void titleFilter_Button_Click(object sender, EventArgs e)
        {
            titleFilter = !titleFilter;
            if(titleFilter)
            {
                titleFilter_Button.ForeColor = Color.Blue;
            }
            else
            {
                titleFilter_Button.ForeColor = Color.Red;
            }
            titleFilter_Button.Text = "Title: " + titleFilter.ToString();
        }

        private void authorFilter_Button_Click(object sender, EventArgs e)
        {
            authorFilter = !authorFilter;
            if (authorFilter)
            {
                authorFilter_Button.ForeColor = Color.Blue;
            }
            else
            {
                authorFilter_Button.ForeColor = Color.Red;
            }
            authorFilter_Button.Text = "Author Information: " + authorFilter.ToString();
        }

        private void abstractFilter_Button_Click(object sender, EventArgs e)
        {
            abstractFilter = !abstractFilter;
            if (abstractFilter)
            {
                abstractFilter_Button.ForeColor = Color.Blue;
            }
            else
            {
                abstractFilter_Button.ForeColor = Color.Red;
            }
            abstractFilter_Button.Text = "Abstract: " + abstractFilter.ToString();
        }
        
        private void docListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Print document detail to right panel
            if (docList[docListBox.SelectedIndex].title != null)
            {
                title_Msg.Text = docList[docListBox.SelectedIndex].title;
            }

            if (docList[docListBox.SelectedIndex].authorInfo != null)
            {
                author_Msg.Text = docList[docListBox.SelectedIndex].authorInfo;
            }

            if (docList[docListBox.SelectedIndex].abstractText != null)
            {
                abstract_Msg.Text = docList[docListBox.SelectedIndex].abstractText;
            }

            // Highlighting
            if(this.queryTerms != null)
            {
                for(int i = 0; i < this.queryTerms.Length; i++)
                {
                    title_Msg.highlight(this.queryTerms[i], Color.Red, Color.Yellow);
                    author_Msg.highlight(this.queryTerms[i], Color.Red, Color.Yellow);
                    abstract_Msg.highlight(this.queryTerms[i], Color.Red, Color.Yellow);
                }
            }
        }

        private void stopWordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new Form_StopWords();
            form.ShowDialog();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(docListBox.CheckedIndices.Count > 0)
            {
                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter fWriter;

                    try
                    {
                        // Write checked documents
                        fWriter = new StreamWriter(saveFileDialog.FileName);
                        for(int i = 0; i < docListBox.CheckedIndices.Count; i++)
                        {
                            int indexTmp = docListBox.CheckedIndices[i];
                            fWriter.Write((indexTmp + 1).ToString() + ". ");
                            fWriter.Write(docList[indexTmp].ToString());
                            fWriter.WriteLine();
                        }

                        fWriter.Flush();
                        fWriter.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Failed to write file!", "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("No documents checked!", "Error");
            }
        }
    }
}
