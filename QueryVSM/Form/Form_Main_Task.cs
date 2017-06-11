using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QueryVSM
{
    public partial class MainForm : Form
    {
        Thread queryThread;

        private delegate void SetChkBoxList(CheckedListBox dst, CheckedListBox src);
        private delegate void SetUIText(Control ctrl, String str);
        private delegate void SetProgBar(ProgressBar progBar, int status);

        private void set_prog_bar(ProgressBar progBar, int status)
        {
            if(this.InvokeRequired)
            {
                SetProgBar setProgBar = new SetProgBar(set_prog_bar);
                this.Invoke(setProgBar, progBar, status);
            }
            else
            {
                progBar.Value = status;
            }
        }

        private void set_chk_box_list(CheckedListBox dst, CheckedListBox src)
        {
            if(this.InvokeRequired)
            {
                SetChkBoxList setChkBoxList = new SetChkBoxList(set_chk_box_list);
                this.Invoke(setChkBoxList, dst, src);
            }
            else
            {
                dst.Items.Clear();
                for(int i = 0; i < src.Items.Count; i++)
                {
                    dst.Items.Add(src.Items[i]);
                }
            }
        }

        private void set_ui_text(Control ctrl, String str)
        {
            if (this.InvokeRequired)
            {
                SetUIText setUI = new SetUIText(set_ui_text);
                this.Invoke(setUI, ctrl, str);
            }
            else
            {
                ctrl.Text = str;
            }
        }
        
        // Function: Combine time string
        private String get_time_str()
        {
            return "[ " + 
                DateTime.Now.Hour.ToString() + ":" +
                DateTime.Now.Minute.ToString() + ":" +
                DateTime.Now.Second.ToString() + 
                " ] ";
        }

        // Function: Query task
        private void query_task(object obj)
        {
            List<DataPrep.DocInfo> webDocList = new List<DataPrep.DocInfo>();
            System.Diagnostics.Stopwatch watch;
            String queryTerms = (String)obj;
            String xmlDoc;

            // Download documents
            try
            {
                set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Start downloading...\r\n");
                set_prog_bar(progressBar, 5);
                Console.Write("Start downloading... ");
                watch = System.Diagnostics.Stopwatch.StartNew();

                xmlDoc = DataPrep.request_xml_docs(queryTerms, Convert.ToInt32(retMax_Num.Value), Convert.ToInt32(relDate_Num.Value));
                set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Finish, Cost " + watch.ElapsedMilliseconds.ToString() + " ms\r\n");
                set_prog_bar(progressBar, 20);
                Console.WriteLine("Finish, Cost {0} ms", watch.ElapsedMilliseconds);
            }
            catch(Exception)
            {
                set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Download failed!\r\n");
                set_prog_bar(progressBar, 0);
                return;
            }

            // Parsing documents
            try
            {
                set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Parsing documents...\r\n");
                Console.Write("Parsing documents...");
                watch = System.Diagnostics.Stopwatch.StartNew();
                webDocList = DataPrep.parse_xml_docs(xmlDoc);
                set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Finish, Cost " + watch.ElapsedMilliseconds.ToString() + " ms\r\n");
                set_prog_bar(progressBar, 40);
                Console.WriteLine("Finish, Cost {0} ms", watch.ElapsedMilliseconds);
            }
            catch (Exception)
            {
                set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Parsing failed!\r\n");
                set_prog_bar(progressBar, 0);
                return;
            }

            // Processing document filter
            set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Processing filter...\r\n");
            Console.Write("Processing document filter... ");
            watch = System.Diagnostics.Stopwatch.StartNew();
            if (titleFilter)
            {
                for (int i = 0; i < webDocList.Count; i++)
                {
                    if (webDocList[i].title.Length <= 0)
                    {
                        webDocList.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (authorFilter)
            {
                for (int i = 0; i < webDocList.Count; i++)
                {
                    if (webDocList[i].authorInfo.Length <= 0)
                    {
                        webDocList.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (abstractFilter)
            {
                for (int i = 0; i < webDocList.Count; i++)
                {
                    if (webDocList[i].abstractText.Length <= 0)
                    {
                        webDocList.RemoveAt(i);
                        i--;
                    }
                }
            }

            set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Finish, Cost " + watch.ElapsedMilliseconds.ToString() + " ms\r\n");
            set_prog_bar(progressBar, 60);
            Console.WriteLine("Finish, Cost {0} ms", watch.ElapsedMilliseconds);

            // Processing vector space model
            List<DataPrep.DocInfo> tmpDocList = new List<DataPrep.DocInfo>();
            lock (Program.vsm)
            {
                Console.Write("Processing VSM... ");
                set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Processing VSM...\r\n");
                watch = System.Diagnostics.Stopwatch.StartNew();
                Program.vsm.restart();
                for (int i = 0; i < webDocList.Count; i++)
                {
                    Program.vsm.add_doc(webDocList[i].get_text());
                }

                int[] rankIndexList = Program.vsm.get_ranked_doc_index(queryTerms);
                Console.WriteLine("Finish, Cost {0} ms", watch.ElapsedMilliseconds);

                // Sort document list
                Console.Write("Sort documents... ");
                watch = System.Diagnostics.Stopwatch.StartNew();
                for (int i = 0; i < rankIndexList.Length; i++)
                {
                    tmpDocList.Add(webDocList[rankIndexList[i]]);
                }
                set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Finish, Cost " + watch.ElapsedMilliseconds.ToString() + " ms\r\n");
                set_prog_bar(progressBar, 80);
                Console.WriteLine("Finish, Cost {0} ms", watch.ElapsedMilliseconds);
            }

            // Processing document list
            Console.Write("Processing document list... ");
            set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Update document list...\r\n");
            watch = System.Diagnostics.Stopwatch.StartNew();
            CheckedListBox tmpDocListBox = new CheckedListBox();
            for (int i = 0; i < tmpDocList.Count; i++)
            {
                tmpDocListBox.Items.Add((i + 1).ToString() + ": " + tmpDocList[i].title);
            }
            this.docList = tmpDocList;
            set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Finish, Cost " + watch.ElapsedMilliseconds.ToString() + " ms\r\n");
            Console.WriteLine("Finish, Cost {0} ms", watch.ElapsedMilliseconds);

            // Update ui
            set_ui_text(docCount_Label, "Counts: " + tmpDocListBox.Items.Count.ToString());
            set_chk_box_list(docListBox, tmpDocListBox);

            // Query task finished
            set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Query finished.\r\n");
            set_prog_bar(progressBar, 100);
            set_ui_text(start_Button, "Start Query");
        }

        private void start_Button_Click(object sender, EventArgs e)
        {
            if (queryThread != null && queryThread.IsAlive)
            {
                // Cancle thread
                queryThread.Abort();
                debugMsg.Text += this.get_time_str() + "Query aborted.\r\n";
                start_Button.Text = "Start Query";
                progressBar.Value = 0;
            }
            else
            {
                // Process query terms
                String queryTermsBak = queryTerms_Msg.Text;
                String[] queryTermList = queryTermsBak.ToLower().Split(' ');
                this.queryTerms = queryTermList;
                String queryTermStr = "";
                for (int i = 0; i < queryTermList.Length; i++)
                {
                    queryTermStr += queryTermList[i].Trim();
                    if (i != queryTermList.Length - 1)
                    {
                        queryTermStr += "+";
                    }
                }

                if (queryTermStr == "")
                {
                    MessageBox.Show("Invalid query terms!", "Error");
                    return;
                }

                // Start background task
                queryThread = new Thread(query_task);
                queryThread.Start(queryTermStr);
                debugMsg.Text += "\r\n" + this.get_time_str() + "Query started.\r\n";
                start_Button.Text = "Abort Query";
                progressBar.Value = 0;
            }
        }
    }
}
