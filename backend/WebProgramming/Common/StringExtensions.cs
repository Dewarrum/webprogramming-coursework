using System.Text;

namespace Common
{
    public static class StringExtensions
    {
        public static byte[] GetBytes(this string self)
        {
            return Encoding.UTF8.GetBytes(self);
        }

        public static bool IsEmpty(this string self)
        {
            if (self is null)
                return true;

            return self.Length == 0;
        }
    }
}