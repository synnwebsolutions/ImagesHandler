using ImagesHandler;
using Smach_Core.DataAccess.FileDataBase;
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
            var path = @"C:\Users\smachew.WISMAIN\Desktop\tmp\fdTst";
            FileDatabase db = new FileDatabase { Path = path };
            db.KeyDelimeter = "||";
            var t = new testClass
            {
                lastname = "adela",
                mail = "asfas@",
                name = "smachew",
                phone = "054-5642094",
                website = "google.com"
            };

            db.InsertEntrie(t);
            // requirements
            
            // scan job :
            // detect file
            //Console.ReadLine();
        }

        private static void T1()
        {
            try
            {


                // var get root directory
                // get scan settings : priority folders| delete| move| back up image| supported Extensions| action type { scan and save info| scan duplications}
                // 

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
        }
    }

    public class testClass : IFileDataLoadAble
    {
        public  string name { get; set; }
        public int id { get; set; }

        public string lastname { get; set; }
        public string phone { get; set; }
        public string mail { get; set; }
        public string website { get; set; }

        public List<string> GetInsertFields()
        {
            return new List<string>
            {
                name,
                lastname,
                phone,
                mail,
                website
            };
        }
    }
}
