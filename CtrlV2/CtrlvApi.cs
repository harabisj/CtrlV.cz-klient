using CtrlV2.Data;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CtrlV2
{
    internal class CtrlvApi : IDisposable
    {
        HttpClient client;

        public CtrlvApi()
        {
            client = new HttpClient();
        }

        public async Task<string> UploadImage(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                StreamContent content = new StreamContent(ms);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/lukyer-binary-paster-data");

                HttpResponseMessage response = await client.PostAsync("https://ctrlv.cz/upload.php", content);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<bool> EditImage(string id, string token, int deleteType)
        {
            return (await client.GetAsync(
                "https://ctrlv.cz/edit.php?" +
                "imgId=" + id +
                "&action=" + "liveliness" +
                "&token=" + token +
                "&deleteType=" + deleteType
            )).IsSuccessStatusCode;
        }

        public async Task<bool> DeleteImage(string id, string token)
        {
            return (await client.GetAsync(
                "https://ctrlv.cz/edit.php?" +
                "imgId=" + id +
                "&action=" + "delete" +
                "&token=" + token
            )).IsSuccessStatusCode;
        }

        public async Task<BitmapImage> FetchImage(ImageData data)
        {
            return await FetchImage(GetImageUrl(data));
        }

        public string GetImageUrl(ImageData data)
        {
            return "https://ctrlv.cz/shots/" +
                data.Year + "/" +
                data.Month + "/" +
                data.Day + "/" +
                data.Link + ".png";
        }

        private async Task<BitmapImage> FetchImage(string url)
        {
            var response = await client.GetAsync(url);

            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.StreamSource = response.Content.ReadAsStream();
            img.EndInit();
            return img;
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
