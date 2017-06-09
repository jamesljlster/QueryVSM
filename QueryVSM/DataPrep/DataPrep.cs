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
    partial class DataPrep
    {
        // Function: Parse xml documents to document list
        public static List<DocInfo> parse_xml_docs(String xmlDoc)
        {
            List<DocInfo> docList = new List<DocInfo>();

            // Create xml
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlDoc);

            // Select articles
            XmlNodeList bookArticle = xml.SelectNodes("PubmedArticleSet/PubmedBookArticle");
            XmlNodeList journalArticl = xml.SelectNodes("PubmedArticleSet/PubmedArticle");

            // Parsing articles
            parse_journal_xml(ref docList, journalArticl);

            return docList;
        }

        // Function: Parse book article
        private static void parse_journal_xml(ref List<DocInfo> docList, XmlNodeList xmlList)
        {
            for(int i = 0; i < xmlList.Count; i++)
            {
                XmlNode tmpNode;
                XmlNodeList tmpNodeList;
                DocInfo docInfo = new DocInfo();

                // Get article title
                tmpNode = xmlList[i].SelectSingleNode("MedlineCitation/Article/ArticleTitle");
                if(tmpNode != null)
                {
                    docInfo.title = tmpNode.InnerText;
                }

                // Get abstruct
                tmpNodeList = xmlList[i].SelectNodes("MedlineCitation/Article/Abstract/AbstractText");
                if(tmpNodeList != null)
                {
                    String tmp = "";
                    for(int j = 0; j < tmpNodeList.Count; j++)
                    {
                        tmp += tmpNodeList[j].InnerText + "\r\n";
                    }
                    docInfo.abstruct = tmp;
                }

                // Get author info
                tmpNodeList = xmlList[i].SelectNodes("MedlineCitation/Article/AuthorList/Author");
                if(tmpNodeList != null)
                {
                    String tmp = "";
                    for(int j = 0; j < tmpNodeList.Count; j++)
                    {
                        // Get last name
                        tmpNode = tmpNodeList[j].SelectSingleNode("LastName");
                        if(tmpNode != null)
                        {
                            tmp += tmpNode.InnerText + " ";
                        }

                        // Get fore name
                        tmpNode = tmpNodeList[j].SelectSingleNode("ForeName");
                        if(tmpNode != null)
                        {
                            tmp += tmpNode.InnerText + " ";
                        }

                        // Get affiliation information
                        XmlNodeList infoNodeList = tmpNodeList[j].SelectNodes("AffiliationInfo");
                        if(infoNodeList != null)
                        {
                            for (int k = 0; k < infoNodeList.Count; k++)
                            {
                                tmpNode = infoNodeList[k].SelectSingleNode("Affiliation");
                                if(tmpNode != null)
                                {
                                    tmp += tmpNode.InnerText + " ";
                                }
                            }
                        }
                    }

                    docInfo.authorInfo += tmp + "\r\n";
                }

                docList.Add(docInfo);
            }
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
