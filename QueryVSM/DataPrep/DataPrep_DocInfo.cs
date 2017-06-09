using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryVSM
{
    partial class DataPrep
    {
        public class DocInfo
        {
            // Constructor
            public DocInfo()
            {
                this.title = null;
                this.abstruct = null;
                this.authorInfo = null;
            }

            // Function: To string.
            public override String ToString()
            {
                String tmp = "";

                if (this.title != null)
                {
                    tmp += "Title:\r\n" + this.title + "\r\n\r\n";
                }

                if (this.authorInfo != null)
                {
                    tmp += "Author Info:\r\n" + this.authorInfo + "\r\n\r\n";
                }

                if (this.abstruct != null)
                {
                    tmp += "Abstruct:\r\n" + this.abstruct + "\r\n\r\n";
                }

                return tmp;
            }

            // Function: Get combined text
            public String get_text()
            {
                String text = "";

                if (this.title != null)
                {
                    text += this.title;
                }

                if (this.authorInfo != null)
                {
                    text += this.authorInfo;
                }

                if (this.abstruct != null)
                {
                    text += this.abstruct;
                }

                return text;
            }

            public String title;
            public String abstruct;
            public String authorInfo;
        }
    }
}
