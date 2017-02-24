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
        //Create a UnicodeEncoder to convert between byte array and string.
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        RSA rsa = new RSA();
        ECC ecc = new ECC();
        RSAParameters KeyDigitalSignature;
        Stopwatch sw = new Stopwatch();
        Stopwatch sw1 = new Stopwatch();

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
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Title = "Выберите файл для шифрования";
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.FileName = @"C:\Users\User\Desktop\TestFileToEncrypt.txt";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFilePath.Text = "";
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            textBoxFilePath.Text = openFileDialog1.FileName;

                            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
                            //    {
                            //        System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                            //        MessageBox.Show(sr.ReadToEnd());
                            //        sr.Close();
                            //    }
                        }
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
            if (!String.IsNullOrWhiteSpace(textBoxFilePath.Text))
            {
                try
                {                    
                    richTextBoxRSAparams.Clear();                  

                    BinaryReader br = new BinaryReader(File.OpenRead(textBoxFilePath.Text));

                    int block_size = ((KeySizeRSA.Value - 384) / 8) + 37;
                    byte[] chunk;

                    chunk = br.ReadBytes(block_size);
                    while (chunk.Length > 0)
                    {
                        sw.Start();
                        rsa.Encrypt(chunk, KeySizeRSA.Value);
                        sw.Stop();
                        chunk = br.ReadBytes(block_size);
                    }

                    TimeSpan ts = sw.Elapsed;
                    sw.Reset();

                    // Format and display the TimeSpan value.
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);

                    richTextBoxLog.AppendText("RSA Encrypting time:\n");
                    richTextBoxLog.AppendText(elapsedTime);
                    richTextBoxLog.AppendText("\n");
                    richTextBoxLog.AppendText("\nRSA last chunk of encrypted data:\n" + Convert.ToBase64String(rsa.EncryptedData) + "\n\n");

                    richTextBoxRSAparams.AppendText("D: " + Convert.ToBase64String(rsa.RSAParams.D) + "\n");
                    richTextBoxRSAparams.AppendText("DP: " + Convert.ToBase64String(rsa.RSAParams.DP) + "\n");
                    richTextBoxRSAparams.AppendText("DQ: " + Convert.ToBase64String(rsa.RSAParams.DQ) + "\n");
                    richTextBoxRSAparams.AppendText("Exponent: " + Convert.ToBase64String(rsa.RSAParams.Exponent) + "\n");
                    richTextBoxRSAparams.AppendText("InverseQ: " + Convert.ToBase64String(rsa.RSAParams.InverseQ) + "\n");
                    richTextBoxRSAparams.AppendText("Modulus: " + Convert.ToBase64String(rsa.RSAParams.Modulus) + "\n");
                    richTextBoxRSAparams.AppendText("P: " + Convert.ToBase64String(rsa.RSAParams.P) + "\n");
                    richTextBoxRSAparams.AppendText("Q: " + Convert.ToBase64String(rsa.RSAParams.Q) + "\n");

                    br.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something goes wrong " + ex.Message);
                }
            }
            else
            { MessageBox.Show("Enter file to encrypt first."); }
        }

        private void Decrypt_Click(object sender, EventArgs e)
        {
            if (rsa.EncryptedData != null)
            {
                try
                {
                    richTextBoxRSAparams.Clear();
                    BinaryReader br = new BinaryReader(File.OpenRead(textBoxFilePath.Text));

                    int block_size = ((KeySizeRSA.Value - 384) / 8) + 37;
                    byte[] chunk;
                    chunk = br.ReadBytes(block_size);
                    while (chunk.Length > 0)
                    {                        
                        rsa.Encrypt(chunk, KeySizeRSA.Value);
                        sw.Start();
                        rsa.Decrypt(KeySizeRSA.Value);
                        sw.Stop();
                        chunk = br.ReadBytes(block_size);
                    }

                    TimeSpan ts = sw.Elapsed;
                    sw.Reset();

                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    richTextBoxLog.AppendText("\nRSA Decrypting time:\n");
                    richTextBoxLog.AppendText(elapsedTime);
                    richTextBoxLog.AppendText("\n");
                    richTextBoxLog.AppendText("Last chunk of Decrypted data:\n" + Convert.ToBase64String( rsa.DecryptedData) + "\n");
                    richTextBoxLog.AppendText("Last chunk of Original data:\n" + Convert.ToBase64String( rsa.OriginalData));
                    richTextBoxRSAparams.AppendText("D: " + Convert.ToBase64String(rsa.RSAParams.D) + "\n");
                    richTextBoxRSAparams.AppendText("DP: " + Convert.ToBase64String(rsa.RSAParams.DP) + "\n");
                    richTextBoxRSAparams.AppendText("DQ: " + Convert.ToBase64String(rsa.RSAParams.DQ) + "\n");
                    richTextBoxRSAparams.AppendText("Exponent: " + Convert.ToBase64String(rsa.RSAParams.Exponent) + "\n");
                    richTextBoxRSAparams.AppendText("InverseQ: " + Convert.ToBase64String(rsa.RSAParams.InverseQ) + "\n");
                    richTextBoxRSAparams.AppendText("Modulus: " + Convert.ToBase64String(rsa.RSAParams.Modulus) + "\n");
                    richTextBoxRSAparams.AppendText("P: " + Convert.ToBase64String(rsa.RSAParams.P) + "\n");
                    richTextBoxRSAparams.AppendText("Q: " + Convert.ToBase64String(rsa.RSAParams.Q) + "\n");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something goes wrong " + ex.Message);
                }
            }
            else
            { MessageBox.Show("Enter file to encrypt and make Encrypt operation first."); }
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
                if (rsa.SignedData == null)
                {
                    MessageBox.Show("Data wasn`t signed yet! Try to sign data first.");
                    return;
                }
                sw.Start();
                bool verified = rsa.VerifySignedHash(rsa.OriginalData, rsa.SignedData, KeyDigitalSignature, KeySizeRSA.Value);
                sw.Stop();
                if (verified)
                {
                    TimeSpan ts = sw.Elapsed;
                    sw.Reset();
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    richTextBoxLog.AppendText("The data was verified. Time of verification is " + elapsedTime);
                }
                else
                {
                    richTextBoxLog.AppendText("The data does not match the signature.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Sign_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(textBoxFilePath.Text))
                {
                    richTextBoxRSAparams.Clear();

                    // Create a UnicodeEncoder to convert between byte array and string.
                    ASCIIEncoding ByteConverter = new ASCIIEncoding();
                    StreamReader sr = new StreamReader(textBoxFilePath.Text);

                    // Create byte arrays to hold original, encrypted, and decrypted data.

                    byte[] originalData = ByteConverter.GetBytes(sr.ReadToEnd());
                    byte[] signedData;

                    // Create a new instance of the RSACryptoServiceProvider class 
                    // and automatically create a new key-pair.
                    RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(KeySizeRSA.Value);

                    // Export the key information to an RSAParameters object.
                    // You must pass true to export the private key for signing.
                    // However, you do not need to export the private key
                    // for verification.
                    KeyDigitalSignature = RSAalg.ExportParameters(true);

                    sw.Start();
                    // Hash and sign the data.
                    signedData = rsa.HashAndSignBytes(originalData, KeyDigitalSignature, KeySizeRSA.Value);
                    sw.Stop();
                    TimeSpan ts = sw.Elapsed;
                    sw.Reset();
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);

                    richTextBoxLog.AppendText("RSA Signature Time : " + elapsedTime + "\n");
                    richTextBoxLog.AppendText("RSA Signature: " + Convert.ToBase64String(signedData) + "\n");

                    richTextBoxRSAparams.AppendText("D: " + Convert.ToBase64String(rsa.RSAParams.D) + "\n");
                    richTextBoxRSAparams.AppendText("DP: " + Convert.ToBase64String(rsa.RSAParams.DP) + "\n");
                    richTextBoxRSAparams.AppendText("DQ: " + Convert.ToBase64String(rsa.RSAParams.DQ) + "\n");
                    richTextBoxRSAparams.AppendText("Exponent: " + Convert.ToBase64String(rsa.RSAParams.Exponent) + "\n");
                    richTextBoxRSAparams.AppendText("InverseQ: " + Convert.ToBase64String(rsa.RSAParams.InverseQ) + "\n");
                    richTextBoxRSAparams.AppendText("Modulus: " + Convert.ToBase64String(rsa.RSAParams.Modulus) + "\n");
                    richTextBoxRSAparams.AppendText("P: " + Convert.ToBase64String(rsa.RSAParams.P) + "\n");
                    richTextBoxRSAparams.AppendText("Q: " + Convert.ToBase64String(rsa.RSAParams.Q) + "\n");

                }
                else
                { MessageBox.Show("Enter file to Encrypt first."); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EncryptECC_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBoxFilePath.Text))
            {
                try
                {
                    richTextBoxECCParams.Clear();
                    BinaryReader br = new BinaryReader(File.OpenRead(textBoxFilePath.Text));

                    int block_size = 20;
                    byte[] chunk;
                    
                    chunk = br.ReadBytes(block_size);
                    while (chunk.Length > 0)
                    {
                        sw.Start();
                        ecc.Encrypt(chunk, Convert.ToInt32(comboBoxECCKeySize.Text));
                        sw.Stop();
                        sw1.Start();
                        ecc.Decrypt(ecc.EncryptedData, ecc.IV);
                        sw1.Stop();
                        chunk = br.ReadBytes(block_size);
                    }

                    TimeSpan ts = sw.Elapsed;
                    TimeSpan ts1 = sw1.Elapsed;
                    sw.Reset();
                    sw1.Reset();

                    // Format and display the TimeSpan value.
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);

                    richTextBoxLog.AppendText("ECDH Encrypting time:\n");
                    richTextBoxLog.AppendText(elapsedTime);
                    richTextBoxLog.AppendText("ECDH Decrypting time:\n");
                    richTextBoxLog.AppendText(String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts1.Hours, ts1.Minutes, ts1.Seconds,
                        ts1.Milliseconds / 10));
                    richTextBoxLog.AppendText("\n");
                    richTextBoxLog.AppendText("\nECDH last chunk of encrypted data:\n" + Convert.ToBase64String(ecc.EncryptedData) + "\n\n");

                    br.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something goes wrong " + ex.Message);
                }
            }
            else
            { MessageBox.Show("Enter file to encrypt first."); }
        }

        private void DecryptECC_Click(object sender, EventArgs e)
        {
            if (ecc.EncryptedData != null)
            {
                try
                {                  
                    richTextBoxECCParams.Clear();
                    BinaryReader br = new BinaryReader(File.OpenRead(textBoxFilePath.Text));

                    int block_size = 20;
                    byte[] chunk;

                    chunk = br.ReadBytes(block_size);
                    while (chunk.Length > 0)
                    {                        
                        ecc.Encrypt(chunk, Convert.ToInt32(comboBoxECCKeySize.Text));
                        sw.Start();
                        ecc.Decrypt(ecc.EncryptedData,ecc.IV);
                        sw.Stop();
                        chunk = br.ReadBytes(block_size);
                    }

                    TimeSpan ts = sw.Elapsed;
                    sw.Reset();

                    // Format and display the TimeSpan value.
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);

                    richTextBoxLog.AppendText("ECDH Decrypting time:\n");
                    richTextBoxLog.AppendText(elapsedTime);
                    richTextBoxLog.AppendText("\n");
                    richTextBoxLog.AppendText("\nECDH last chunk of decrypted data:\n" + ByteConverter.GetString(ecc.Decrypt(ecc.EncryptedData, ecc.IV)));
                    br.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something goes wrong " + ex.Message);
                }
            }
            else
            { MessageBox.Show("Enter file to encrypt and make Encrypt operation first."); }
        }

        private void SignECC_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBoxFilePath.Text))
            {
                richTextBoxECCParams.Clear();
                try
                {
                    // Create a UnicodeEncoder to convert between byte array and string.
                    ASCIIEncoding ByteConverter = new ASCIIEncoding();
                    StreamReader sr = new StreamReader(textBoxFilePath.Text);
                    sw.Start(); 
                    ecc.Sign(ByteConverter.GetBytes(sr.ReadToEnd()), Convert.ToInt32(comboBoxECCKeySize.Text));
                    sw.Stop();
                    TimeSpan ts = sw.Elapsed;
                    sw.Reset();
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);

                    richTextBoxLog.AppendText("ECC Signature Time : " + elapsedTime + "\n");
                    richTextBoxLog.AppendText("ECC Signature: " + Convert.ToBase64String(ecc.Signature) + "\n");

                }
                catch (Exception ex )
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            { MessageBox.Show("Enter file to Encrypt first."); }
        }

        private void VirifyECC_Click(object sender, EventArgs e)
        {
            try
            {
                // Verify the data and display the result to the 
                // console.
                if (ecc.Signature == null)
                {
                    MessageBox.Show("Data wasn`t signed yet! Try to sign data first.");
                    return;
                }
                sw.Start();
                bool verified = ecc.Verify(ecc.OriginalData,ecc.Signature);
                sw.Stop();
                if (verified)
                {
                    TimeSpan ts = sw.Elapsed;
                    sw.Reset();
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    richTextBoxLog.AppendText("The data was verified. Time - " + elapsedTime);
                }
                else
                {
                    richTextBoxLog.AppendText("The data does not match the signature.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
