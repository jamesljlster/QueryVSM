using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryVSM
{
    partial class BinaryStrTree
    {
        // Function: Find nodes
        public int find_nodes()
        {
            return this.find_nodes_recursive(this.root);
        }

        // Function: Find nodes recursive
        private int find_nodes_recursive(_tree[] tree)
        {
            // Checking
            if (tree == null)
            {
                return 0;
            }
            else
            {
                return 1 + this.find_nodes_recursive(tree[0].left) + this.find_nodes_recursive(tree[0].right);
            }
        }
    }
}
