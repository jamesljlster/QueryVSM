using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryVSM
{
    partial class BinaryStrTree
    {
        // Function: Copy entire string tree.
        public BinaryStrTree copy()
        {
            BinaryStrTree ret = new BinaryStrTree();

            ret.root = copy_tree_recursive(this.root, false);

            return ret;
        }

        // Function: Copy string tree with entity only.
        public BinaryStrTree copy_entity()
        {
            BinaryStrTree ret = new BinaryStrTree();

            ret.root = copy_tree_recursive(this.root, true);

            return ret;
        }

        // Function: Copy tree recursive
        private _tree[] copy_tree_recursive(_tree[] tree, bool keepEntity)
        {
            // Checking
            if (tree == null)
            {
                return null;
            }

            _tree[] ret = this.alloc_node();

            // Keep entity only or not
            if (!keepEntity)
            {
                ret[0].counter = tree[0].counter;
            }
            ret[0].str = tree[0].str;

            // Copy recursive
            ret[0].left = copy_tree_recursive(tree[0].left, keepEntity);
            ret[0].right = copy_tree_recursive(tree[0].right, keepEntity);

            return ret;
        }
    }
}
