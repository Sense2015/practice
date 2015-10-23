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
using System.Threading;

//using System.Drawing;
//using EyeXFramework;
//using Tobii.EyeX.Client;

namespace ProjectPractice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    using EyeXFramework;
    using System;
    using Tobii.EyeX.Framework;
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
        Point startPoint;
        List<Point> pointList = new List<Point>();
        Thread dot,dot2;
        double eyex, eyey;
        Ellipse ellipse;
        System.Windows.Threading.DispatcherTimer m_timer;

        private void Canvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(canvas1);
        }
        private void Canvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // 返回指针相对于Canvas的位置
                Point point = e.GetPosition(canvas1);

                if (pointList.Count == 0)
                {
                    // 加入起始点
                    pointList.Add(new Point(this.startPoint.X, this.startPoint.Y));
                }
                else
                {
                    // 加入移动过程中的point
                    pointList.Add(point);
                }

                // 去重复点
                var disList = pointList.Distinct().ToList();
                var count = disList.Count(); // 总点数
                if (point != this.startPoint && this.startPoint != null)
                {
                    var l = new Line();
                    
                    l.Stroke = Brushes.Red;
                    
                    l.StrokeThickness = 1;
                    if (count < 2)
                        return;
                    l.X1 = disList[count - 2].X;  // count-2  保证 line的起始点为点集合中的倒数第二个点。
                    l.Y1 = disList[count - 2].Y;
                    // 终点X,Y 为当前point的X,Y
                    l.X2 = point.X;
                    l.Y2 = point.Y;
                    canvas1.Children.Add(l);
                }
            }
        }
        
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
        private void DrawLine(Canvas c, Point s, Point e)
        {
            /// new a line geometry
            LineGeometry l = new LineGeometry(s, e);

            /// new a path and set its data as the geometry
            System.Windows.Shapes.Path p =
                new System.Windows.Shapes.Path();
            p.Data = l;

            /// add the line as a child of canvas
            c.Children.Add(p);
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //創button
            CreateButton(x, y);
            //Eyetracking();
            var ellipse = new Ellipse
            {
                Width = 10,
                Height = 10,
                Cursor = Cursors.Hand,
                Fill = new SolidColorBrush(Colors.Red),
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 2
            };
            canvas1.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, 10);
            Canvas.SetTop(ellipse, 10);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            canvas1.Children.Clear();
            dot = new Thread(Eyetracking);
            ellipse = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            ellipse.Fill = mySolidColorBrush;
            ellipse.StrokeThickness = 2;
            ellipse.Stroke = Brushes.Black;
            ellipse.Width = 10;
            ellipse.Height = 10;
            canvas1.Children.Add(ellipse);
            dot2 = new Thread(DrawDot);
            dot2.SetApartmentState(ApartmentState.STA);
            dot.Start();

            //dot2.Start();

            m_timer = new System.Windows.Threading.DispatcherTimer();
            m_timer.Tick += new EventHandler(testFunction);
            m_timer.IsEnabled = true;
        }

        
        private void testFunction(object sender,EventArgs e)
        {
            //if (eyex <0) return;
            //Canvas.SetLeft(ellipse,eyex*canvas1.Width/1920);
            //Canvas.SetTop(ellipse, eyey * canvas1.Height / 1080);
            Canvas.SetLeft(ellipse,eyex-800);
            Canvas.SetTop(ellipse, eyey-350);
            Console.WriteLine("x:" + eyex.ToString() + "- y:" + eyey.ToString());
        }
        private void DrawDot()
        {
            //testFunction();
            
            //{
            //    Width = 10,
            //    Height = 10,
            //    Cursor = Cursors.Hand,
            //    Fill = new SolidColorBrush(Colors.Red),
            //    Stroke = new SolidColorBrush(Colors.Black),
            //    StrokeThickness = 2
            //};
            //while (true)
            //{
                
                //this.Content = canvas1;
                //Canvas.SetLeft(ellipse, eyex);
                //Canvas.SetTop(ellipse, eyey);
            //}
            //while (true)
            //{
            //    MessageBox.Show(eyex + "," + eyey);
            //}
            
        }

        private void Eyetracking()
        {
            Console.WriteLine("eyetracking");
            using (var eyeXHost = new EyeXHost())
            {
                eyeXHost.Start();

                using (var lightlyFilteredGazeDataStream = eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered))
                {
                    lightlyFilteredGazeDataStream.Next += (s, e) =>
                    {
                        //Console.WriteLine("Gaze point at ({0:0.0}, {1:0.0}) @{2:0}", e.X, e.Y, e.Timestamp);
                        eyex = e.X;
                        eyey = e.Y;
                        
                        //Point point = new Point(e.LeftEye.X, e.LeftEye.Y);

                        //if (pointList.Count == 0)
                        //{
                        //    // 加入起始点
                        //    pointList.Add(new Point(e.LeftEye.X, e.LeftEye.Y));
                        //}
                        //else
                        //{
                        //    // 加入移动过程中的point
                        //    pointList.Add(point);
                        //}

                        //// 去重复点
                        //var disList = pointList.Distinct().ToList();
                        //var count = 15; // 总点数
                        //if (point != this.startPoint && this.startPoint != null)
                        //{
                        //    var l = new Line();

                        //    l.Stroke = Brushes.Red;

                        //    l.StrokeThickness = 1;
                        //    if (count < 2)
                        //        return;
                        //    l.X1 = disList[count - 2].X;  // count-2  保证 line的起始点为点集合中的倒数第二个点。
                        //    l.Y1 = disList[count - 2].Y;
                        //    // 终点X,Y 为当前point的X,Y
                        //    l.X2 = point.X;
                        //    l.Y2 = point.Y;
                        //    canvas1.Children.Add(l);
                        //}
                    };

                    //Console.SetCursorPosition(0, 12);
                    //Console.WriteLine("");
                    //Console.WriteLine("The 3D position consists of X,Y,Z coordinates expressed in millimeters");
                    //Console.WriteLine("in relation to the center of the screen where the eye tracker is mounted.");
                    //Console.WriteLine("\n");
                    //Console.WriteLine("The normalized coordinates are expressed in relation to the track box,");
                    //Console.WriteLine("i.e. the volume in which the eye tracker is theoretically able to track eyes.");
                    //Console.WriteLine("- (0,0,0) represents the upper, right corner closest to the eye tracker.");
                    //Console.WriteLine("- (1,1,1) represents the lower, left corner furthest away from the eye tracker.");
                    //Console.WriteLine();
                    //Console.WriteLine("---------------------------------------------------------");
                    //Console.WriteLine("Listening for eye position data, press any key to exit...");

                    Console.In.Read();
                }
            }
        }
        [STAThread]
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
