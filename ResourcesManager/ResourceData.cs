using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections;
using System.ComponentModel;

namespace ScreenCreatorMC.ResourcesManager
{
    public class ResourceDataList : ObservableCollection<ResourceData>, IEnumerable<ResourceData>
    {
        private static readonly ResourceDataList _instance = new();
        public static ResourceDataList Instance { get { return _instance; } }

        private ResourceDataList()
        {

        }

        public static readonly DirectoryInfo images = new("./images");

        public void Init()
        {
            if (!images.Exists)
            {
                images.Create();
            }

            ReloadResources();
        }

        public void ReloadResources()
        {
            Clear();

            images.GetFiles().ToList().ForEach(file => {
                if (file.Name.ToLower().EndsWith(".png"))
                {
                    Add(new ResourceData(file.Name, new(0, 0)));
                }
            });
        }
    }

    /**
     * 图片的数据，只接受 png 格式
     */
    public class ResourceData
    {
        public string ResourceName { get; set; }
        public string ResourcePath
        {
            get
            {
                return "pack://SiteOfOrigin:,,,/images/" + ResourceName;
            }
        }

        public Point Size { get; set; }

        public ResourceData(string resourceName, Point size)
        {
            ResourceName = resourceName;
            Size = size;
        }
    }
}
