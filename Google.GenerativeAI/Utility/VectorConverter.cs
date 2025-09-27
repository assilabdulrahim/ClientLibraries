namespace Google.GenerativeAI.Utility
{
    public static class VectorConverter
    {
        public static float[] DeserializeVector(byte[] data)
        {
            var vector = new float[data.Length / sizeof(float)];
            Buffer.BlockCopy(data, 0, vector, 0, data.Length);
            return vector;
        }

        public static byte[] SerializeVector(float[] vector)
        {
            var byteArray = new byte[vector.Length * sizeof(float)];
            Buffer.BlockCopy(vector, 0, byteArray, 0, byteArray.Length);
            return byteArray;
        }
    }
}