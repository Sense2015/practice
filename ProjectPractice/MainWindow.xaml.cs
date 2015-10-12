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

           
        }

        private void CreateButton(int x, int y)
        {

            canvas1.Children.Clear();
            MessageBox.Show(""+this.canvas1.ActualHeight);
            double width = (this.canvas1.ActualWidth - (x + 1) * 5) / x; //按鈕寬
            double height = (this.canvas1.ActualHeight - (y + 1) * 5) / y; //按鈕高
            
            countimg = 0;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    //MessageBox.Show(Width + "," + width + ";" + Height + "," + height);
                    Button bt = new Button()
                    {
                        Width = width,
                        Height = height,
                    };

                    if (countimg < imagePaths.Length)
                    {
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

       

        //將資料夾中的圖片"路徑"存到imagepaths
        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    imageFolder = @"C:\Sense2015\ProjectPractice\ProjectPractice\images";
        //    imagePaths = Directory.GetFiles(imageFolder, "*.png");
        //    if (0 == imagePaths.Length)
        //    {
        //        MessageBox.Show("No image found.");
        //        return;
        //    }
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
            MessageBox.Show("b1");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("b2");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StreamReader sr = new StreamReader(File.OpenRead(@"C:\Sense2015\ProjectPractice\ProjectPractice\formxy.txt"));
            while (!sr.EndOfStream)
            {               // 每次讀取一行，直到檔尾
                line = sr.ReadLine();            // 讀取文字到 line 變數
                words = line.Split(',');        //切割完存到words
            }
            sr.Close();                     // 關閉串流

            imageFolder = @"C:\Sense2015\ProjectPractice\ProjectPractice\images";
            imagePaths = Directory.GetFiles(imageFolder, "*.png");
            if (0 == imagePaths.Length)
            {
                MessageBox.Show("No image found.");
                return;
            }
            x = Convert.ToInt32(words[0]);
            y = Convert.ToInt32(words[1]);
            
            CreateButton(x, y);
        }

    }
}
