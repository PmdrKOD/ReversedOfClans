using System.Security.Cryptography;

namespace ReversedOfClans.CryptoRC4
{
    public class CryptoRc4
    {
        private static readonly byte[] KEY = System.Text.Encoding.UTF8.GetBytes("fhsd6f86f67rt8fw78fw789we78r9789wer6re");
        private static readonly byte[] NONCE = System.Text.Encoding.UTF8.GetBytes("nonce");

        private readonly RC4 rc4Stream;
        private readonly RC4 rc4Stream2;
        private readonly ICryptoTransform encryptor1;
        private readonly ICryptoTransform encryptor2;

        public CryptoRc4()
        {
            try
            {
                byte[] fullKey = Concat(KEY, NONCE);

                rc4Stream = RC4.Create();
                rc4Stream.Key = fullKey;
                encryptor1 = rc4Stream.CreateEncryptor();
                // Пропускаем fullKey через шифр (как в Java update)
                encryptor1.TransformFinalBlock(fullKey, 0, fullKey.Length);

                rc4Stream2 = RC4.Create();
                rc4Stream2.Key = fullKey;
                encryptor2 = rc4Stream2.CreateEncryptor();
                encryptor2.TransformFinalBlock(fullKey, 0, fullKey.Length);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Failed to initialize RC4 cipher", e);
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            return encryptor1.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Encrypt(byte[] data)
        {
            return encryptor2.TransformFinalBlock(data, 0, data.Length);
        }

        private static byte[] Concat(byte[] a, byte[] b)
        {
            byte[] result = new byte[a.Length + b.Length];
            Buffer.BlockCopy(a, 0, result, 0, a.Length);
            Buffer.BlockCopy(b, 0, result, a.Length, b.Length);
            return result;
        }
    }

    // Простая реализация RC4, так как в .NET нет встроенной
    public sealed class RC4 : SymmetricAlgorithm
    {
        public RC4()
        {
            KeySizeValue = 128;
            BlockSizeValue = 8;
            LegalKeySizesValue = new KeySizes[] { new KeySizes(1, 2048, 1) };
            LegalBlockSizesValue = new KeySizes[] { new KeySizes(1, 2048, 1) };
        }

        public static new RC4 Create()
        {
            return new RC4();
        }

        public override ICryptoTransform CreateEncryptor()
        {
            return new RC4Transform(Key);
        }

        public override ICryptoTransform CreateDecryptor()
        {
            return new RC4Transform(Key);
        }

        public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[]? rgbIV)
        {
            return new RC4Transform(rgbKey);
        }

        public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[]? rgbIV)
        {
            return new RC4Transform(rgbKey);
        }

        public override void GenerateKey()
        {
            throw new NotSupportedException();
        }

        public override void GenerateIV()
        {
            throw new NotSupportedException();
        }
    }

    public sealed class RC4Transform : ICryptoTransform
    {
        private readonly byte[] s;
        private int i;
        private int j;

        public RC4Transform(byte[] key)
        {
            s = new byte[256];
            i = 0;
            j = 0;

            // KSA (Key Scheduling Algorithm)
            for (int x = 0; x < 256; x++)
            {
                s[x] = (byte)x;
            }

            int y = 0;
            for (int x = 0; x < 256; x++)
            {
                y = (y + s[x] + key[x % key.Length]) % 256;
                byte temp = s[x];
                s[x] = s[y];
                s[y] = temp;
            }
        }

        public int InputBlockSize => 1;
        public int OutputBlockSize => 1;
        public bool CanTransformMultipleBlocks => false;
        public bool CanReuseTransform => true;

        public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            byte[] result = new byte[inputCount];
            for (int k = 0; k < inputCount; k++)
            {
                i = (i + 1) % 256;
                j = (j + s[i]) % 256;
                byte temp = s[i];
                s[i] = s[j];
                s[j] = temp;
                byte k_byte = (byte)((s[i] + s[j]) % 256);
                result[k] = (byte)(inputBuffer[inputOffset + k] ^ s[k_byte]);
            }
            Buffer.BlockCopy(result, 0, outputBuffer, outputOffset, inputCount);
            return inputCount;
        }

        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            byte[] result = new byte[inputCount];
            TransformBlock(inputBuffer, inputOffset, inputCount, result, 0);
            return result;
        }

        public void Dispose()
        {
        }
    }
}
