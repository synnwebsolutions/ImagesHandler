using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesHandler
{
    public static class ImageIdentityHandler
    {
        public static Dictionary<string,string> GenerateIdentity(IdentitySettings settings, string imagePath)
        {
            var data = new Dictionary<string, string>();
            if (File.Exists(imagePath))
            {
                var bytes = File.ReadAllBytes(imagePath);
                var base64Str = Convert.ToBase64String(bytes);
                var identity = $"{base64Str.Substring(0,settings.PreffixDelimeter)}{base64Str.Substring(base64Str.Length - settings.SuffixDelimeter - 1, settings.SuffixDelimeter)}";
                data.Add(imagePath, identity);
            }
            return data;
        }
    }

    public class IdentitySettings
    {
        public int PreffixDelimeter { get;  set; }
        public int SuffixDelimeter { get;  set; }
    }
}
