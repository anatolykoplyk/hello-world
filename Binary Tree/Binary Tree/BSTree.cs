using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Tree
{
	/// <summary>
	/// Узел дерева
	/// </summary>
	/// <typeparam name="T">ываиаи</typeparam>
	public class Node<T>
	{
		// Private member-variables
		private int data;
		private NodeList<T> neighbors = null;//потомки

		public Node() {}//узел
		public Node(int data) : this(data, null) { }
		public Node(int data, NodeList<T> neighbors)
		{
			this.data = data;
			this.neighbors = neighbors;
		}
		/// <summary>
		/// Получает значение узла
		/// </summary>
		public int Value//значение узла
		{
			get
			{
				return data;
			}
			set
			{
				data = value;
			}
		}
		/// <summary>
		/// Получает потомков
		/// </summary>
		protected NodeList<T> Neighbors
		{
			get
			{
				return neighbors;
			}
			set
			{
				neighbors = value;
			}
		}
	}

	public class NodeList<T> : Collection<Node<T>>
	{
		public NodeList() : base() { }

		public NodeList(int initialSize)
		{
			// Add the specified number of items
			for (int i = 0; i < initialSize; i++)
				base.Items.Add(default(Node<T>));
		}

		public Node<T> FindByValue(T value)
		{
			// search the list for the value
			foreach (Node<T> node in Items)
				if (node.Value.Equals(value))
					return node;

			// if we reached here, we didn't find a matching node
			return null;
		}
	}

	public class BinaryTreeNode<T> : Node<T>
	{
		/// <summary>
		/// Конструктор по умолчанию
		/// </summary>
		public BinaryTreeNode() : base() { }
		/// <summary>
		/// Конструктор 2
		/// </summary>
		/// <param name="data">Данные</param>
		public BinaryTreeNode(int data) : base(data, null) { }
		/// <summary>
		/// Конструктор 3
		/// </summary>
		/// <param name="data">Данные</param>
		/// <param name="left">Левый сын</param>
		/// <param name="right">Правый сын</param>
		public BinaryTreeNode(int data, BinaryTreeNode<T> left, BinaryTreeNode<T> right)
		{
			base.Value = data;
			NodeList<T> children = new NodeList<T>(2);
			children[0] = left;
			children[1] = right;

			base.Neighbors = children;
		}
		/// <summary>
		/// Левый сын
		/// </summary>
		public BinaryTreeNode<T> Left
		{
			get
			{
				if (base.Neighbors == null)
					return null;
				else
					return (BinaryTreeNode<T>)base.Neighbors[0];
			}
			set
			{
				if (base.Neighbors == null)
					base.Neighbors = new NodeList<T>(2);

				base.Neighbors[0] = value;
			}
		}
		/// <summary>
		/// Правый сын
		/// </summary>
		public BinaryTreeNode<T> Right
		{
			get
			{
				if (base.Neighbors == null)
					return null;
				else
					return (BinaryTreeNode<T>)base.Neighbors[1];
			}
			set
			{
				if (base.Neighbors == null)
					base.Neighbors = new NodeList<T>(2);

				base.Neighbors[1] = value;
			}
		}
	}

	public class IntComparer : IComparer<int>
	{
		public int Compare(int x, int y)
		{
			return x - y;
		}
	}

	public class BinaryTree<T>
	{
		private BinaryTreeNode<T> root;
		private IComparer<int> comparer = new IntComparer();
		private int count = 0;

		public BinaryTree()
		{

			root = null;
		}

		/// <summary>
		/// Очищает дерево
		/// </summary>
		public virtual void Clear()
		{
			root = null;
		}

		/// <summary>
		/// Получает корень дерева
		/// </summary>
		public BinaryTreeNode<T> Root
		{
			get
			{
				return root;
			}
			set
			{
				root = value;
			}
		}

		/// <summary>
		/// обход в прямом порядке
		/// </summary>
		/// <param name="current">Текущий узел</param>


		/// <summary>
		/// Содержит ли какой то узел дерева передаваемую информацию?
		/// </summary>
		/// <param name="data">Передаваемая инфа</param>
		/// <returns></returns>
		public bool Contains(int data)
		{
			// search the tree for a node that contains data
			BinaryTreeNode<T> current = root;
			int result;
			while (current != null)
			{
				result = comparer.Compare(current.Value, data);
				if (result == 0)
					// we found data
					return true;
				else if (result > 0)
					// current.Value > data, search current's left subtree
					current = current.Left;
				else if (result < 0)
					// current.Value < data, search current's right subtree
					current = current.Right;
			}

			return false;       // didn't find data
		}


		/// <summary>
		/// Вставка нового узла
		/// </summary>
		/// <param name="data">Информация внутри узла</param>
		public virtual void Add(int data)
		{
			// create a new Node instance
			BinaryTreeNode<T> n = new BinaryTreeNode<T>(data);
			int result;

			// now, insert n into the tree
			// trace down the tree until we hit a NULL
			BinaryTreeNode<T> current = root, parent = null;
			while (current != null)
			{
				result = comparer.Compare(current.Value, data);
				if (result == 0)
					// they are equal - attempting to enter a duplicate - do nothing
					return;
				else if (result > 0)
				{
					// current.Value > data, must add n to current's left subtree
					parent = current;
					current = current.Left;
				}
				else if (result < 0)
				{
					// current.Value < data, must add n to current's right subtree
					parent = current;
					current = current.Right;
				}
			}

			// We're ready to add the node!
			count++;
			if (parent == null)
				// the tree was empty, make n the root
				root = n;
			else
			{
				result = comparer.Compare(parent.Value, data);
				if (result > 0)
					// parent.Value > data, therefore n must be added to the left subtree
					parent.Left = n;
				else
					// parent.Value < data, therefore n must be added to the right subtree
					parent.Right = n;
			}
		}
		/// <summary>
		/// Удаление элемента
		/// </summary>
		/// <param name="data">Что удалять</param>
		/// <returns>Удалился или нет</returns>
		public bool Remove(int data)
		{
			// first make sure there exist some items in this tree
			if (root == null)
				return false;       // no items to remove

			// Now, try to find data in the tree
			BinaryTreeNode<T> current = root, parent = null;
			int result = comparer.Compare(current.Value, data);
			while (result != 0)
			{
				if (result > 0)
				{
					// current.Value > data, if data exists it's in the left subtree
					parent = current;
					current = current.Left;
				}
				else if (result < 0)
				{
					// current.Value < data, if data exists it's in the right subtree
					parent = current;
					current = current.Right;
				}

				// If current == null, then we didn't find the item to remove
				if (current == null)
					return false;
				else
					result = comparer.Compare(current.Value, data);
			}

			// At this point, we've found the node to remove
			count--;

			// We now need to "rethread" the tree
			// CASE 1: If current has no right child, then current's left child becomes
			//         the node pointed to by the parent
			if (current.Right == null)
			{
				if (parent == null)
					root = current.Left;
				else
				{
					result = comparer.Compare(parent.Value, current.Value);
					if (result > 0)
						// parent.Value > current.Value, so make current's left child a left child of parent
						parent.Left = current.Left;
					else if (result < 0)
						// parent.Value < current.Value, so make current's left child a right child of parent
						parent.Right = current.Left;
				}
			}
			// CASE 2: If current's right child has no left child, then current's right child
			//         replaces current in the tree
			else if (current.Right.Left == null)
			{
				current.Right.Left = current.Left;

				if (parent == null)
					root = current.Right;
				else
				{
					result = comparer.Compare(parent.Value, current.Value);
					if (result > 0)
						// parent.Value > current.Value, so make current's right child a left child of parent
						parent.Left = current.Right;
					else if (result < 0)
						// parent.Value < current.Value, so make current's right child a right child of parent
						parent.Right = current.Right;
				}
			}
			// CASE 3: If current's right child has a left child, replace current with current's
			//          right child's left-most descendent
			else
			{
				// We first need to find the right node's left-most child
				BinaryTreeNode<T> leftmost = current.Right.Left, lmParent = current.Right;
				while (leftmost.Left != null)
				{
					lmParent = leftmost;
					leftmost = leftmost.Left;
				}

				// the parent's left subtree becomes the leftmost's right subtree
				lmParent.Left = leftmost.Right;

				// assign leftmost's left and right to current's left and right children
				leftmost.Left = current.Left;
				leftmost.Right = current.Right;

				if (parent == null)
					root = leftmost;
				else
				{
					result = comparer.Compare(parent.Value, current.Value);
					if (result > 0)
						// parent.Value > current.Value, so make leftmost a left child of parent
						parent.Left = leftmost;
					else if (result < 0)
						// parent.Value < current.Value, so make leftmost a right child of parent
						parent.Right = leftmost;
				}
			}

			return true;
		}

		public void PreorderTraversal(BinaryTreeNode<T> current)
		{
			if (current != null)
			{
				// Output the value of the current node
				Console.Write(current.Value + " ");
				// Recursively print the left and right children
				PreorderTraversal(current.Left);
				PreorderTraversal(current.Right);
			}
		}

		public void Print(BinaryTreeNode<T> current, int offset,int depth)
		{
			if (current != null)
			{
				Console.SetCursorPosition(offset, depth);
				// Output the value of the current node
				Console.WriteLine(current.Value);
				Console.SetCursorPosition(offset, depth + 1);
				Console.WriteLine("/\\");
				// Recursively print the left and right children				
				Print(current.Left, offset - 3, depth + 2);
				Print(current.Right, offset + 3, depth + 2);
			}
		}

		/// <summary>
		/// Симместричный обход
		/// </summary>
		/// <param name="current">текущий элемент</param>
		public void InorderTraversal(BinaryTreeNode<T> current)
		{
			if (current != null)
			{
				// Visit the left child...
				InorderTraversal(current.Left);

				// Output the value of the current node
				Console.Write(current.Value + " ");

				// Visit the right child...
				InorderTraversal(current.Right);
			}
		}

		/// <summary>
		/// обход в обратном порядке
		/// </summary>
		/// <param name="current">текущий элемент</param>
		public void PostorderTraversal(BinaryTreeNode<T> current)
		{
			if (current != null)
			{
				// Recursively print the left and right children
				PostorderTraversal(current.Left);
				PostorderTraversal(current.Right);

				// Output the value of the current node
				Console.Write(current.Value + " ");
			}
		}
	}
}