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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            VectorSpaceModel vsm = new VectorSpaceModel();
            vsm.add_doc("Users need to translate their information needs into queries and submit into IR systems.The key goal of an IR system is to retrieve all the items that are relevant to a user query, while retrieving as few nonrelevant items as possible.");
            vsm.add_doc("Possible approaches to effectively and efficiently searching or finding relevant documents.Ranking scores all retrieved documents according to a relevance metric.");
            vsm.add_doc("Initial exploration of text retrieval systems for “small” corpora of scientific abstracts, and law and business documents.IR systems have filters to handle most popular documents.");
            vsm.add_doc("Strings of the same length, distance between them is the number of positions with different characters.The definition of a ranking function that allows quantifying the similarities among documents and queries.");

            String[] rankedDoc = vsm.get_ranked_doc("user");

            for(int i = 0; i < rankedDoc.Length; i++)
            {
                debugMsg.Text += "Rank " + i.ToString() + "\r\n";
                debugMsg.Text += rankedDoc[i] + "\r\n";
                debugMsg.Text += "\r\n";
            }
        }
    }
}
