using Newtonsoft.Json;
using System.Windows.Media.Imaging;

#pragma warning disable CS8618
namespace CtrlV2.Data
{
    public class ImageData
    {
        public string Link { get; set; }

        public string Token { get; set; }

        public string Day { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }

        [JsonIgnore]
        public BitmapImage? Image { get; set; }

        public ImageData()
        {

        }

        public ImageData(UploadResponse uploadResponse)
        {
            Link = uploadResponse.Link;
            Token = uploadResponse.Token;

            string[] date = uploadResponse.Time.Split(' ')[0].Split('.');
            Day = date[0];
            Month = date[1];
            Year = date[2];
        }
    }
}
