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
        Button bt;
        Button[] abcBtn;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CreateButton(int x, int y)
        {

            canvas1.Children.Clear();
            
            double width = (this.canvas1.ActualWidth - (x + 1) * 5) / x; //按鈕寬
            double height = (this.canvas1.ActualHeight - (y + 1) * 5) / y; //按鈕高
            
            countimg = 0;
            abcBtn= new Button[x*y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    //MessageBox.Show(Width + "," + width + ";" + Height + "," + height);
                    bt = new Button()
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
                    bt.Click += new RoutedEventHandler(this.Button_Click);
                    bt.MouseEnter += new MouseEventHandler(this.MouseEnterHandler);
                    bt.MouseLeave += new MouseEventHandler(this.MouseHoverHandler);
                    
                    bt.Name = "btn" + countimg;
                    
                    abcBtn[countimg] = bt;
                    Canvas.SetTop(bt, j * height + 5);  //建立間距
                    Canvas.SetLeft(bt, i * width + 5);
                    
                    canvas1.Children.Add(bt);
                    
                }
            }
        }
        private void Button_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(( (Button)sender).Name );
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

            for (int i = 1; i < countimg; i++)
            {
                if(abcBtn[i]==sender){
                    berriesBrush = new ImageBrush();
                    berriesBrush.ImageSource = new BitmapImage(new Uri(@"" + imagePaths[i], UriKind.Relative));
                    abcBtn[i].Background = berriesBrush;
                }
            }

            //Button but = (Button)sender;
            //MessageBox.Show(but.Name);
            //berriesBrush = new ImageBrush();
            //berriesBrush.ImageSource = new BitmapImage(new Uri(@"C:\Sense2015\ProjectPractice\ProjectPractice\images\backstep_hover.png", UriKind.Relative));
            //but.Background = berriesBrush;
        }
        private void MouseHoverHandler(object sender, System.EventArgs e)
        {
            Console.Write("  der   ");
            berriesBrush = new ImageBrush();
            berriesBrush.ImageSource = new BitmapImage(new Uri(@"C:\Sense2015\ProjectPractice\ProjectPractice\images\backstep.png", UriKind.Relative));
            bt.Background = berriesBrush;
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
