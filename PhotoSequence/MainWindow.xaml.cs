using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PhotoSequence
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TrueMain();
        }

        private void TrueMain()
        {
            SaveEachImage();

            Thread show = new Thread(DisplayEachImageLoop);
            show.Start();
        }

        private void SaveEachImage()
        {
            string[] fileArray = Directory.GetFiles(@".\img\");
            frameSpeed = 1000 / fileArray.Length;
            for (int i = 0; i < fileArray.Length; i++)
            using (FileStream fs = new FileStream(fileArray[i], FileMode.Open))
            {
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                fs.Close();
                imageList.Add(ByteToImage(data));
            }
        }

        private void DisplayEachImageLoop()
        {
            while (true)
            foreach (var image in imageList)
            {
                img.Dispatcher.Invoke(() => img.Source = image);
                if (isRandomSpeed)
                {
                    cnt = (cnt < Math.PI) ? cnt + rnd.NextDouble() / 10 : 0;    // (rand / 10) -> 0.0###
                    Thread.Sleep(frameSpeed - Convert.ToInt32(Math.Sin(cnt)) * 5);   // (Sin(0..PI) * 5) -> #.###
                }
                else Thread.Sleep(frameSpeed);
            }
        }

        private readonly bool isRandomSpeed = true;
        private double cnt = 0;
        private int frameSpeed = 0; // 圖片數量決定每秒偵數
        private Random rnd = new Random();
        
        private List<ImageSource> imageList = new List<ImageSource>();

        public static ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }

        private void Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
    }
}
