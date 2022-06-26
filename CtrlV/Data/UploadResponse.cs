using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlV.Data
{
    class UploadResponse
    {
        [JsonProperty]
        public float delay;

        [JsonProperty]
        public string link;

        [JsonProperty]
        public int state;

        [JsonProperty]
        public string time;

        [JsonProperty]
        public string token;
    }
}
