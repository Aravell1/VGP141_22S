using System.Collections.Generic;
using System.Text;

namespace VGP141.DataStructures
{
    public class Queue<T>
	{
		private LinkedList<T> _data;

		public Queue()
        {
			_data = new LinkedList<T>();
        }

		public void Enqueue(T value)
		{
			_data.AddLast(value);
		}

		public void Dequeue()
		{
			_data.RemoveFirst();
		}

		public T Peek()
		{
			return _data.First.Value;
		}

		public bool Empty()
		{
			return _data.Count == 0;
		}

		public uint Size()
		{
			return (uint)_data.Count;
		}

		public void Clear()
		{
			_data.Clear();
		}

        public override string ToString()
        {
			StringBuilder sb = new StringBuilder();

			if (Empty())
            {
				sb.Append("Empty");
            }
			else
            {
                foreach (T item in _data)
                {
					sb.Append(item.ToString());
					sb.Append(" ");
                }
            }

            return sb.ToString();
        }
    }
}