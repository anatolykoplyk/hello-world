using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace RSAvsElliptic
{
    public partial class MainForm : Form
    {
        readonly RSA _rsa = new RSA();
        readonly ECC _ecc = new ECC();
        RSAParameters _keyDigitalSignature;
        readonly Stopwatch _sw = new Stopwatch();
        readonly Stopwatch _sw1 = new Stopwatch();
        private const int EccBlockSize = 20;

        public MainForm()
        {
            InitializeComponent();
            tabControl1.AutoSize = true;
            tabControl1.TabPages[0].AutoSize = true;
            comboBoxECCKeySize.Text = comboBoxECCKeySize.Items[0].ToString();
            label2.Text = KeySizeRSA.Value.ToString();
        }

        private void KeySizeRSA_ValueChanged(object sender, EventArgs e)
        {
            label2.Text = KeySizeRSA.Value.ToString();
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            var ofd = new OpenFileDialog
            {
                Title = "Select file to work with",
                InitialDirectory = "c:\\",
                FileName = @"C:\Users\anatoliy.koplik\Desktop\New Text Document.txt",
                Filter = "Txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxFilePath.Text = "";
                try
                {
                    myStream = ofd.OpenFile();
                    using (myStream)
                    {
                        textBoxFilePath.Text = ofd.FileName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void Encrypt_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxFilePath.Text))
            {
                MessageBox.Show("First please enter file to encrypt.");
                return;
            }
            try
            {
                richTextBoxRSAparams.Clear();
                BinaryReader br = new BinaryReader(File.OpenRead(textBoxFilePath.Text));
                int blockSize = (KeySizeRSA.Value - 384) / 8 + 37;

                var chunk = br.ReadBytes(blockSize);
                while (chunk.Length > 0)
                {
                    _sw.Start();
                    _rsa.Encrypt(chunk, KeySizeRSA.Value);
                    _sw.Stop();
                    chunk = br.ReadBytes(blockSize);
                }

                TimeSpan ts = _sw.Elapsed;
                _sw.Reset();

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

                richTextBoxLog.AppendText("RSA Encrypting time:\n");
                richTextBoxLog.AppendText(elapsedTime + "\n");
                richTextBoxLog.AppendText("\nRSA last chunk of encrypted data:\n" + Encoding.Default.GetString(_rsa.EncryptedData) + "\n\n");

                richTextBoxRSAparams.AppendText("D: " + Convert.ToBase64String(_rsa.RSAParams.D) + "\n");
                richTextBoxRSAparams.AppendText("DP: " + Convert.ToBase64String(_rsa.RSAParams.DP) + "\n");
                richTextBoxRSAparams.AppendText("DQ: " + Convert.ToBase64String(_rsa.RSAParams.DQ) + "\n");
                richTextBoxRSAparams.AppendText("Exponent: " + Convert.ToBase64String(_rsa.RSAParams.Exponent) + "\n");
                richTextBoxRSAparams.AppendText("InverseQ: " + Convert.ToBase64String(_rsa.RSAParams.InverseQ) + "\n");
                richTextBoxRSAparams.AppendText("Modulus: " + Convert.ToBase64String(_rsa.RSAParams.Modulus) + "\n");
                richTextBoxRSAparams.AppendText("P: " + Convert.ToBase64String(_rsa.RSAParams.P) + "\n");
                richTextBoxRSAparams.AppendText("Q: " + Convert.ToBase64String(_rsa.RSAParams.Q) + "\n");

                br.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong " + ex.Message);
            }
        }

        private void Decrypt_Click(object sender, EventArgs e)
        {
            if (_rsa.EncryptedData == null)
            {
                MessageBox.Show("Enter file to encrypt and make Encrypt operation first.");
                return;
            }
            try
                {
                    richTextBoxRSAparams.Clear();
                    BinaryReader br = new BinaryReader(File.OpenRead(textBoxFilePath.Text));

                    int blockSize = (KeySizeRSA.Value - 384) / 8 + 37;
                    byte[] chunk;
                    chunk = br.ReadBytes(blockSize);
                    while (chunk.Length > 0)
                    {
                        _rsa.Encrypt(chunk, KeySizeRSA.Value);
                        _sw.Start();
                        _rsa.Decrypt(KeySizeRSA.Value);
                        _sw.Stop();
                        chunk = br.ReadBytes(blockSize);
                    }

                    TimeSpan ts = _sw.Elapsed;
                    _sw.Reset();

                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}\n",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    richTextBoxLog.AppendText("\nRSA Decrypting time:\n");
                    richTextBoxLog.AppendText(elapsedTime);
                    richTextBoxLog.AppendText("\n");
                    richTextBoxLog.AppendText("Last chunk of Decrypted data:\n" + Encoding.Default.GetString(_rsa.DecryptedData) + "\n");
                    richTextBoxLog.AppendText("Last chunk of Original data:\n" + Encoding.Default.GetString(_rsa.OriginalData));
                    richTextBoxRSAparams.AppendText("D: " + Convert.ToBase64String(_rsa.RSAParams.D) + "\n");
                    richTextBoxRSAparams.AppendText("DP: " + Convert.ToBase64String(_rsa.RSAParams.DP) + "\n");
                    richTextBoxRSAparams.AppendText("DQ: " + Convert.ToBase64String(_rsa.RSAParams.DQ) + "\n");
                    richTextBoxRSAparams.AppendText("Exponent: " + Convert.ToBase64String(_rsa.RSAParams.Exponent) + "\n");
                    richTextBoxRSAparams.AppendText("InverseQ: " + Convert.ToBase64String(_rsa.RSAParams.InverseQ) + "\n");
                    richTextBoxRSAparams.AppendText("Modulus: " + Convert.ToBase64String(_rsa.RSAParams.Modulus) + "\n");
                    richTextBoxRSAparams.AppendText("P: " + Convert.ToBase64String(_rsa.RSAParams.P) + "\n");
                    richTextBoxRSAparams.AppendText("Q: " + Convert.ToBase64String(_rsa.RSAParams.Q) + "\n");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong " + ex.Message);
                }
            
        }

        private void ClearLog_Click(object sender, EventArgs e)
        {
            richTextBoxLog.Clear();
            richTextBoxRSAparams.Clear();
            richTextBoxECCParams.Clear();
        }

        private void Verify_Click(object sender, EventArgs e)
        {

            try
            {
                if (_rsa.SignedData == null)
                {
                    MessageBox.Show("Data wasn`t signed yet! Try to sign data first.");
                    return;
                }
                _sw.Start();
                bool verified = _rsa.VerifySignedHash(_rsa.OriginalData, _rsa.SignedData, _keyDigitalSignature, KeySizeRSA.Value);
                _sw.Stop();
                if (verified)
                {
                    TimeSpan ts = _sw.Elapsed;
                    _sw.Reset();
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    richTextBoxLog.AppendText("\nThe DS was verified. " +
                                              "\nTime of verification is " + elapsedTime + "\n");
                }
                else
                {
                    richTextBoxLog.AppendText("The DS does not match to the signature." + "\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Sign_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxFilePath.Text))
            {
                MessageBox.Show("First please enter file to encrypt.");
                return;
            }
            richTextBoxRSAparams.Clear();
            // Create a UnicodeEncoder to convert between byte array and string.
            var byteConverter = new ASCIIEncoding();
            var sr = new StreamReader(textBoxFilePath.Text);

            // Create byte arrays to hold original, encrypted, and decrypted data.
            byte[] originalData = byteConverter.GetBytes(sr.ReadToEnd());

            // Create a new instance of the RSACryptoServiceProvider class 
            // and automatically create a new key-pair.
            var rsaAlg = new RSACryptoServiceProvider(KeySizeRSA.Value);

            // Export the key information to an RSAParameters object.
            // You must pass true to export the private key for signing.
            // No need to export the private key
            // for verification.
            _keyDigitalSignature = rsaAlg.ExportParameters(true);

            _sw.Start();
            // Hash and sign the data.
            var signedData = _rsa.HashAndSignBytes(originalData, _keyDigitalSignature, KeySizeRSA.Value);
            _sw.Stop();
            TimeSpan ts = _sw.Elapsed;
            _sw.Reset();
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            richTextBoxLog.AppendText("RSA Signature Time : " + elapsedTime + "\n");
            richTextBoxLog.AppendText("RSA Signature: " + Encoding.Default.GetString(signedData) + "\n");

            richTextBoxRSAparams.AppendText("D: " + Convert.ToBase64String(_rsa.RSAParams.D) + "\n");
            richTextBoxRSAparams.AppendText("DP: " + Convert.ToBase64String(_rsa.RSAParams.DP) + "\n");
            richTextBoxRSAparams.AppendText("DQ: " + Convert.ToBase64String(_rsa.RSAParams.DQ) + "\n");
            richTextBoxRSAparams.AppendText("Exponent: " + Convert.ToBase64String(_rsa.RSAParams.Exponent) + "\n");
            richTextBoxRSAparams.AppendText("InverseQ: " + Convert.ToBase64String(_rsa.RSAParams.InverseQ) + "\n");
            richTextBoxRSAparams.AppendText("Modulus: " + Convert.ToBase64String(_rsa.RSAParams.Modulus) + "\n");
            richTextBoxRSAparams.AppendText("P: " + Convert.ToBase64String(_rsa.RSAParams.P) + "\n");
            richTextBoxRSAparams.AppendText("Q: " + Convert.ToBase64String(_rsa.RSAParams.Q) + "\n");

        }

        private void EncryptECC_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxFilePath.Text))
            {
                MessageBox.Show("First please enter file to encrypt.");
                return;
            }
            try
            {
                richTextBoxECCParams.Clear();
                var br = new BinaryReader(File.OpenRead(textBoxFilePath.Text));
                var chunk = br.ReadBytes(EccBlockSize);
                while (chunk.Length > 0)
                {
                    _sw.Start();
                    _ecc.Encrypt(chunk, Convert.ToInt32(comboBoxECCKeySize.Text));
                    _sw.Stop();
                    chunk = br.ReadBytes(EccBlockSize);
                }

                TimeSpan ts = _sw.Elapsed;
                _sw.Reset();

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds);

                richTextBoxLog.AppendText("\nECDH Encrypting time: " + elapsedTime + "\n");
                richTextBoxLog.AppendText("\nECDH last chunk of encrypted data:\n" + Encoding.Default.GetString(_ecc.EncryptedData) + "\n\n");

                br.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something goes wrong " + ex.Message);
            }

        }

        private void DecryptECC_Click(object sender, EventArgs e)
        {
            if (_ecc.EncryptedData == null)
            {
                MessageBox.Show("Enter file to encrypt and make Encrypt operation first.");
                return;
            }
            try
            {
                richTextBoxECCParams.Clear();
                var br = new BinaryReader(File.OpenRead(textBoxFilePath.Text));
                var chunk = br.ReadBytes(EccBlockSize);
                while (chunk.Length > 0)
                {
                    _ecc.Encrypt(chunk, Convert.ToInt32(comboBoxECCKeySize.Text));
                    _sw.Start();
                    _ecc.Decrypt(_ecc.EncryptedData, _ecc.IV);
                    _sw.Stop();
                    chunk = br.ReadBytes(EccBlockSize);
                }

                TimeSpan ts = _sw.Elapsed;
                _sw.Reset();

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds);

                richTextBoxLog.AppendText("\nECDH: Decrypting time:" + elapsedTime + "\n");
                richTextBoxLog.AppendText("\nECDH: The last chunk of decrypted data:" + Encoding.Default.GetString(_ecc.DencryptedData));
                richTextBoxLog.AppendText("\nECDH: The last chunk of original data was:" + Encoding.Default.GetString(_ecc.OriginalData));
                br.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }
        }

        private void SignECC_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxFilePath.Text))
            {
                MessageBox.Show("First please enter file to encrypt.");
                return;
            }
            
            richTextBoxECCParams.Clear();
            try
            {
                // Create a UnicodeEncoder to convert between byte array and string.
                var byteConverter = new ASCIIEncoding();
                var sr = new StreamReader(textBoxFilePath.Text);
                _sw.Start();
                _ecc.Sign(byteConverter.GetBytes(sr.ReadToEnd()), Convert.ToInt32(comboBoxECCKeySize.Text));
                _sw.Stop();
                TimeSpan ts = _sw.Elapsed;
                _sw.Reset();
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

                richTextBoxLog.AppendText("\nECC Signature Time : " + elapsedTime + "\n");
                richTextBoxLog.AppendText("\nECC Signature: " + Encoding.Default.GetString(_ecc.Signature) + "\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void VirifyECC_Click(object sender, EventArgs e)
        {
            if (_ecc.Signature == null)
            {
                MessageBox.Show("Data wasn`t signed yet! Try to sign data first.");
                return;
            }
            try
            {
                _sw.Reset();
                _sw.Start();
                bool verified = _ecc.Verify(_ecc.OriginalData, _ecc.Signature);
                _sw.Stop();
                if (verified)
                {
                    TimeSpan ts = _sw.Elapsed;
                    _sw.Reset();
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                    richTextBoxLog.AppendText("\nThe DS was verified. Time - " + elapsedTime + '\n');
                }
                else
                {
                    richTextBoxLog.AppendText("\nThe DS does not match the signature.\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
