using ScreenCreatorMC.ResourcesManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScreenCreatorMC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ResourceDataList.Instance.Init();

            ResourceDataListBox.ItemsSource = ResourceDataList.Instance;
        }

        private void Grid_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Link;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        public static readonly string imagePrefix = "images/";

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            try
            {
                (from data in (IList<string>)e.Data.GetData(DataFormats.FileDrop)
                where data != null && data.ToLower().EndsWith(".png")
                select data).ToList().ForEach(image =>
                {
                    FileInfo imageInfo = new(image);

                    CopyFile(image, imagePrefix + imageInfo.Name);
                });

                ResourceDataList.Instance.ReloadResources();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void CopyFile(string o, string t)
        {
            FileInfo file1 = new(o);
            FileInfo file2 = new(t);

            if (file2.Exists)
            {
                file2.Delete();
            }

            file1.CopyTo(t);
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
