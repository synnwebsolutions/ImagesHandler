using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesHandler
{
    public class MyImageInfo
    {
        public string iPath { get; set; }

        public string FileName { get; set; }

        public Bitmap iBitmap { get; set; }

        public int currentDifrences { get; set; }

        public bool? IsUnique { get; set; }

        public MyImageInfo(string path)
        {
            try
            {
                iPath = path;
                iBitmap = new Bitmap(path);
                Clones = new List<MyImageInfo>();
                FileName = Path.GetFileName(path);
            }
            catch (Exception ex)
            {
                var xxc = ex.Message;
            }
        }

        private void Comapre(MyImageInfo other)
        {
            var thisfn = FileName;
            var otherfn = other.FileName;

            bool equals = true;
            Rectangle rect1 = new Rectangle(0, 0, iBitmap.Width, iBitmap.Height);
            Rectangle rect2 = new Rectangle(0, 0, other.iBitmap.Width, other.iBitmap.Height);
            if (rect1.Width == rect2.Width && rect1.Height == rect2.Height)
            {
                BitmapData bmpData1 = iBitmap.LockBits(rect1, ImageLockMode.ReadOnly, iBitmap.PixelFormat);
                BitmapData bmpData2 = other.iBitmap.LockBits(rect1, ImageLockMode.ReadOnly, other.iBitmap.PixelFormat);
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
                iBitmap.UnlockBits(bmpData1);
                other.iBitmap.UnlockBits(bmpData2);

                if (equals)
                {
                    Clones.Add(other);
                    other.Clones.Add(this);
                }
            }
        }

        public void ScanImages(List<MyImageInfo> images)
        {
            foreach (var image in images)
            {
                if (!image.IsUnique.HasValue)
                {
                    Comapre(image);
                }
            }
            if (Clones.Count > 0)
                IsUnique = true;
        }

        public List<MyImageInfo> Clones { get; set; }
    }
}
