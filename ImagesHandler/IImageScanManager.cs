using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesHandler
{
    interface IImageScanManager
    {
        void SaveInfo(MyImageInfo imageinfo);
        List<string> GetMatchingFilesToCompare(MyImageInfo ii);
        void SaveMatch(MyImageInfo currentImage, MyImageInfo imi);
    }
}
