using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesHandler
{
    public enum ScanType
    {
        Info,
        DuplicationDetection
    }

    public class ScanSettings
    {
        public ScanType ScanType { get;  set; }
        public string RootDirectory { get;  set; }
        public List<string> SupportedExtensions { get;  set; }
        public List<string> FilesToScan { get; set; }
        internal IImageScanManager ScanManager { get; set; }
    }
}
