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
    public partial class Form_StopWords : Form
    {
        public Form_StopWords()
        {
            InitializeComponent();

            // Get stop words
            List<String> stopWords = Program.vsm.get_stop_words();

            // Insert stop words to binary string tree
            for(int i = 0; i < stopWords.Count; i++)
            {
                this.bStrTree.insert_entity(stopWords[i]);
            }

            // Print stop words
            String[] wordList = bStrTree.get_word_list();
            StringBuilder tmpStr = new StringBuilder();
            for(int i = 0; i < wordList.Length; i++)
            {
                tmpStr.Append(wordList[i] + "\r\n");
            }
            stopWords_Msg.Text = tmpStr.ToString();
        }
        
        BinaryStrTree bStrTree = new BinaryStrTree();
    }
}
