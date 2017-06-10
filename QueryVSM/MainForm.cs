using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QueryVSM
{
    public partial class MainForm : Form
    {
        bool titleFilter = false;
        bool authorFilter = false;
        bool abstractFilter = false;

        List<DataPrep.DocInfo> docList;

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

        private void start_Button_Click(object sender, EventArgs e)
        {
            // Process query terms
            String queryTermsBak = queryTerms_Msg.Text;
            String[] queryTermList = queryTermsBak.ToLower().Split(' ');
            String queryTerms = "";
            for(int i = 0; i < queryTermList.Length; i++)
            {
                queryTerms += queryTermList[i].Trim();
                if(i != queryTermList.Length - 1)
                {
                    queryTerms += "+";
                }
            }

            if(queryTerms == "")
            {
                MessageBox.Show("Invalid query terms!", "Error");
                return;
            }

            // Download documents
            System.Diagnostics.Stopwatch watch;
            Console.Write("Start downloading... ");
            watch = System.Diagnostics.Stopwatch.StartNew();
            
            String xmlDoc = DataPrep.request_xml_docs(queryTerms, Convert.ToInt32(retMax_Num.Value), Convert.ToInt32(relDate_Num.Value));
            Console.WriteLine("Finish, Cost {0} ms", watch.ElapsedMilliseconds);

            Console.Write("Parsing... ");
            watch = System.Diagnostics.Stopwatch.StartNew();
            this.docList = DataPrep.parse_xml_docs(xmlDoc);
            Console.WriteLine("Finish, Cost {0} ms", watch.ElapsedMilliseconds);

            // Processing document filter
            Console.Write("Processing document filter... ");
            watch = System.Diagnostics.Stopwatch.StartNew();
            if(titleFilter)
            {
                for(int i = 0; i < this.docList.Count; i++)
                {
                    if(docList[i].title.Length <= 0)
                    {
                        docList.RemoveAt(i);
                        i--;
                    }
                }
            }

            if(authorFilter)
            {
                for (int i = 0; i < this.docList.Count; i++)
                {
                    if (docList[i].authorInfo.Length <= 0)
                    {
                        docList.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (abstractFilter)
            {
                for (int i = 0; i < this.docList.Count; i++)
                {
                    if (docList[i].abstractText.Length <= 0)
                    {
                        docList.RemoveAt(i);
                        i--;
                    }
                }
            }

            Console.WriteLine("Finish, Cost {0} ms", watch.ElapsedMilliseconds);
            
            // Processing vector space model
            Console.Write("Processing VSM... ");
            watch = System.Diagnostics.Stopwatch.StartNew();
            Program.vsm.restart();
            for(int i = 0; i < this.docList.Count; i++)
            {
                Program.vsm.add_doc(this.docList[i].get_text());
            }
            int[] docRankIndex = Program.vsm.get_ranked_doc_index(queryTerms);
            Console.WriteLine("Finish, Cost {0} ms", watch.ElapsedMilliseconds);

            // Processing document list
            Console.Write("Processing document list... ");
            watch = System.Diagnostics.Stopwatch.StartNew();
            docListBox.Items.Clear();
            for (int i = 0; i < this.docList.Count; i++)
            {
                docListBox.Items.Add(docList[i].title);
            }
            Console.WriteLine("Finish, Cost {0} ms", watch.ElapsedMilliseconds);
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
        }
    }
}
