using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QueryVSM
{
    class VectorSpaceModel
    {
        // Constructor
        public VectorSpaceModel()
        {
            this.stopWords = new List<String>();
            this.docList = new List< List<String> >();
            this.wordEntity = new BinaryStrTree();
        }

        private List<String> stopWords;
        private List< List<String> > docList;
        private BinaryStrTree wordEntity;

        // Function: Load stop words from file, separate with new line or space
        public void load_stop_words(String filePath)
        {
            // Open file
            StreamReader fReader = new StreamReader(filePath);

            // Parsing
            char[] sepCh = new char[] { '\r', '\n', ' ' };
            String[] wordList = fReader.ReadToEnd().Split(sepCh);
            for(int i = 0; i < wordList.Length; i++)
            {
                String tmp = wordList[i].ToLower().Trim();
                if(tmp != null)
                {
                    if(!this.stopWords.Contains(tmp))
                    {
                        this.stopWords.Add(tmp);
                    }
                }
            }
        }

        // Function: Add a documemtation
        public void add_doc(String doc)
        {
            char[] trimCh = new char[] { ' ', '.', '?', ',', '\"', '\'', '!', '(', ')' };
            char[] sepCh = new char[] { '\r', '\n', ' ' };

            // Parsing documemtation
            String[] wordList = doc.Split(sepCh);
            List<String> tmpDoc = new List<String>();
            for(int i = 0; i < wordList.Length; i++)
            {
                String tmp = wordList[i].ToLower().Trim(trimCh);
                if(tmp != null)
                {
                    if(!this.stopWords.Contains(tmp))
                    {
                        tmpDoc.Add(tmp);
                    }
                }
            }

            // Append documentation
            docList.Add(tmpDoc);
        }

        // Function: Create binary string tree with all documemtation.
        private void update_str_tree()
        {
            
        }
    }
}
