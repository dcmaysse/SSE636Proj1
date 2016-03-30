using System;
using System.Drawing;
using System.Windows.Forms;

namespace SSE636Proj1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.LockUnlockBitsExample();
        }

        private static void LockUnlockBitsExample()
        {

            // Create a new bitmap.
            Bitmap bmp = new Bitmap("C:\\Users\\PiKa\\Pictures\\Wallpapers\\Untitled3.png");

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);
            Console.WriteLine(bmp.PixelFormat);
            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            const int BYTES_PER_PIXEL = 3;
            byte[] rgbValues = new byte[bmpData.Width * bmpData.Height * BYTES_PER_PIXEL];
            if (bmpData.Stride == bmpData.Width * BYTES_PER_PIXEL)
            {
                System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, rgbValues, 0, rgbValues.Length);
            }
            else
            {
                for (int y = 0; y < bmpData.Height; ++y)
                {
                    IntPtr startOfLine = (IntPtr)((long)bmpData.Scan0 + (bmpData.Stride * y));
                    int dataOffset = y * bmpData.Width * BYTES_PER_PIXEL;
                    System.Runtime.InteropServices.Marshal.Copy(startOfLine, rgbValues, dataOffset, bmpData.Width * BYTES_PER_PIXEL);
                }
            }
            Console.WriteLine(rgbValues.Length);
            for (int i=0; i<rgbValues.Length; i++)
                Console.WriteLine(rgbValues[i]);
            string input = Console.ReadLine();
            bmp.UnlockBits(bmpData);
        }
    }
}
