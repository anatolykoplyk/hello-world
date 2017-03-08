using System;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace RSAvsElliptic
{
    class RSA
    {
        private byte[] _dataToEncrypt;
        private byte[] _encryptedData;
        private byte[] _decryptedData;
        private byte[] _signedData;

        private RSACryptoServiceProvider _rsaForDigitalSignature;
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

        public byte[] Encrypt(byte[] message, int keysize)
        {
            try
            {
                //Create a new instance of RSACryptoServiceProvider with specified key size to generate public and private key data.
                using (var rsa = new RSACryptoServiceProvider(keysize))
                {
                    _dataToEncrypt = message;
                    _rsaParams = rsa.ExportParameters(true);
                    _encryptedData = RsaEncrypt(_dataToEncrypt, _rsaParams, false, keysize);
                    return _encryptedData;
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
                return new byte[0];
            }
        }

        public byte[] Decrypt(int keysize)
        {
            try
            {
                _decryptedData = RsaDecrypt(_encryptedData, _rsaParams, false, keysize);
                return _decryptedData;
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
                return new byte[0];
            }
        }


        private byte[] RsaEncrypt(byte[] dataToEncrypt, RSAParameters rsaKeyInfo, bool doOaepPadding, int keysize)
        {
            try
            {
                using (var rsa = new RSACryptoServiceProvider(keysize))
                {
                    rsa.ImportParameters(rsaKeyInfo);
                    _encryptedData = rsa.Encrypt(dataToEncrypt, doOaepPadding);
                }
                return _encryptedData;
            }
            catch (CryptographicException e)
            {
                MessageBox.Show(e.Message);
                return new byte[0];
            }
        }

        private byte[] RsaDecrypt(byte[] dataToDecrypt, RSAParameters rsaKeyInfo, bool doOaepPadding, int keysize)
        {
            try
            {
                using (var rsa = new RSACryptoServiceProvider(keysize))
                {
                    rsa.ImportParameters(rsaKeyInfo);
                    _decryptedData = rsa.Decrypt(dataToDecrypt, doOaepPadding);
                }
                return _decryptedData;
            }
            catch (CryptographicException e)
            {
                MessageBox.Show(e.Message);
                return new byte[0];
            }
        }
        
        public byte[] HashAndSignBytes(byte[] dataToSign, RSAParameters key, int keysize)
        {
            try
            {
                if (_rsaForDigitalSignature == null)
                    _rsaForDigitalSignature = new RSACryptoServiceProvider(keysize);

                // Create a new instance of RSACryptoServiceProvider using the 
                // key from RSAParameters. 

                _dataToEncrypt = dataToSign;
                _rsaForDigitalSignature.ImportParameters(key);
                _rsaParams = key;
                _signedData = _rsaForDigitalSignature.SignData(dataToSign, new SHA1CryptoServiceProvider());
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
                using (var rsaVerify = new RSACryptoServiceProvider(keysize))
                {
                    rsaVerify.ImportParameters(key);
                    return rsaVerify.VerifyData(dataToVerify, new SHA1CryptoServiceProvider(), signedData);
                }
            }
            catch (CryptographicException e)
            {
                MessageBox.Show(e.Message); 
                return false;
            }
        }
    }
}
