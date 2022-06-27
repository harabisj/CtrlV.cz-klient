using CtrlV.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlV
{
    class StorageManager
    {
        public static void StoreImage(UploadResponse ur)
        {
            UploadedImages uis = JsonConvert.DeserializeObject<UploadedImages>(
                Properties.Settings.Default.UploadedImages
            );

            uis.images.Add(new UploadedImage(ur));

            Properties.Settings.Default.UploadedImages = JsonConvert.SerializeObject(uis);
            Properties.Settings.Default.Save();
        }

        public static List<UploadedImage> LoadImages()
        {
            return JsonConvert.DeserializeObject<UploadedImages>(
                Properties.Settings.Default.UploadedImages
            ).images;
        }

        public static UploadedImage GetImage(string link)
        {
            return LoadImages()
                .Where(x => x.link == link)
                .First();
        }

        public static void DeleteImage(string link)
        {
            UploadedImages uis = JsonConvert.DeserializeObject<UploadedImages>(
                Properties.Settings.Default.UploadedImages
            );

            uis.images = uis.images
                .Where(x => x.link != link)
                .ToList();

            Properties.Settings.Default.UploadedImages = JsonConvert.SerializeObject(uis);
            Properties.Settings.Default.Save();
        }

        public static void UpdateDeleteType(int deleteType)
        {
            Properties.Settings.Default.DeleteType = deleteType;
            Properties.Settings.Default.Save();
        }

        public static int GetDeleteType()
        {
            return Properties.Settings.Default.DeleteType;
        }
    }
}
