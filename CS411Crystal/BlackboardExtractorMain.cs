using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml.Linq;
// ReSharper disable once RedundantUsingDirective
using ArchiveExtractorBusinessCode;

namespace CS411Crystal
{
    public partial class BlackboardExtractorMain : Form
    {
        public BlackboardExtractorMain()
        {
            InitializeComponent();
            progressBar.Maximum = 100;
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker senderWorker = sender as BackgroundWorker;

            var archiveLocation = "";
            var extractDestination = "";
            var tempLocation = "";

            archiveLocation = tbxSourcePath.Text;
            extractDestination = tbxDestination.Text;
            tempLocation = extractDestination + @"/Archive";

            if (Directory.Exists(tempLocation))
            {
                Directory.Delete(tempLocation, true);
            }
            
            Archive.ExtractArchive(archiveLocation, tempLocation);
            var xml = File.ReadAllText(tempLocation + "/imsmanifest.xml");
            XElement manifest = XElement.Parse(xml);


            List<XElement> xele = ManifestParser.GetOrganizationElements(manifest);
            List<CourseContent> course = new List<CourseContent>();
            int count = 0;
            foreach (XElement x in xele)
            {
                count++;
                CourseContent cc = new CourseContent(x, tempLocation);
                course.Add(cc);

                double progress = 1.0 * count / (1.0 * xele.Count);
                senderWorker.ReportProgress(0,progress);
            }
            Output.CreateRootIndex(course, extractDestination);
            Directory.Delete(tempLocation, true);

        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            double percent = (double)(e.UserState);
            int progress = (int)(percent * 100);
            progressBar.Value = progress;

        }
    }
}
