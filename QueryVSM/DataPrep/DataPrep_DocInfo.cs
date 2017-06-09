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
                this.abstractText = null;
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

                if (this.abstractText != null)
                {
                    tmp += "Abstract:\r\n" + this.abstractText + "\r\n\r\n";
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

                if (this.abstractText != null)
                {
                    text += this.abstractText;
                }

                return text;
            }

            public String title;
            public String abstractText;
            public String authorInfo;
        }
    }
}
