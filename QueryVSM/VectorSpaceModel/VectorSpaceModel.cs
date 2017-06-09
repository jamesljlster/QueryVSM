using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QueryVSM
{
    partial class VectorSpaceModel
    {
        // Constructor
        public VectorSpaceModel()
        {
            this.stopWords = new List<String>();
            this.docList = new List< List<String> >();
            this.docBakList = new List<String>();
            this.wordEntity = new BinaryStrTree();
        }

        private List<String> stopWords;
        private List<String> docBakList;
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
            this.docBakList.Add(doc);
            this.docList.Add(tmpDoc);
        }

        // Function: Create binary string tree entity with all documemtation.
        private void update_str_tree()
        {
            for(int i = 0; i < this.docList.Count; i++)
            {
                for(int j = 0; j < this.docList[i].Count; j++)
                {
                    this.wordEntity.insert_entity(this.docList[i][j]);
                }
            }
        }

        // Function: Erase binary string tree
        private void erase_str_tree()
        {
            this.wordEntity = new BinaryStrTree();
        }

        // Function: Get word count list for each document
        private int[][] get_count_list()
        {
            int[][] ret = new int[this.docList.Count][];

            for (int i = 0; i < this.docList.Count; i++)
            {
                BinaryStrTree tmpTree = this.wordEntity.copy_entity();
                for(int j = 0; j < this.docList[i].Count; j++)
                {
                    tmpTree.insert(this.docList[i][j]);
                }
                ret[i] = tmpTree.get_word_counter_list();
            }

            return ret;
        }

        // Function: Calculate document weight vector using tf-idf
        private double[][] get_doc_weight()
        {
            // Memory allocation
            double[][] docWeight = new double[this.docList.Count][];

            // Get word count list
            int[][] wordCountList = this.get_count_list();

            // Calculate document weight vector
            for(int i = 0; i < this.docList.Count; i++)
            {
                docWeight[i] = new double[wordCountList[i].Length];
                for(int j = 0; j < docWeight[i].Length; j++)
                {
                    if(wordCountList[i][j] > 0)
                    {
                        // Get numbers of documents with the term.
                        int docFreq = 0;    
                        for(int k = 0; k < wordCountList.Length; k++)
                        {
                            if(wordCountList[k][j] > 0)
                            {
                                docFreq++;
                            }
                        }

                        // Calculate weight
                        docWeight[i][j] = (1 + Math.Log10(wordCountList[i][j])) * Math.Log10((double)this.docList.Count / (double)docFreq);
                    }
                    else
                    {
                        docWeight[i][j] = 0;
                    }
                }
            }

            return docWeight;
        }

        // Function: Get ranked document list
        public String[] get_ranked_doc(String queryTerm)
        {
            String[] rankedDoc = new String[this.docList.Count];

            // Insert query term as a document
            this.add_doc(queryTerm);

            // Update string tree
            this.update_str_tree();

            // Get document weight vector
            double[][] docVector = this.get_doc_weight();

            // Calculate similarity of each document with query term
            double[] docCosine = new double[docVector.Length - 1];
            for(int i = 0; i < docVector.Length - 1; i++)
            {
                docCosine[i] = this.vector_cos(docVector[docVector.Length - 1], docVector[i]);
            }

            // Sort document with similarity
            int[] docIndex = new int[docVector.Length - 1];
            for(int i = 0; i < docVector.Length - 1; i++)
            {
                docIndex[i] = i;
            }
            Array.Sort(docCosine, docIndex);

            // Remove query term
            this.docBakList.RemoveAt(this.docBakList.Count - 1);
            this.docList.RemoveAt(this.docBakList.Count - 1);

            // Return ranked document
            for (int i = 0; i < docIndex.Length; i++)
            {
                rankedDoc[i] = this.docBakList[docIndex[docIndex.Length - 1 - i]];
            }

            return rankedDoc;
        }
    }
}
