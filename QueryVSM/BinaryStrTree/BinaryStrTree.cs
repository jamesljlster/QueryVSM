using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryVSM
{
    partial class BinaryStrTree
    {
        // Tree structure
        struct _tree
        {
            public int counter;
            public String str;
            public _tree[] left;
            public _tree[] right;
        }

        private _tree[] root;

        // Constructor
        public BinaryStrTree()
        {
            this.root = null;
        }

        // Function: Allocate a empty node
        private _tree[] alloc_node()
        {
            _tree[] ret = new _tree[1];

            ret[0].counter = 0;
            ret[0].str = null;
            ret[0].left = null;
            ret[0].right = null;

            return ret;
        }

        // Function: Calculate string order
        private int str_order(String cmp, String src)
        {
            int ret = 0;
            int cmpLen = (cmp.Length < src.Length) ? cmp.Length : src.Length;

            for (int i = 0; i < cmpLen; i++)
            {
                if (src[i] > cmp[i])
                {
                    ret = 1;
                    break;
                }
                else if (src[i] < cmp[i])
                {
                    ret = -1;
                    break;
                }
            }

            if (ret == 0)
            {
                if (src.Length > cmp.Length)
                {
                    ret = 1;
                }
                else if (src.Length < cmp.Length)
                {
                    ret = -1;
                }
            }

            return ret;
        }

        // Function: Print tree in order to string
        public override String ToString()
        {
            String ret = "";
            this.get_tree_str_recursive(this.root, ref ret);
            return ret;
        }
    }
}
