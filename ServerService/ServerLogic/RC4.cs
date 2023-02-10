using System.Linq;

namespace ServerLogic
{
    public class RC4
    {
        private static int range = 16;
        private byte[] S = new byte[range];
        private int x;
        private int y;

        public RC4(byte[] key) => init(key);

        private void init(byte[] key)
        {
            int length = key.Length;
            for (int index = 0; index < range; ++index)
                S[index] = (byte) index;
            int index2 = 0;
            for (int index1 = 0; index1 < range; ++index1)
            {
                index2 = (index2 + S[index1] + key[index1 % length]) % range;
                S.Swap(index1, index2);
            }
        }

        public byte[] Encode(byte[] dataB, int size)
        {
            byte[] array = dataB.Take(size).ToArray();
            byte[] numArray = new byte[array.Length];
            for (int index = 0; index < array.Length; ++index)
                numArray[index] = (byte) (array[index] ^ (uint) keyItem());
            return numArray;
        }

        public byte[] Decode(byte[] dataB, int size) => Encode(dataB, size);

        private byte keyItem()
        {
            x = (x + 1) % range;
            y = (y + S[x]) % range;
            S.Swap(x, y);
            return S[(S[x] + S[y]) % range];
        }
    }
}