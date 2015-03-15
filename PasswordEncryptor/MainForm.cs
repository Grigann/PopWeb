//-----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace PasswordEncryptor {
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Main form of the application
    /// </summary>
    public partial class MainForm : Form {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm() {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles the click on the Encrypt button event
        /// </summary>
        /// <param name="sender">A sender object</param>
        /// <param name="e">Event arguments</param>
        private void BtnEncrypt_Click(object sender, EventArgs e) {
            this.txtEncrypted.Clear();
            var sourceBytes = Encoding.ASCII.GetBytes(this.txtPassword.Text.Trim());
            var hashedBytes = new MD5CryptoServiceProvider().ComputeHash(sourceBytes);
            this.txtEncrypted.Text = BitConverter.ToString(hashedBytes).Replace("-", "");
        }
    }
}
