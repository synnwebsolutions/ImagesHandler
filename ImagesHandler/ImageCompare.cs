using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesHandler
{
    public static class ImageCompare
    {
        public static bool Comapre(MyImageInfo imageInfo1, MyImageInfo imageInfo2)
        {
            bool equals = true;
            Rectangle rect1 = new Rectangle(0, 0, imageInfo1.iBitmap.Width, imageInfo1.iBitmap.Height);
            Rectangle rect2 = new Rectangle(0, 0, imageInfo2.iBitmap.Width, imageInfo2.iBitmap.Height);
            if (rect1.Width == rect2.Width && rect1.Height == rect2.Height)
            {
                BitmapData bmpData1 = imageInfo1.iBitmap.LockBits(rect1, ImageLockMode.ReadOnly, imageInfo1.iBitmap.PixelFormat);
                BitmapData bmpData2 = imageInfo2.iBitmap.LockBits(rect1, ImageLockMode.ReadOnly, imageInfo2.iBitmap.PixelFormat);
                unsafe
                {
                    byte* ptr1 = (byte*)bmpData1.Scan0.ToPointer();
                    byte* ptr2 = (byte*)bmpData2.Scan0.ToPointer();
                    int width = rect1.Width * 3; // for 24bpp pixel data
                    for (int y = 0; equals && y < rect1.Height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            if (*ptr1 != *ptr2)
                            {
                                equals = false;
                                break;
                            }
                            ptr1++;
                            ptr2++;
                        }
                        ptr1 += bmpData1.Stride - width;
                        ptr2 += bmpData2.Stride - width;
                    }
                }
                imageInfo1.iBitmap.UnlockBits(bmpData1);
                imageInfo2.iBitmap.UnlockBits(bmpData2);
            }
            return equals;
        }
    }
}
