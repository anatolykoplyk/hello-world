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

namespace BST
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class BinaryTree : Window
	{
		private int II = 0;
		private int i = 0;
		private double lengthScale = 0.75;
		private double deltaTheta = Math.PI / 4;


		public BinaryTree()
		{
			InitializeComponent();
		}

		private void btnStart_Click(object sender, RoutedEventArgs e)
		{
			canvas1.Children.Clear();
			tbLabel.Text = "";
			i = 0;
			II = 1;
			CompositionTarget.Rendering += StartAnimation;
		}

		private void StartAnimation(object sender, EventArgs e)
		{
			i += 1;
			if (i % 60 == 0)
			{
				DrawBinaryTree(canvas1, II , new Point(canvas1.Width / 2 , 0.03 * canvas1.Height),	0.1 * canvas1.Width, 100);
				string str = "Binary Tree - Depth = " +
				II.ToString();
				tbLabel.Text = str;
				II += 1;
				if (II > 7)
				{
					tbLabel.Text = "Binary Tree - Depth = 10. Finished";
					CompositionTarget.Rendering -= StartAnimation;
				}
			}
		}

		private void DrawBinaryTree(Canvas canvas, int depth, Point pt, double length, double Fi)
		{
			SolidColorBrush ellipseSolidColorBrush = new SolidColorBrush();
			Ellipse ellipse = new Ellipse();

			ellipseSolidColorBrush.Color = Color.FromArgb(100, 255, 255, 0);
			ellipse.Fill = ellipseSolidColorBrush;
			ellipse.StrokeThickness = 1;
			ellipse.Stroke = Brushes.Black;

			// Set the width and height of the Ellipse.
			ellipse.Width = 0.03 * canvas1.Width;
			ellipse.Height = 0.03 * canvas1.Width;

			double left = pt.X - (ellipse.Width / 2);
			double top = pt.Y - (ellipse.Height / 2) ;

			ellipse.Margin = new Thickness(left, top, 0, 0);
	
			Grid grid = new Grid();
			TextBlock tb = new TextBlock();
			tb.FontSize = 8;
			tb.Margin = new Thickness(left, top, 0, 0);
			tb.Text = "1";
			tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
			tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;

			grid.Children.Add(tb);
			grid.Children.Add(ellipse);
			canvas.Children.Add(grid);
			double new_fi = Fi * 0.5;
			if (depth > 1)
			{
				DrawBinaryTree(canvas, depth - 1, new Point(pt.X - 4 * length * Math.Abs(Math.Sin(Fi)), pt.Y + length * (1 + Math.Abs(Math.Cos(Fi)))), length, new_fi);
				DrawBinaryTree(canvas, depth - 1, new Point(pt.X + 4 * length * Math.Abs(Math.Sin(Fi)), pt.Y + length * (1 + Math.Abs(Math.Cos(Fi)))), length, new_fi);
			}
			else
				return;
		}

		//private void DrawBinaryTree(Canvas canvas,int depth, Point pt, double length, double theta)
		//{
		//	double x1 = pt.X + length * Math.Cos(theta);
		//	double y1 = pt.Y + length * Math.Sin(theta);
		//	Line line = new Line();
		//	line.Stroke = Brushes.Blue;
		//	line.X1 = pt.X;
		//	line.Y1 = pt.Y;
		//	line.X2 = x1;
		//	line.Y2 = y1;
		//	canvas.Children.Add(line);

		//	SolidColorBrush ellipseSolidColorBrush = new SolidColorBrush();
		//	Ellipse ellipse = new Ellipse();


		//	ellipseSolidColorBrush.Color = Color.FromArgb(100, 255, 255, 0);
		//	ellipse.Fill = ellipseSolidColorBrush;
		//	ellipse.StrokeThickness = 1;
		//	ellipse.Stroke = Brushes.Black;


		//	// Set the width and height of the Ellipse.
		//	ellipse.Width = 0.05 * canvas1.Width;
		//	ellipse.Height = 0.05 * canvas1.Width;
		//	double left = pt.X - (ellipse.Width / 2);
		//	double top = pt.Y - (ellipse.Height / 2);

		//	ellipse.Margin = new Thickness(left, top, 0, 0);

		//	Grid grid = new Grid();
		//	TextBlock tb = new TextBlock();
		//	tb.FontSize = 8;
		//	tb.Margin = new Thickness(left, top, 0, 0);
		//	tb.Text = "1";
		//	tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
		//	tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;

		//	grid.Children.Add(tb);
		//	grid.Children.Add(ellipse);
		//	canvas.Children.Add(grid);
		//	if (depth > 1)
		//	{
		//		DrawBinaryTree(canvas, depth - 1,
		//		new Point(x1, y1),
		//		length * lengthScale, deltaTheta);
		//		DrawBinaryTree(canvas, depth - 1,
		//		new Point(x1, y1),
		//		length * lengthScale, 3 * deltaTheta);
		//	}
		//	else
		//		return;
		//}		
	}
}
