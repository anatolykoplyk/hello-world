using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Binary_Tree
{
	class Program
	{
		static void Main(string[] args)
		{
			int count = 10;
			int minval = 0;
			int maxval = 200;
			
			Random rnd = new Random(DateTime.Now.Millisecond);
			BinaryTree<int> bst;

			while (true)
			{
				bst = new BinaryTree<int>();
				int[] array = new int[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = rnd.Next(minval, maxval);
					bst.Add(array[i]);
				}

				Console.WriteLine("Source array:\n");
				foreach (var item in array)
				{
					Console.Write(item + " ");
				}
				Console.ReadKey();
				Console.WriteLine("\n\n Inorder Traversal");
				bst.InorderTraversal(bst.Root);
				Console.WriteLine("\n\n Postorder Traversal");
				bst.PostorderTraversal(bst.Root);
				Console.WriteLine("\n\n Preorder Traversal");
				bst.PreorderTraversal(bst.Root);
				Console.ReadKey();
				Console.Clear();
				Console.WriteLine("\n\nTree:");
				bst.Print(bst.Root, 25, 0);
				Console.ReadKey();
				Console.Clear();
			}
		}
	}
}