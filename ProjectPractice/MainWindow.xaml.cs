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
        public MainWindow()
        {
            InitializeComponent();

            //讀取config
            //string xmlDataDirectory = System.Configuration.ConfigurationManager.AppSettings.Get("y");
            //System.Configuration.ConfigurationManager.AppSettings("y");
            //string xlDataDirectory = ConfigurationSettings.AppSettings.Get("xmlDataDirectory");
            //string abbb = System.Configuration.ConfigurationSettings.AppSettings.Get("y");
            //string ooo = ConfigurationManager.AppSettings["y"];
            //ConfigurationSettings.AppSettings.Get;

            //讀txt
            StreamReader sr = new StreamReader(File.OpenRead(@"C:\Sense2015\ProjectPractice\ProjectPractice\formxy.txt"));
            while (!sr.EndOfStream) {               // 每次讀取一行，直到檔尾
                line = sr.ReadLine();            // 讀取文字到 line 變數
                words = line.Split(',');        //切割完存到words
            }
            sr.Close();                     // 關閉串流
            
            //建立快捷鍵ESC
            
            InputBinding ib = new InputBinding(ApplicationCommands.Properties, new KeyGesture(Key.Escape));
            this.InputBindings.Add(ib);
            
            CommandBinding cb = new CommandBinding(ApplicationCommands.Properties);
            cb.Executed += new ExecutedRoutedEventHandler(cb_Executed);
            this.CommandBindings.Add(cb);
            
        }
        //聽到ESC之後關程式
        void cb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //public bool CanExecute(object parameter)
        //{
        //    //we can only close Windows
        //    return (parameter is Window);
        //}

        //public event EventHandler CanExecuteChanged;

        //public void Execute(object parameter)
        //{
        //    if (this.CanExecute(parameter))
        //    {
        //        ((Window)parameter).Close();
        //    }
        //}
        

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            x = Convert.ToInt32(words[0]);
            y = Convert.ToInt32(words[1]);
            CreateButton(x, y);
        }

        private void CreateButton(int x, int y)
        {

            canvas1.Children.Clear();
            
            double width = (this.canvas1.ActualWidth - (x + 1) * 5) / x; //按鈕寬
            double height = (this.canvas1.ActualHeight - (y + 1) * 5) / y; //按鈕高
            
            countimg = 0;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Button bt = new Button()
                    {
                        Width = width,
                        Height = height,
                    };

                    if (countimg < imagePaths.Length)
                    {
                        MessageBox.Show("" + imagePaths[countimg]);
                        berriesBrush = new ImageBrush();
                        berriesBrush.ImageSource = new BitmapImage(new Uri(@"" + imagePaths[countimg], UriKind.Relative));
                        countimg++;
                    }
                    
                    bt.Background = berriesBrush;
                    
                    Canvas.SetTop(bt, j * height + 5);  //建立間距
                    Canvas.SetLeft(bt, i * width + 5);
                    
                    canvas1.Children.Add(bt);
                }
            }
        }
        // 先將圖檔讀到FileStream, 再轉換為byte array。
        private static void ToBinaryByFileStream(string imageFile)
        {
            FileStream fs = new FileStream(imageFile, FileMode.Open);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();
        }

        // 先將圖檔讀到Image object, 再轉換為byte array。
        //private static void ToBinaryByImageObj(string imageFile)
        //{
        //    System.Drawing.Image image = System.Drawing.Image.FromFile(imageFile);
        //    MemoryStream mr = new MemoryStream();
        //    image.Save(mr, System.Drawing.Imaging.ImageFormat.Png);
        //    byte[] binaryImage = mr.ToArray();
        //    mr.Dispose();
        //    image.Dispose();
        //}
        //public static void ConvertImageToBinary()

        //將資料夾中的圖片"路徑"存到imagepaths
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("=== Convert the image file to the binary object. ===");

            imageFolder = @"C:\Sense2015\ProjectPractice\ProjectPractice\images";
            // 測試的image數量。
            //int sampleCount = 14;

            imagePaths = Directory.GetFiles(imageFolder, "*.png");
            if (0 == imagePaths.Length)
            {
                MessageBox.Show("No image found.");
                return;
            }

            //Stopwatch sw = new Stopwatch();

            //sw.Reset();
            //sw.Start();
            //for (int i = 0; i < sampleCount; i++)
            //{
            //    ToBinaryByFileStream(imagePaths[i]);
            //}
            //sw.Stop();
            //Console.WriteLine("By FileStream: {0} ms.",sw.ElapsedMilliseconds);

            //sw.Reset();
            //sw.Start();
            ////for (int i = 0; i < sampleCount; i++)
            ////{
            ////    ToBinaryByImageObj(imagePaths[i]);
            ////}
            //sw.Stop();
            //Console.WriteLine("By System.Drawing.Image: {0} ms.",sw.ElapsedMilliseconds);
        }
    }
}
