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

namespace ProjectPractice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CreateButton(6, 6);
        }


        private void button2_Click(object sender, RoutedEventArgs e)
        {
            CreateButton(9, 9);
        }

        private void CreateButton(int x, int y)
        {



            canvas1.Children.Clear();
            MessageBox.Show("" + this.canvas1.ActualWidth);
            
            double w= (this.canvas1.ActualWidth - (x + 1) * 5) / x;
            MessageBox.Show("" + w);

            double h = (this.canvas1.ActualHeight - (x + 1) * 5) / x;
            MessageBox.Show("" + h);
            
            //四个方向的边距都是5
            double width = (this.canvas1.ActualWidth - (x + 1) * 5) / x;
            double height = (this.canvas1.ActualHeight - (y + 1) * 5) / y;




            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Button bt = new Button()
                    {
                        Width = width,
                        Height = height
                    };
                    MessageBox.Show("" + i+","+ j);
                    Canvas.SetTop(bt, j * height + 5);
                    Canvas.SetLeft(bt, i * width + 5);
                    //这两句很关键。按钮在Canvas中的定位与它自己的Left以及Top不是一个概念


                    canvas1.Children.Add(bt);


                }
            }


        }
    }
}
