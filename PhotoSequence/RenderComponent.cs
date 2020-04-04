using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PhotoSequence
{
    public class RenderComponent
    {
        public RenderComponent(Image image0, Image image1)
        {
            list[0] = image0;
            list[1] = image1;
            list[0].Source = ByteToImage(SaveNextImage());
            list[0].Dispatcher.Invoke(() => list[0].Source = ByteToImage(SaveNextImage()));
        }

        public void NextImage()
        {
            list[isA ? 1 : 0].Visibility = Visibility.Collapsed;
            list[isA ? 0 : 1].Visibility = Visibility.Visible;
            list[isA ? 1 : 0].Dispatcher.Invoke(() => list[isA ? 1 : 0].Source = ByteToImage(SaveNextImage()));
            isA = isA ? false : true;
        }

        private byte[] SaveNextImage()
        {
            using (fs = new FileStream(fileArray[index++], FileMode.Open))
            {
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                fs.Close();

                if (index == fileArray.Length) index %= fileArray.Length;

                return data;
            }
        }

        public ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }
        // private string[] fileArray = Directory.GetFiles(@"G:\Dragons\Dragon\00_NewSort\sort\Barlu\ai\Output\Nergigante and Velkhana Animation_LOOP\original_frames");
        private string[] fileArray = Directory.GetFiles(@"./img");
        private FileStream fs = null;
        private Image[] list = new Image[2];
        private int index = 0;
        private bool isA = true;
    }
}
