using ImagesHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesHandlerTester
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                
                var path = @"C:\Users\smachew.WISMAIN\Desktop\tmp\testimages";
                string supportedExtensions = "*.jpg,*.gif,*.png,*.bmp,*.jpeg";

                var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower()));
                var mIs = files.Select(x => new MyImageInfo(x)).ToList();
                var currentIdx = 0;
                do
                {
                    for (int i = 0; i < mIs.Count; i++)
                    {
                        var currentMi = mIs[currentIdx];
                        currentMi.ScanImages(mIs.GetRange(currentIdx + 1, mIs.Count - currentIdx - 1));
                    }
                    currentIdx++;
                }
                while (currentIdx < mIs.Count);

                //foreach (var mi in mIs)
                //{
                //    var actives = mIs.Where(x => x.IsUnique != true && x != mi).ToList();
                //    mi.ScanImages(actives);
                //}
                var cnt = mIs.Where(x => x.IsUnique == true).Count();
                Console.WriteLine(cnt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.ReadLine();
        }
    }
}
