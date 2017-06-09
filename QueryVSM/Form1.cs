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
            
            List<DataPrep.DocInfo> docList = DataPrep.parse_xml_docs(DataPrep.request_xml_docs("AIDS", 100, 60));
            for(int i = 0; i < docList.Count; i++)
            {
                debugMsg.Text += docList[i].ToString();
                debugMsg.Text += "\r\n";
            }
        }
    }
}
