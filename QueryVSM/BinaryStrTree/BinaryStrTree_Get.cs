using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryVSM
{
    partial class BinaryStrTree
    {
        // Get word counter list
        public int[] get_word_counter_list()
        {
            // Get nodes
            int nodes = this.find_nodes();
            if (nodes == 0)
            {
                return null;
            }

            // Processing word list
            int[] counterList = new int[nodes];
            int listShift = 0;
            this.get_word_counter_list_recursive(this.root, ref counterList, ref listShift);

            return counterList;
        }

        // Get word counter list recursive
        private void get_word_counter_list_recursive(_tree[] tree, ref int[] counterList, ref int listShift)
        {
            // Checking
            if (tree != null)
            {
                this.get_word_counter_list_recursive(tree[0].right, ref counterList, ref listShift);
                counterList[listShift++] = tree[0].counter;
                this.get_word_counter_list_recursive(tree[0].left, ref counterList, ref listShift);
            }
        }

        // Get word list
        public String[] get_word_list()
        {
            // Get nodes
            int nodes = this.find_nodes();
            if(nodes == 0)
            {
                return null;
            }

            // Processing word list
            String[] wordList = new String[nodes];
            int listShift = 0;
            this.get_word_list_recursive(this.root, ref wordList, ref listShift);

            return wordList;
        }

        // Get word list recursive
        private void get_word_list_recursive(_tree[] tree, ref String[] wordList, ref int listShift)
        {
            // Checking
            if(tree != null)
            {
                this.get_word_list_recursive(tree[0].right, ref wordList, ref listShift);
                wordList[listShift++] = tree[0].str;
                this.get_word_list_recursive(tree[0].left, ref wordList, ref listShift);
            }
        }

        // Get tree string
        private void get_tree_str_recursive(_tree[] tree, ref String str)
        {
            if (tree != null)
            {
                this.get_tree_str_recursive(tree[0].right, ref str);
                str += tree[0].str + ": " + tree[0].counter.ToString() + "\r\n";
                this.get_tree_str_recursive(tree[0].left, ref str);
            }
        }
    }
}
