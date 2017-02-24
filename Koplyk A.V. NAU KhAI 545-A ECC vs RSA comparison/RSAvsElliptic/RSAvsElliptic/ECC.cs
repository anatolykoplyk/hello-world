using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace RSAvsElliptic
{
    class ECC
    {
        //Create byte arrays to hold encrypted, and decrypted data.
        private byte[] _originalData;
        private byte[] _encryptedData;
        private byte[] _decryptedData;
        private byte[] _signature;
        public byte[] KeyForSign;
        private byte[] _alicePublicKey;
        private byte[] _bobPublicKey;
        private byte[] _bobKey;
        private byte[] _iv;
        public byte[] OriginalData
        {
            get { return _originalData; }
        }

        public byte[] EncryptedData
        {
            get { return _encryptedData; }
        }
        
        public byte[] IV
        {
            get { return _iv; }
        }

        public byte[] Signature
        {
            get { return _signature; }
        }

        public byte[] Encrypt(byte[] dataToEncrypt,int keysize)
        {
            try
            {
                using (var alice = new ECDiffieHellmanCng(keysize))
                {

                    alice.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
                    alice.HashAlgorithm = CngAlgorithm.Sha256;
                    _alicePublicKey = alice.PublicKey.ToByteArray();


                    using (ECDiffieHellmanCng bob = new ECDiffieHellmanCng(keysize))
                    {

                        bob.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
                        bob.HashAlgorithm = CngAlgorithm.Sha256;
                        _bobPublicKey = bob.PublicKey.ToByteArray();
                        _bobKey = bob.DeriveKeyMaterial(CngKey.Import(_alicePublicKey, CngKeyBlobFormat.EccPublicBlob));
                    }

                    CngKey k = CngKey.Import(_bobPublicKey, CngKeyBlobFormat.EccPublicBlob);

                    byte[] aliceKey = alice.DeriveKeyMaterial(k);

                    using (Aes aes = new AesCryptoServiceProvider())
                    {
                        aes.Key = aliceKey;
                        _iv = aes.IV;

                        // Encrypt the message
                        using (var ciphertext = new MemoryStream())
                        {
                            using (var cs = new CryptoStream(ciphertext, aes.CreateEncryptor(), 
                                CryptoStreamMode.Write))
                            {
                                _originalData = dataToEncrypt;
                                byte[] plaintextMessage = dataToEncrypt;
                                cs.Write(plaintextMessage, 0, plaintextMessage.Length);
                                cs.Close();
                                _encryptedData = ciphertext.ToArray();
                                return _encryptedData;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }           
        }

        public byte[] Decrypt(byte[] encryptedMessage, byte[] iv)
        {
            try
            {
                using (Aes aes = new AesCryptoServiceProvider())
                {
                    aes.Key = _bobKey;
                    aes.IV = iv;
                    // Decrypt the message
                    using (var plaintext = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(plaintext, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(encryptedMessage, 0, encryptedMessage.Length);
                            cs.Close();
                            _decryptedData = plaintext.ToArray();
                            return _decryptedData;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public byte[] Sign(byte [] dataToSign, int keysize)
        {
            try
            {
                using (var dsa = new ECDsaCng(keysize))
                {
                    dsa.HashAlgorithm = CngAlgorithm.Sha256;
                    KeyForSign = dsa.Key.Export(CngKeyBlobFormat.EccPublicBlob);
                    _signature = dsa.SignData(dataToSign);
                    _originalData = dataToSign;

                    return _signature;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }        

        public bool Verify(byte[] data, byte[] signature)
        {
            try
            {
                //test of wrong key
                using (var ecsdKey = new ECDsaCng(CngKey.Import(KeyForSign, CngKeyBlobFormat.EccPublicBlob)))                
                {
                    return ecsdKey.VerifyData(data, signature);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false; ;
            }
        }
    }
}
