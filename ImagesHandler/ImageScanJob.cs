using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesHandler
{
    public static class ImageScanJob
    {
        private static IImageScanManager manager;
        public static void Scan(ScanSettings settings)
        {
            manager = settings.ScanManager;
            if (settings.ScanType == ScanType.DuplicationDetection)
            {
                PerformDuplicationDetection(settings);
            }
            else if (settings.ScanType == ScanType.Info)
            {
                PerformScanInfo(settings);
            }
        }

        private static void PerformScanInfo(ScanSettings settings)
        {
            string rootDirectory = settings.RootDirectory;
            List<string> matchingFiles = GetFiles(settings);
            var imageinfos = matchingFiles.Select(x => new MyImageInfo(x)).ToList();
            foreach (var imageinfo in imageinfos)
                manager.SaveInfo(imageinfo);

        }

    private static List<string> GetFiles(ScanSettings settings)
        {
            string rootDirectory = settings.RootDirectory;
            var files = Directory.GetFiles(rootDirectory, "*.*", SearchOption.AllDirectories).Where(s => settings.SupportedExtensions.Contains(Path.GetExtension(s).ToLower())).ToList();
            return files;
        }

        private static void PerformDuplicationDetection(ScanSettings settings)
        {
            List<string> actionFiles = settings.RootDirectory == null ? settings.FilesToScan : GetFiles(settings);
            foreach (var actionFile in actionFiles)
            {
                var currentImage = new MyImageInfo(actionFile);
                List<string> matchingFiles = manager.GetMatchingFilesToCompare(currentImage);
                foreach (var matchingFile in matchingFiles)
                {
                    var imi = new MyImageInfo(matchingFile);
                    if (ImageCompare.Comapre(currentImage, imi))
                    {
                        manager.SaveMatch(currentImage, imi);
                    }
                }
            }
        }
    }
}
