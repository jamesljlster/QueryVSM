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

        public MainForm()
        {
            InitializeComponent();

            titleFilter_Button.ForeColor = Color.Red;
            titleFilter_Button.Text = "Title: " + titleFilter.ToString();

            authorFilter_Button.ForeColor = Color.Red;
            authorFilter_Button.Text = "Author Information: " + authorFilter.ToString();

            abstractFilter_Button.ForeColor = Color.Red;
            abstractFilter_Button.Text = "Abstract: " + abstractFilter.ToString();

            /*
            System.Diagnostics.Stopwatch watch;

            Console.Write("Start downloading... ");
            watch = System.Diagnostics.Stopwatch.StartNew();
            String envKey = DataPrep.request_xml_docs("AIDS", 100, 60);
            Console.WriteLine("Finish, Cost {0} ms", watch.ElapsedMilliseconds);

            Console.Write("Parsing... ");
            watch = System.Diagnostics.Stopwatch.StartNew();
            List<DataPrep.DocInfo> docList = DataPrep.parse_xml_docs(envKey);
            Console.WriteLine("Finish, Cost {0} ms", watch.ElapsedMilliseconds);

            VectorSpaceModel vsm = new VectorSpaceModel();
            for(int i = 0; i < docList.Count; i++)
            {
                vsm.add_doc(docList[i].get_text());
            }
            int[] docRankIndex = vsm.get_ranked_doc_index("AIDS");

            for(int i = 0; i < docRankIndex.Length; i++)
            {
                debugMsg.Text += docList[docRankIndex[i]].ToString() + "\r\n";
            }
            */
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
    }
}
