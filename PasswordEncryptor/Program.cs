//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace PasswordEncryptor {
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Main object of the application
    /// </summary>
    public static class Program {
        /// <summary>
        /// Main entry point of the application
        /// </summary>
        [STAThread]
        public static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
