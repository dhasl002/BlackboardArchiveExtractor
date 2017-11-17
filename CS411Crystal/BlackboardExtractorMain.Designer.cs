using System.ComponentModel;
using System.Windows.Forms;

namespace CS411Crystal
{
    partial class BlackboardExtractorMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.tbxSourcePath = new System.Windows.Forms.TextBox();
            this.tbxDestination = new System.Windows.Forms.TextBox();
            this.btnExtract = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // tbxSourcePath
            // 
            this.tbxSourcePath.AllowDrop = true;
            this.tbxSourcePath.Location = new System.Drawing.Point(12, 12);
            this.tbxSourcePath.Name = "tbxSourcePath";
            this.tbxSourcePath.Size = new System.Drawing.Size(143, 20);
            this.tbxSourcePath.TabIndex = 0;
            this.tbxSourcePath.Text = "Source Path";
            // 
            // tbxDestination
            // 
            this.tbxDestination.AllowDrop = true;
            this.tbxDestination.Location = new System.Drawing.Point(161, 12);
            this.tbxDestination.Name = "tbxDestination";
            this.tbxDestination.Size = new System.Drawing.Size(143, 20);
            this.tbxDestination.TabIndex = 1;
            this.tbxDestination.Text = "Destination Path";
            // 
            // btnExtract
            // 
            this.btnExtract.Location = new System.Drawing.Point(229, 38);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(75, 23);
            this.btnExtract.TabIndex = 2;
            this.btnExtract.Text = "Extract";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 38);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(211, 23);
            this.progressBar.TabIndex = 3;
            // 
            // BlackboardExtractorMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 74);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnExtract);
            this.Controls.Add(this.tbxDestination);
            this.Controls.Add(this.tbxSourcePath);
            this.Name = "BlackboardExtractorMain";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbxSourcePath;
        private TextBox tbxDestination;
        private Button btnExtract;
        private BackgroundWorker backgroundWorker;
        private BackgroundWorker backgroundWorker1;
        private ProgressBar progressBar;
    }
}

