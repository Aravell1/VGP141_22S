using System.Collections.Generic;

namespace VGP141.DataStructures
{
	public class DirectedGraph<Data>
	{
		public class Node
		{
			public Node(Data data)
			{
				Data = data;
			}

			public Data Data { get; private set; } = default;
			public List<Node> Incoming { get; private set; } = new List<Node>();
			public List<Node> Outgoing { get; private set; } = new List<Node>();
		};

		private List<Node> nodes = new List<Node>();

		public Node AddNode(Data data)
		{
			Node node = new Node(data);
			nodes.Add(node);
			return node;
		}

		public void AddEdge(Node srcNode, Node dstNode)
		{
			if (srcNode == null || dstNode == null)
			{
				return;
			}

			srcNode.Outgoing.Add(dstNode);
			dstNode.Incoming.Add(srcNode);
		}

		public void AddEdge(Data src, Data dst)
		{
			AddEdge(FindNode(src), FindNode(dst));
		}

		public Node FindNode(Data data)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes[i].Data.Equals(data))
				{
					return nodes[i];
				}
			}

			return null;
		}
	}
}