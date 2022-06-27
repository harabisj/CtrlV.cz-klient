using CtrlV2.Data;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CtrlV2
{
    internal delegate void StorageChangedEventHandler();

    internal class StorageManager : IList<ImageData>
    {
        private List<ImageData> images;

        public event StorageChangedEventHandler StorageChanged;

        public StorageManager()
        {
            images = new();
        }

        public int Count => images.Count;

        public bool IsReadOnly => false;

        public int DeleteType
        {
            get => Settings.Default.DeleteType;
            set
            {
                Settings.Default.DeleteType = value;
                Settings.Default.Save();
            }
        }

        public ImageData this[int index]
        {
            get => images[index];
            set
            {
                images[index] = value;
                Save();
            }
        }

        public void Save()
        {
            Settings.Default.UploadedImages = JsonConvert.SerializeObject(images);
            Settings.Default.Save();
            StorageChanged?.Invoke();
        }

        public void LoadImages()
        {
            images = JsonConvert.DeserializeObject<List<ImageData>>(Settings.Default.UploadedImages) ?? new();

            StorageChanged?.Invoke();
        }

        public ImageData GetImage(string link)
        {
            return images.First(x => x.Link == link);
        }

        public int IndexOf(ImageData item)
        {
            return images.IndexOf(item);
        }

        public void Insert(int index, ImageData item)
        {
            images.Insert(index, item);
            Save();
        }

        public void RemoveAt(int index)
        {
            images.RemoveAt(index);
            Save();
        }

        public void Add(ImageData item)
        {
            item.Image = App.API.FetchImage(item).Result;
            images.Add(item);
            Save();
        }

        public void Clear()
        {
            images.Clear();
            Save();
        }

        public bool Contains(ImageData item)
        {
            return images.Contains(item);
        }

        public void CopyTo(ImageData[] array, int arrayIndex)
        {
            images.CopyTo(array, arrayIndex);
        }

        public bool Remove(ImageData item)
        {
            bool flag = images.Remove(item);
            Save();
            return flag;
        }

        public IEnumerator<ImageData> GetEnumerator()
        {
            return images.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return images.GetEnumerator();
        }
    }
}
