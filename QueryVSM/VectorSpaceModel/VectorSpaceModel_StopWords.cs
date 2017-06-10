using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryVSM
{
    partial class VectorSpaceModel
    {
        // Function: Load stop words from string
        public void load_stop_words(String stopWordsStr, char[] sepCh)
        {
            this.stopWords = new List<String>();
            this.add_stop_words(stopWordsStr, sepCh);
        }

        // Function: Load stop words form list
        public void load_stop_words(String[] stopWordsList)
        {
            this.stopWords = new List<String>();
            this.add_stop_words(stopWordsList);
        }

        // Function: Add stop words from string
        public void add_stop_words(String stopWordsStr, char[] sepCh)
        {
            // Parsing
            String[] wordList = stopWordsStr.Split(sepCh);
            this.add_stop_words(wordList);
        }

        // Function: Add stop words from list
        public void add_stop_words(String[] stopWordsList)
        {
            // Parsing
            for (int i = 0; i < stopWordsList.Length; i++)
            {
                String tmp = stopWordsList[i].ToLower().Trim();
                if (tmp.Length > 0)
                {
                    if (!this.stopWords.Contains(tmp))
                    {
                        this.stopWords.Add(tmp);
                    }
                }
            }
        }

        // Function: Get current stop words list
        public List<String> get_stop_words()
        {
            return this.stopWords;
        }
    }
}
