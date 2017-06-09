using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryVSM
{
    partial class BinaryStrTree
    {
        // Function: Insert string entity only.
        public void insert_entity(String str)
        {
            this.insert_recursive(ref this.root, str, true);
        }

        // Function: Public insert element function.
        public void insert(String str)
        {
            this.insert_recursive(ref this.root, str, false);
        }

        // Function: Insert element.
        private void insert_recursive(ref _tree[] tree, String str, bool keepEntity)
        {
            if (tree == null)
            {
                tree = this.alloc_node();
                tree[0].str = str;
                if (!keepEntity)
                    tree[0].counter++;
            }
            else
            {
                int strOrder = this.str_order(tree[0].str, str);
                if (strOrder > 0)
                {
                    this.insert_recursive(ref tree[0].left, str, keepEntity);
                }
                else if (strOrder < 0)
                {
                    this.insert_recursive(ref tree[0].right, str, keepEntity);
                }
                else
                {
                    tree[0].counter++;
                }
            }
        }
    }
}
