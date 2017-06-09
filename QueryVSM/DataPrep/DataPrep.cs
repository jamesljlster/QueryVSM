using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Net;

namespace QueryVSM
{
    class DocInfo
    {
        // Constructor
        public DocInfo()
        {
            this.title = null;
            this.abstruct = null;
            this.authorInfo = null;
        }

        public override string ToString()
        {
            String tmp = "";

            if(this.title != null)
            {
                tmp += "Title: " + this.title + "\r\n";
            }
            
            if(this.authorInfo != null)
            {
                tmp += "Author Info: " + this.title + "\r\n";
            }

            if(this.abstruct != null)
            {
                tmp += "Abstruct: " + this.abstruct + "\r\n";
            }

            return tmp;
        }

        public String title;
        public String abstruct;
        public String authorInfo;
    }

    class DataPrep
    {
        // Function: Parse xml documents to document list
        public static List<DocInfo> parse_xml_docs(String xmlDoc)
        {
            // Create xml
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlDoc);

            // Parsing xml
            XmlNodeList bookArticle = xml.SelectNodes("PubmedBookArticle");
            XmlNodeList journalArticl = xml.SelectNodes("PubmedArticle");

            return null;
        }

        // Function: Get documents associated with query term
        public static String request_xml_docs(String queryTerm, int retMax, int relDate)
        {
            String url;
            String webEnv;

            StreamReader reader;

            XmlDocument xml;
            XmlNode xmlNode;

            // Generate web environment request url
            url = "https://eutils.ncbi.nlm.nih.gov/entrez/eutils/esearch.fcgi?db=pubmed&datetype=edat&usehistory=y" +
                "&term=" + queryTerm +
                "&reldate=" + relDate.ToString() +
                "&retmax=" + retMax.ToString();

            // Processing request and response
            reader = new StreamReader(WebRequest.Create(url).GetResponse().GetResponseStream());

            // Parsing web environment key
            xml = new XmlDocument();
            xml.LoadXml(reader.ReadToEnd());
            xmlNode = xml.SelectSingleNode("eSearchResult/WebEnv");
            if (xmlNode == null)
            {
                return null;
            }

            webEnv = xmlNode.InnerText;

            // Generate paper request url
            url = "https://eutils.ncbi.nlm.nih.gov/entrez/eutils/efetch.fcgi?db=pubmed&retmode=xml&rettype=abstract&query_key=1&restart=0" +
                "&term=" + queryTerm +
                "&retmax=" + retMax.ToString() +
                "&WebEnv=" + webEnv;

            // Get papers
            reader = new StreamReader(WebRequest.Create(url).GetResponse().GetResponseStream());

            return reader.ReadToEnd();
        }
    }
}
