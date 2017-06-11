namespace QueryVSM
{
    partial class Form_StopWords
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stopWords_List = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.remove_Button = new System.Windows.Forms.Button();
            this.insert_Button = new System.Windows.Forms.Button();
            this.add_Msg = new System.Windows.Forms.TextBox();
            this.close_Button = new System.Windows.Forms.Button();
            this.import_Button = new System.Windows.Forms.Button();
            this.append_Button = new System.Windows.Forms.Button();
            this.export_Button = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stopWords_List);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stop Words";
            // 
            // stopWords_List
            // 
            this.stopWords_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stopWords_List.FormattingEnabled = true;
            this.stopWords_List.Location = new System.Drawing.Point(3, 18);
            this.stopWords_List.Name = "stopWords_List";
            this.stopWords_List.Size = new System.Drawing.Size(144, 216);
            this.stopWords_List.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.remove_Button);
            this.groupBox2.Controls.Add(this.insert_Button);
            this.groupBox2.Controls.Add(this.add_Msg);
            this.groupBox2.Location = new System.Drawing.Point(168, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(204, 108);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add / Remove";
            // 
            // remove_Button
            // 
            this.remove_Button.Location = new System.Drawing.Point(6, 78);
            this.remove_Button.Name = "remove_Button";
            this.remove_Button.Size = new System.Drawing.Size(192, 23);
            this.remove_Button.TabIndex = 2;
            this.remove_Button.Text = "Remove Selected";
            this.remove_Button.UseVisualStyleBackColor = true;
            this.remove_Button.Click += new System.EventHandler(this.remove_Button_Click);
            // 
            // insert_Button
            // 
            this.insert_Button.Location = new System.Drawing.Point(6, 49);
            this.insert_Button.Name = "insert_Button";
            this.insert_Button.Size = new System.Drawing.Size(192, 23);
            this.insert_Button.TabIndex = 1;
            this.insert_Button.Text = "Insert";
            this.insert_Button.UseVisualStyleBackColor = true;
            this.insert_Button.Click += new System.EventHandler(this.insert_Button_Click);
            // 
            // add_Msg
            // 
            this.add_Msg.Location = new System.Drawing.Point(6, 21);
            this.add_Msg.Name = "add_Msg";
            this.add_Msg.Size = new System.Drawing.Size(192, 22);
            this.add_Msg.TabIndex = 0;
            // 
            // close_Button
            // 
            this.close_Button.Location = new System.Drawing.Point(168, 226);
            this.close_Button.Name = "close_Button";
            this.close_Button.Size = new System.Drawing.Size(204, 23);
            this.close_Button.TabIndex = 4;
            this.close_Button.Text = "Close";
            this.close_Button.UseVisualStyleBackColor = true;
            this.close_Button.Click += new System.EventHandler(this.close_Button_Click);
            // 
            // import_Button
            // 
            this.import_Button.Location = new System.Drawing.Point(168, 126);
            this.import_Button.Name = "import_Button";
            this.import_Button.Size = new System.Drawing.Size(204, 23);
            this.import_Button.TabIndex = 7;
            this.import_Button.Text = "Import from File";
            this.import_Button.UseVisualStyleBackColor = true;
            this.import_Button.Click += new System.EventHandler(this.import_Button_Click);
            // 
            // append_Button
            // 
            this.append_Button.Location = new System.Drawing.Point(168, 155);
            this.append_Button.Name = "append_Button";
            this.append_Button.Size = new System.Drawing.Size(204, 23);
            this.append_Button.TabIndex = 8;
            this.append_Button.Text = "Append from file";
            this.append_Button.UseVisualStyleBackColor = true;
            this.append_Button.Click += new System.EventHandler(this.append_Button_Click);
            // 
            // export_Button
            // 
            this.export_Button.Location = new System.Drawing.Point(168, 184);
            this.export_Button.Name = "export_Button";
            this.export_Button.Size = new System.Drawing.Size(204, 23);
            this.export_Button.TabIndex = 9;
            this.export_Button.Text = "Export to File";
            this.export_Button.UseVisualStyleBackColor = true;
            this.export_Button.Click += new System.EventHandler(this.export_Button_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "StopWords.txt";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "txt";
            this.saveFileDialog.Filter = "文字檔 (*.txt)|*.txt|所有檔案 (*.*)|*.*";
            // 
            // Form_StopWords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.export_Button);
            this.Controls.Add(this.append_Button);
            this.Controls.Add(this.import_Button);
            this.Controls.Add(this.close_Button);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_StopWords";
            this.Text = "Stop Words Setting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button insert_Button;
        private System.Windows.Forms.TextBox add_Msg;
        private System.Windows.Forms.Button close_Button;
        private System.Windows.Forms.Button import_Button;
        private System.Windows.Forms.Button append_Button;
        private System.Windows.Forms.Button export_Button;
        private System.Windows.Forms.Button remove_Button;
        private System.Windows.Forms.CheckedListBox stopWords_List;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}