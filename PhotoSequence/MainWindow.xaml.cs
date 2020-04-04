using System;
using System.Windows;
using System.Windows.Input;
using Timer = System.Windows.Forms.Timer;

namespace PhotoSequence
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            imageWidgets = new RenderComponent(img0, img1);

            int frameSpeed = 16; // 16.666..
            Timer mainTimer = new Timer { Interval = frameSpeed };
            mainTimer.Tick += MainTimer_Tick;
            mainTimer.Start();
        }

        private RenderComponent imageWidgets;
        private void MainTimer_Tick(object sender, EventArgs e)
        {
            imageWidgets.NextImage();
        }

        private void Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
    }
}
