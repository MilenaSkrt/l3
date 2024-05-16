using System;
using System.Diagnostics;
using System.Text;

namespace laba3_TP2
{
    public class ArrayList<T> : BaseList<T> where T : IComparable<T>
    {
        private T[] buffer;
        private int count;
        private int sizeBuffer; // начальная емкость массива

        public ArrayList() //конструктор
        {
            sizeBuffer = 2;
            buffer = new T[sizeBuffer];
            count = 0;
        }

        Stopwatch stopwatch = new Stopwatch();

        private void Expand()
        {
            sizeBuffer = buffer.Length + 2;
            T[] newBuffer = new T[sizeBuffer];
            for (int i = 0; i < buffer.Length; i++)
            {
                newBuffer[i] = buffer[i];
            }
            buffer = newBuffer;
        }

        public override void Add(T item)
        {
            if (count == sizeBuffer)
            {
                Expand();
            }

            buffer[count] = item;
            count++;
            OnListMethod();
        }

        public override void Delete(int position)
        {
            if (position < 0 || position >= count)
            {
                return;
            }

            for (int i = position; i < count - 1; i++)
            {
                buffer[i] = buffer[i + 1];
            }
            count--;
            OnListMethod();
        }

        public override void Insert(int position, T item)
        {
            if (position < 0 || position > count)
            {
                return;
            }

            if (count == sizeBuffer)
            {
                Expand();
            }

            for (int i = count; i > position; i--)
            {
                buffer[i] = buffer[i - 1];
            }
            buffer[position] = item;
            count++;
            OnListMethod();
        }

        public override void Clear()
        {
            buffer = new T[sizeBuffer];
            count = 0;
        }

        public override T this[int index]
        {
            get
            {
                if (index >= count) throw new Exceptions.BadIndexException("Позиция выходит за рамки листа");
                if (index < 0) throw new Exceptions.BadIndexException("Позиция имеет отрицательное значение");
                return buffer[index];
            }

            set
            {
                if (index >= count) throw new Exceptions.BadIndexException("Позиция выходит за рамки листа");
                if (index < 0) throw new Exceptions.BadIndexException("Позиция имеет отрицательное значение");
                buffer[index] = value;
            }
        }

        public override void Print()
        {
            Console.Write("[");
            for (int i = 0; i < count; i++)
            {
                Console.Write(buffer[i] + " ");
            }
            Console.Write("]");
        }

        public override int Count
        {
            get { return count; }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (count >= 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (i == count - 1) sb.Append($"[{buffer[i]}].\n ");
                    else sb.Append($"[{buffer[i]}], ");
                }
                return sb.ToString();
            }
            else
            {
                sb.Append("Нет элементов в array листе");
                return sb.ToString();
            }

        }
        protected override BaseList<T> EmptyClone() => new ArrayList<T>();
    }

}
