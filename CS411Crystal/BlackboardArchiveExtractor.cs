﻿using System;
using System.Windows.Forms;

namespace CS411Crystal
{
    internal static class BlackboardArchiveExtractor
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BlackboardExtractorMain());
        }
    }
}
