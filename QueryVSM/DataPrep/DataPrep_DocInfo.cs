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
                    tmp += "Title: " + this.title + "\r\n";
                }

                if (this.authorInfo != null)
                {
                    tmp += "Author Info: " + this.title + "\r\n";
                }

                if (this.abstruct != null)
                {
                    tmp += "Abstruct: " + this.abstruct + "\r\n";
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
                    text += this.title;
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
