using Binary_Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace BSTVisualization
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		int minval = 0;
		int maxval = 200;
		System.Windows.Threading.DispatcherTimer dispatcherTimer;

		Random rnd = new Random(DateTime.Now.Millisecond);
		BinaryTree<int> bst;

		public MainWindow()
		{
			InitializeComponent();
			dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
			dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
			dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(speedSlider.Value));
		}

		private void btnDraw_Click(object sender, RoutedEventArgs e)
		{
			int cnt = Convert.ToInt32(countSlider.Value);
			canvas1.Children.Clear();
			
			bst = new BinaryTree<int>();
			int[] array = new int[cnt];
			if (useArrayChBox.IsChecked != true)
			{
				textArray.Text = "";
				for (int i = 0; i < cnt; i++)
				{
					array[i] = rnd.Next(minval, maxval);
					bst.Add(array[i]);
					textArray.Text += array[i].ToString() + ", ";
				}
				textArray.Text = textArray.Text.Substring(0, textArray.Text.Length - 2);
			}
			else if (useArrayChBox.IsChecked == true)
			{
				
				try
				{
					string[] str_arr = textArray.Text.Split(',');
					for (int i = 0; i < str_arr.Count(); i++)
					{
						array[i] = Convert.ToInt32(str_arr[i].Trim(' '));
						bst.Add(array[i]);
					}
				}
				catch { }
			}
			else return;
			
			Print(canvas1, bst.Root, new Point(canvas1.Width / 2, 0.03 * canvas1.Height), 200, new Point(canvas1.Width / 2, 0.03 * canvas1.Height));
			//DrawBinaryTree(canvas1, 0, new Point(canvas1.Width / 2, 0.03 * canvas1.Height), 0.1 * canvas1.Width, 100);
		}

		//private void DrawBinaryTree(Canvas canvas, int depth, Point pt, double length, double Fi)
		//{
		//	SolidColorBrush ellipseSolidColorBrush = new SolidColorBrush();
		//	Ellipse ellipse = new Ellipse();
		//	ellipseSolidColorBrush.Color = Color.FromArgb(100, 255, 255, 0);
		//	ellipse.Fill = ellipseSolidColorBrush;
		//	ellipse.StrokeThickness = 1;
		//	ellipse.Stroke = Brushes.Black;
		//	ellipse.Width = 0.01 * canvas1.Width;
		//	ellipse.Height = 0.01 * canvas1.Width;
		//	double left = pt.X - (ellipse.Width / 2);
		//	double top = pt.Y - (ellipse.Height / 2);
		//	ellipse.Margin = new Thickness(left, top, 0, 0);
		//	Grid grid = new Grid();
		//	TextBlock tb = new TextBlock();
		//	tb.FontSize = 6;
		//	tb.Margin = new Thickness(left, top, 0, 0);			
		//	tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
		//	tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;

		//	tb.Text = "1";

		//	grid.Children.Add(tb);
		//	grid.Children.Add(ellipse);
		//	canvas.Children.Add(grid);

		//	double new_fi = Fi * 0.5;

		//	if (depth < 6)
		//	{
		//		DrawBinaryTree(canvas, depth + 1, new Point(pt.X - 5*length * Math.Abs(Math.Sin(Fi)), pt.Y + Math.Abs(length * Math.Cos(Fi))), length, new_fi);
		//		DrawBinaryTree(canvas, depth + 1, new Point(pt.X + 5*length * Math.Abs(Math.Sin(Fi)), pt.Y + Math.Abs(length * Math.Cos(Fi))), length, new_fi);
		//	}
		//	else
		//		return;
		//}

		private void Print(Canvas canvas,BinaryTreeNode<int> current, Point p, double ungle, Point previousPoint)
		{
			if (current != null)
			{	
				SolidColorBrush ellipseSolidColorBrush = new SolidColorBrush();
				SolidColorBrush textBrush = new SolidColorBrush();
				Ellipse ellipse = new Ellipse();
				Random rnd = new Random();
				byte r, g, b;
				Line line = new Line();
				double lenght = canvas.Width * 0.09;

				r = Convert.ToByte(rnd.Next(0, 255));
				g = Convert.ToByte(rnd.Next(0, 255));
				b = Convert.ToByte(rnd.Next(0, 255));

				ellipseSolidColorBrush.Color = Color.FromArgb(155, r,g,b);
				ellipse.Fill = ellipseSolidColorBrush;
				ellipse.StrokeThickness = 1;
				ellipse.Width = 0.02 * canvas.Width;
				ellipse.Height = 0.02 * canvas.Width;

				double left = p.X - (ellipse.Width / 2);
				double top = p.Y - (ellipse.Height / 2);

				ellipse.Margin = new Thickness(left, top, 0, 0);
				Grid grid = new Grid();
				TextBlock tb = new TextBlock();
				tb.FontSize = 6;
				tb.ToolTip = current.Value;

				textBrush.Color = Color.FromRgb(0, 0, 0);
				tb.Foreground = textBrush;
				tb.Margin = new Thickness(left, top, 0, 0);
				tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
				tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;

				tb.Text = current.Value.ToString();

				line.Stroke = Brushes.Blue;
				line.StrokeThickness = 0.5;
				line.X1 = previousPoint.X;
				line.Y1 = previousPoint.Y;
				line.X2 = p.X;
				line.Y2 = p.Y;
						
				grid.Children.Add(ellipse);
				grid.Children.Add(tb);

				canvas.Children.Add(line);
				canvas.Children.Add(grid);
				double newUngle = ((ungle / 2 ) >= 2 )? ungle / 2 : ungle;
				// Recursively print the left and right children				
				Print(canvas, current.Left, new Point(p.X - 2 * lenght * Math.Abs(Math.Sin(ungle)), p.Y + 0.5 * lenght * Math.Abs(Math.Cos(ungle))), newUngle, new Point(p.X, p.Y));
				Print(canvas, current.Right, new Point(p.X + 2 * lenght * Math.Abs(Math.Sin(ungle)), p.Y + 0.5 * lenght * Math.Abs(Math.Cos(ungle))), newUngle, new Point(p.X, p.Y));
			}
		}
		
		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
			int cnt = Convert.ToInt32(countSlider.Value);
			canvas1.Children.Clear();
			textArray.Text = "";
			bst = new BinaryTree<int>();
			int[] array = new int[cnt];
			for (int i = 0; i < cnt; i++)
			{
				array[i] = rnd.Next(minval, maxval);
				bst.Add(array[i]);
				textArray.Text += array[i].ToString() + ", ";
			}
			textArray.Text = textArray.Text.Substring(0, textArray.Text.Length - 2);

			Print(canvas1, bst.Root, new Point(canvas1.Width / 2, 0.03 * canvas1.Height), 200, new Point(canvas1.Width / 2, 0.03 * canvas1.Height));
		}

		private void start_btn_Click(object sender, RoutedEventArgs e)
		{
			dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(speedSlider.Value));
			dispatcherTimer.Start();
		}

		private void stop_btn_Click(object sender, RoutedEventArgs e)
		{
			dispatcherTimer.Stop();
		}
	}
}
