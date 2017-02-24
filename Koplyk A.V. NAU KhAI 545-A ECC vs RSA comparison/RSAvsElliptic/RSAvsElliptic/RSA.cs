using System;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace RSAvsElliptic
{
    class RSA
    {
        //Create byte arrays to hold original, encrypted, and decrypted data.
        private byte[] _dataToEncrypt;
        private byte[] _encryptedData;
        private byte[] _decryptedData;
        private byte[] _signedData;

        private RSACryptoServiceProvider _rsaForSign;
        private RSAParameters _rsaParams;

        public RSAParameters RSAParams
        {
            get { return _rsaParams; }
        }

        public byte[] EncryptedData
        {
            get { return _encryptedData; }
        }

        public byte[] DecryptedData
        {
            get { return _decryptedData; }
        }

        public byte[] SignedData
        {
            get { return _signedData; }
        }

        public byte[] OriginalData
        {
            get { return _dataToEncrypt; }
        }

        /// <summary>
        /// Encrypts message
        /// </summary>
        /// <param name="message">message to encrypt</param>
        /// <returns>encrypted data</returns>
        public byte[] Encrypt(byte[] message,int keysize)
        {
            try
            {
                //Create a new instance of RSACryptoServiceProvider with specified key size to generate
                //public and private key data.
                using (var rsa = new RSACryptoServiceProvider(keysize))
                {
                    _dataToEncrypt = message;
                    _rsaParams = rsa.ExportParameters(true);
                    //Pass the data to ENCRYPT, the public key information 
                    //(using RSACryptoServiceProvider.ExportParameters(false),
                    //and a boolean flag specifying no OAEP padding.

                    _encryptedData = RsaEncrypt(_dataToEncrypt, _rsaParams, false, keysize);
                    
                    //return encrypted data. 
                    return _encryptedData;
                }
            }
            catch (ArgumentNullException ex)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public byte[] Decrypt(int keysize)
        {
            try
            {
                //Create a new instance of RSACryptoServiceProvider with specified key size to generate
                //public and private key data.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(keysize))
                {
                    //Pass the data to DECRYPT, the private key information 
                    //(using RSACryptoServiceProvider.ExportParameters(true),
                    //and a boolean flag specifying no OAEP padding.

                    _decryptedData = RSADecrypt(_encryptedData, _rsaParams, false, keysize);

                    //return decrypted data. 
                    return _decryptedData;
                }
            }
            catch (ArgumentNullException ex)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                MessageBox.Show(ex.Message);
                return null;
            }
        }


        private byte[] RsaEncrypt(byte[] dataToEncrypt, RSAParameters rsaKeyInfo, bool doOaepPadding, int keysize)
        {
            try
            {
                //Create a new instance of RSACryptoServiceProvider.
                using (var rsa = new RSACryptoServiceProvider(keysize))
                {
                    //Import the RSA Key information. This only needs
                    //to include the public key information.
                    rsa.ImportParameters(rsaKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    _encryptedData = rsa.Encrypt(dataToEncrypt, doOaepPadding);
                }
                return _encryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                MessageBox.Show(e.Message);
                return null;
            }

        }

        private byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding, int keysize)
        {
            try
            {
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(keysize))
                {
                    //Import the RSA Key information. This needs
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    _decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return _decryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                MessageBox.Show(e.Message);
                return null;
            }

        }


        public byte[] HashAndSignBytes(byte[] DataToSign, RSAParameters Key, int keysize)
        {
            try
            {
                if (_rsaForSign == null)
                    _rsaForSign = new RSACryptoServiceProvider(keysize);

                // Create a new instance of RSACryptoServiceProvider using the 
                // key from RSAParameters. 

                _dataToEncrypt = DataToSign;

                _rsaForSign.ImportParameters(Key);
                _rsaParams = Key;
                _signedData = _rsaForSign.SignData(DataToSign, new SHA1CryptoServiceProvider());
                // Hash and sign the data. Pass a new instance of SHA1CryptoServiceProvider
                // to specify the use of SHA1 for hashing.
                return _signedData;
            }
            catch (CryptographicException e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public bool VerifySignedHash(byte[] dataToVerify, byte[] signedData, RSAParameters key, int keysize)
        {
            try
            {
                // Create a new instance of RSACryptoServiceProvider using the 
                // key from RSAParameters.

                var rsaVerify = new RSACryptoServiceProvider(keysize);
                rsaVerify.ImportParameters(key);

                // Verify the data using the signature.  Pass a new instance of SHA1CryptoServiceProvider
                // to specify the use of SHA1 for hashing.
                return rsaVerify.VerifyData(dataToVerify, new SHA1CryptoServiceProvider(), signedData);

            }
            catch (CryptographicException e)
            {
                MessageBox.Show(e.Message); 
                return false;
            }
        }
    
    }
}
