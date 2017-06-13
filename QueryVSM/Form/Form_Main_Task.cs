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
        Thread[] getDocThread = null;

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

        // Function: Download documents task
        private void download_doc_task(object obj)
        {
            String xmlDoc;
            List<DataPrep.DocInfo> webDocList = new List<DataPrep.DocInfo>();

            // Convert arguments
            List<object> args = (List<object>)obj;
            String queryTerm = (String)args[0];
            List<DataPrep.DocInfo> docList = (List<DataPrep.DocInfo>)args[1];
            
            // Download documents
            try
            {
                xmlDoc = DataPrep.request_xml_docs(queryTerm, Convert.ToInt32(retMax_Num.Value), Convert.ToInt32(relDate_Num.Value));
            }
            catch (Exception)
            {
                return;
            }

            // Parsing documents
            try
            {
                webDocList = DataPrep.parse_xml_docs(xmlDoc);
            }
            catch (Exception)
            {
                return;
            }

            // Append documents
            lock(docList)
            {
                for(int i = 0; i < webDocList.Count; i++)
                {
                    docList.Add(webDocList[i]);
                }
            }
        }

        // Function: Query task
        private void query_task(object obj)
        {
            List<DataPrep.DocInfo> webDocList = new List<DataPrep.DocInfo>();
            System.Diagnostics.Stopwatch watch;
            String[] queryTermList = (String[])obj;

            /** Download documents **/
            // Set ui text and start watch
            set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Start downloading...\r\n");
            set_prog_bar(progressBar, 5);
            watch = System.Diagnostics.Stopwatch.StartNew();

            // Prepare thread
            List<object>[] threadArg = new List<object>[queryTermList.Length];
            this.getDocThread = new Thread[queryTermList.Length];
            for(int i = 0; i < threadArg.Length; i++)
            {
                // Set argument
                threadArg[i] = new List<object>();
                threadArg[i].Add(queryTermList[i]);
                threadArg[i].Add(webDocList);

                // Run thread
                this.getDocThread[i] = new Thread(this.download_doc_task);
                this.getDocThread[i].Start(threadArg[i]);
            }

            // Join thread
            for(int i = 0; i < threadArg.Length; i++)
            {
                this.getDocThread[i].Join();
            }

            lock (this.getDocThread)
            {
                this.getDocThread = null;
            }

            // Set ui text and stop watch
            set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Finish, Cost " + watch.ElapsedMilliseconds.ToString() + " ms\r\n");
            set_prog_bar(progressBar, 20);

            /** Processing document filter **/
            set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Processing filter...\r\n");
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

            /** Processing vector space model **/
            String queryTermStr = "";
            for(int i = 0; i < queryTerms.Length; i++)
            {
                queryTermStr += queryTerms[i];
                if(i != queryTerms.Length - 1)
                {
                    queryTermStr += " ";
                }
            }

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

                int[] rankIndexList = Program.vsm.get_ranked_doc_index(queryTermStr);

                // Sort document list
                for (int i = 0; i < rankIndexList.Length; i++)
                {
                    tmpDocList.Add(webDocList[rankIndexList[i]]);
                }
                set_ui_text(debugMsg, debugMsg.Text + this.get_time_str() + "Finish, Cost " + watch.ElapsedMilliseconds.ToString() + " ms\r\n");
                set_prog_bar(progressBar, 80);
                Console.WriteLine("Finish, Cost {0} ms", watch.ElapsedMilliseconds);
            }

            /** Processing document list **/
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
                lock (this.getDocThread)
                {
                    // Cancel download document thread
                    if(this.getDocThread != null)
                    {
                        for(int i = 0; i < this.getDocThread.Length; i++)
                        {
                            this.getDocThread[i].Abort();
                        }
                    }

                    // Cancle thread
                    queryThread.Abort();
                    this.getDocThread = null;

                    debugMsg.Text += this.get_time_str() + "Query aborted.\r\n";
                    start_Button.Text = "Start Query";
                    progressBar.Value = 0;
                }
            }
            else
            {
                // Process query terms
                String tmpStr = queryTerms_Msg.Text.ToLower().Trim();
                if(tmpStr == "")
                {
                    MessageBox.Show("Invalid query terms!", "Error");
                    return;
                }

                String[] queryTermList = tmpStr.Split(' ');
                for(int i = 0; i < queryTermList.Length; i++)
                {
                    queryTermList[i] = queryTermList[i].Trim();
                }
                
                // Check if query terms are stop words
                for (int i = 0; i < queryTermList.Length; i++)
                {
                    if(Program.vsm.get_stop_words().Contains(queryTermList[i]))
                    {
                        MessageBox.Show("Don't query stop words!", "Error");
                        return;
                    }
                }
                this.queryTerms = queryTermList;

                // Start background task
                queryThread = new Thread(query_task);
                queryThread.Start(queryTermList);
                debugMsg.Text += "\r\n" + this.get_time_str() + "Query started.\r\n";
                start_Button.Text = "Abort Query";
                progressBar.Value = 0;
            }
        }
    }
}
