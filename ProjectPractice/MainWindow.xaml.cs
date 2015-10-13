using System;
using System.Collections.Generic;
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
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using EyeXFramework;
using Tobii.EyeX.Client;

namespace ProjectPractice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        string line;
        string[] words;
        int x, y,countimg;
        string[] imagePaths;
        string imageFolder;
        ImageBrush berriesBrush;
        Button bt;
        Image[] abcBtn;
        Image ig;
        BitmapImage bi3;
        int temp;
        Image imgh;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void CreateButton(int x, int y)
        {
            //先清除所有canvas上的物件
            canvas1.Children.Clear();
            //決定按鈕大小
            double width = (this.canvas1.ActualWidth - (x + 1) * 5) / x; //按鈕寬
            double height = (this.canvas1.ActualHeight - (y + 1) * 5) / y; //按鈕高
            countimg = 0;
            abcBtn = new Image[x * y];
            //abcBtn= new Button[x*y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    
                    //bt = new Button()
                    //{
                    //    Width = width,
                    //    Height = height,
                    //};

                    //if (countimg < imagePaths.Length)
                    //{
                    //    berriesBrush = new ImageBrush();
                    //    berriesBrush.ImageSource = new BitmapImage(new Uri(@"" + imagePaths[countimg], UriKind.Relative));
                    //    countimg++;
                    //}

                    Image ig = new Image()
                    {
                        Width = width,
                        Height = height,
                    };
                    //if (countimg < imagePaths.Length)
                    //{
                    //    //berriesBrush = new ImageBrush();
                    //    //berriesBrush.ImageSource = new BitmapImage(new Uri(@"" + imagePaths[countimg], UriKind.Relative));
                    //    countimg++;

                    //}
                    //ig.Source = new BitmapImage(new Uri(@"" + imagePaths[countimg], UriKind.Relative));

                    //bt.Background = berriesBrush;
                    //bt.Click += new RoutedEventHandler(this.Button_Click);
                    //bt.MouseEnter += new MouseEventHandler(this.MouseEnterHandler);
                    //bt.MouseLeave += new MouseEventHandler(this.MouseHoverHandler);
                    //bt.Name = "btn" + countimg;

                    //abcBtn[countimg] = bt;
                    Canvas.SetTop(ig, j * height + 5);  //建立間距
                    Canvas.SetLeft(ig, i * width + 5);

                    ig.MouseEnter += new MouseEventHandler(this.MouseEnterHandler);
                    ig.MouseLeave += new MouseEventHandler(this.MouseHoverHandler);
                    
                    canvas1.Children.Add(ig);
                    
                    if(countimg<7)
                    ig.Source = ((Image)TryFindResource("image"+(countimg+1).ToString())).Source;
                    
                    abcBtn[countimg] = ig;
                    countimg++;

                }
            }
            foreach (Image im in abcBtn) im.MouseEnter += new MouseEventHandler(MouseEnterHandler);
        }
        private void Button_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(((Button)sender).Name );
        }
        private void MouseEnterHandler(object sender, System.EventArgs e)
        {
            //switch(( (Button)sender).Name){
            //    case "btn1":
            //        berriesBrush = new ImageBrush();
            //        berriesBrush.ImageSource = new BitmapImage(new Uri(@"C:\Sense2015\ProjectPractice\ProjectPractice\images\backstep_hover.png", UriKind.Relative));
            //        bt.Background = berriesBrush;
            //        break;
            //    case "btn2":
            //        break;
            //}

            imgh = (Image)sender;
            for (int c = 0; c < 7; c++)
            {
                if (imgh == abcBtn[c]) { 
                    temp = c+1;
                }
            }
            imgh.Source = ((Image)TryFindResource("imageh" + temp.ToString())).Source;
            
            //for (int i = 1; i < countimg; i++)
            //{
            //    if(abcBtn[i]==sender){
            //        berriesBrush = new ImageBrush();
            //        berriesBrush.ImageSource = new BitmapImage(new Uri(@"" + imagePaths[i], UriKind.Relative));
            //        abcBtn[i].Background = berriesBrush;
            //    }
            //}

            //bi3 = new BitmapImage();
            //bi3.BeginInit();
            //bi3.UriSource = new Uri(@"C:\Sense2015\ProjectPractice\ProjectPractice\images\backstep.png", UriKind.Relative);
            //bi3.EndInit();

            //ig.Stretch = Stretch.Uniform;
            //ig.Source = bi3;

            //Button but = (Button)sender;
            //MessageBox.Show(but.Name);
            //berriesBrush = new ImageBrush();
            //berriesBrush.ImageSource = new BitmapImage(new Uri(@"C:\Sense2015\ProjectPractice\ProjectPractice\images\backstep_hover.png", UriKind.Relative));
            //but.Background = berriesBrush;
        }
        private void MouseHoverHandler(object sender, System.EventArgs e)
        {
            
            //berriesBrush = new ImageBrush();
            //berriesBrush.ImageSource = new BitmapImage(new Uri(@"C:\Sense2015\ProjectPractice\ProjectPractice\images\backstep.png", UriKind.Relative));
            //bt.Background = berriesBrush;
           
            //bi3 = new BitmapImage();
            //bi3.BeginInit();
            //bi3.UriSource = new Uri(@"C:\Sense2015\ProjectPractice\ProjectPractice\images\backstep.png", UriKind.Relative);
            //bi3.EndInit();
            //ig.Stretch = Stretch.Uniform;
            //ig.Source = bi3;
            imgh.Source = ((Image)TryFindResource("image" + temp.ToString())).Source;
        }
        //// 先將圖檔讀到FileStream, 再轉換為byte array。
        //private static void ToBinaryByFileStream(string imageFile)
        //{
        //    FileStream fs = new FileStream(imageFile, FileMode.Open);
        //    byte[] buffer = new byte[fs.Length];
        //    fs.Read(buffer, 0, buffer.Length);
        //    fs.Close();
        //}

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(""+e.Key); 
            if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //創button
            CreateButton(x, y);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Eyetracking();
        }
        private void Eyetracking()
        {
            Console.WriteLine("eyetracking");
            using (var eyeXHost = new EyeXHost())
            {
                eyeXHost.Start();

                using (var stream = eyeXHost.CreateEyePositionDataStream())
                {
                    stream.Next += (s, e) =>
                    {
                        Console.SetCursorPosition(0, 0);

                        // Output information about the left eye.
                        Console.WriteLine("LEFT EYE");
                        Console.WriteLine("========");
                        Console.WriteLine("3D Position: ({0:0.0}, {1:0.0}, {2:0.0})                   ",
                            e.LeftEye.X, e.LeftEye.Y, e.LeftEye.Z);
                        Console.WriteLine("Normalized : ({0:0.0}, {1:0.0}, {2:0.0})                   ",
                            e.LeftEyeNormalized.X, e.LeftEyeNormalized.Y, e.LeftEyeNormalized.Z);

                        // Output information about the right eye.
                        Console.WriteLine();
                        Console.WriteLine("RIGHT EYE");
                        Console.WriteLine("=========");
                        Console.WriteLine("3D Position: {0:0.0}, {1:0.0}, {2:0.0}                   ",
                            e.RightEye.X, e.RightEye.Y, e.RightEye.Z);
                        Console.WriteLine("Normalized : {0:0.0}, {1:0.0}, {2:0.0}                   ",
                            e.RightEyeNormalized.X, e.RightEyeNormalized.Y, e.RightEyeNormalized.Z);
                    };

                    Console.SetCursorPosition(0, 12);
                    Console.WriteLine("");
                    Console.WriteLine("The 3D position consists of X,Y,Z coordinates expressed in millimeters");
                    Console.WriteLine("in relation to the center of the screen where the eye tracker is mounted.");
                    Console.WriteLine("\n");
                    Console.WriteLine("The normalized coordinates are expressed in relation to the track box,");
                    Console.WriteLine("i.e. the volume in which the eye tracker is theoretically able to track eyes.");
                    Console.WriteLine("- (0,0,0) represents the upper, right corner closest to the eye tracker.");
                    Console.WriteLine("- (1,1,1) represents the lower, left corner furthest away from the eye tracker.");
                    Console.WriteLine();
                    Console.WriteLine("---------------------------------------------------------");
                    Console.WriteLine("Listening for eye position data, press any key to exit...");

                    Console.In.Read();
                }
            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //讀取txt中的兩個值 來決定按鈕數量
            StreamReader sr = new StreamReader(File.OpenRead(@"C:\Sense2015\ProjectPractice\ProjectPractice\formxy.txt"));
            while (!sr.EndOfStream)
            {               // 每次讀取一行，直到檔尾
                line = sr.ReadLine();            // 讀取文字到 line 變數
                words = line.Split(',');        //切割完存到words
            }
            sr.Close();                     // 關閉串流
            x = Convert.ToInt32(words[0]);
            y = Convert.ToInt32(words[1]);

            //讀取資料夾中的圖片存到imagePaths
            imageFolder = @"C:\Sense2015\ProjectPractice\ProjectPractice\images";
            imagePaths = Directory.GetFiles(imageFolder, "*.png");
            if (0 == imagePaths.Length)
            {
                MessageBox.Show("No image found.");
                return;
            }
            
        }

    }
}
