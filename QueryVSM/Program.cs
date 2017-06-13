using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace QueryVSM
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Load default stop words
            try
            {
                StreamReader fReader = new StreamReader("StopWords.txt");
                char[] sepCh = new char[] { '\r', '\n', ',' };
                vsm.load_stop_words(fReader.ReadToEnd(), sepCh);
            }
            catch(Exception)
            {
                MessageBox.Show("Default stop words file was not found or occupied.", "Error");
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
        
        public static VectorSpaceModel vsm = new VectorSpaceModel();

        // Function: Highlight words in rich text box.
        public static void highlight(this RichTextBox richTextBox, String word, Color foreColor, Color backColor)
        {
            // Checking
            if(word.Length <= 0)
            {
                return;
            }
            
            // Searching
            int procIndex = 0;
            while ((procIndex = richTextBox.Find(word, procIndex, RichTextBoxFinds.WholeWord)) >= 0)
            {
                richTextBox.Select(procIndex, word.Length);
                richTextBox.SelectionBackColor = backColor;
                richTextBox.SelectionColor = foreColor;

                procIndex += word.Length;
            }
        }
    }
}
