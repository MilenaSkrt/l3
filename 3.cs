Program:
using System.Diagnostics;

namespace laba3_TP2
{
    class Program
    {
        static void Main(string[] args)
        {
            //оценить быстродействие этих структур какая из структур эффективней array или chain.
            string path = "list.txt";
            BaseList<int> array = new ArrayList<int>();
            BaseList<int> chain = new ChainList<int>();
            Random rand = new Random();

            int array_exception_count = 0;
            int chain_exception_count = 0;
            int array_exception_file_count = 0;
            int chain_exception_file_count = 0;
            int array_event_count = 0;
            int chain_event_count = 0;

            void ArrayHandler()
            {
                array_event_count++;
            }

            void ChainHandler()
            {
                chain_event_count++;
            }

            array.Activated += ArrayHandler;
            chain.Activated += ChainHandler;

            
            for (int i = 0; i < 10000; i++)
            {
                int number = rand.Next(100);
                int position = rand.Next(100);
                int operations = rand.Next(1, 6);

                switch (operations)
                {
                    case 1:
                        array.Add(number);
                        chain.Add(number);
                        break;
                    case 2:
                        try
                        {
                            array.Delete(position);
                        }
                        catch (Exceptions.BadIndexException)
                        {
                            array_exception_count++;
                        }

                        try
                        {
                            chain.Delete(position);
                        }
                        catch (Exceptions.BadIndexException)
                        {
                            chain_exception_count++;
                        }

                        break;

                    case 3:
                        try
                        {
                            array.Insert(position, number);
                        }
                        catch (Exceptions.BadIndexException)
                        {
                            array_exception_count++;
                        }

                        try
                        {
                            chain.Insert(position, number);
                        }
                        catch (Exceptions.BadIndexException)
                        {
                            chain_exception_count++;
                        }
                        break;
                    
                    case 4:
                        try
                        {
                            array[position] = number; 
                        }

                        catch (Exceptions.BadIndexException)
                        {
                            array_exception_count++;
                        }

                        try
                        {
                            chain[position] = number; 
                        }

                        catch (Exceptions.BadIndexException)
                        {
                            chain_exception_count++;
                        }
                        break;
                        //case 5:
                        //    array.Sort();
                        //    chain.Sort();
                        //    break;
                }
            }
            
            
            //array.Add(5);
            //Совпадат ли Array и Chain
            try
            {
                if (array == chain)
                {
                    Console.WriteLine("Array и Chain совпадают.");
                }
                else
                {
                    Console.WriteLine("Array и Chain не совпадают.");
                }
            }
            catch (Exceptions.BadIndexException)
            {
                array_exception_count++;
                chain_exception_count++;
            }

            //Совпадает ли колличество событий в Array и Chain
            try
            {
                if (array_event_count == chain_event_count)
                {
                    Console.WriteLine($"Колличество событий совпадает. Событий в Array: {array_event_count}. Событий в Chain: {chain_event_count}");
                }

                else Console.WriteLine($"Колличество событий не совпадает. Событий в Array: {array_event_count}. Событий в Chain: {chain_event_count}");
            }
            catch (Exceptions.BadIndexException)
            {
                array_exception_count++;
                chain_exception_count++;
            }

            //Совпадает ли колличество исключений в Array и Chain
            try
            {
                if (array_exception_count == chain_exception_count)
                    Console.WriteLine($"Колличество исключений совпадает. Исключений в Array: {array_exception_count}. Исключений в Chain: {chain_exception_count}");
                else
                    Console.WriteLine($"Колличество исключений не совпадает. Исключения в Array: {array_exception_count} \nИсключения в Chain: {chain_exception_count}");
            }
            catch (Exceptions.BadIndexException)
            {
                array_exception_count++;
                chain_exception_count++;
            }

            Console.WriteLine("\nArray:");
            array.Print();
            Console.WriteLine("\nChain:");
            chain.Print();
            BaseList<int> together1 = array + chain;
            BaseList<int> together2 = chain + array;
            Console.WriteLine("\nArray + Chain:");
            together1.Print();
            Console.WriteLine("\nChain + Array:");
            together2.Print();

            //Файловые исключения
            for (int i = 0; i < 1000; i++)
            {
                int ops = rand.Next(2);

                switch (ops)
                {
                    case 0:
                        try
                        {
                            array.LoadFromFile(path);
                        }
                        catch (Exceptions.BadFileException)
                        {
                            array_exception_file_count++;
                        }

                        try
                        {
                            chain.LoadFromFile(path);
                        }
                        catch (Exceptions.BadFileException)
                        {
                            chain_exception_file_count++;
                        }
                        break;

                    case 1:
                        array.SaveToFile(path);
                        chain.SaveToFile(path);
                        break;
                }
            }

            Console.WriteLine($"\n\nКол-во файловых исключений в Array: {array_exception_file_count}");
            Console.WriteLine($"Кол-во файловых исключений в Chain: {chain_exception_file_count}");
            
            array.Time();
            chain.Time();

            Console.WriteLine("\nНажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}


Array:
using System.Diagnostics;
using System.Text;

namespace laba3_TP2
{
    public class ArrayList<T> : BaseList<T> where T : IComparable<T>
    {
        T[] buffer;
        //int count;
        int sizeBuffer; // начальная емкость массива

        public ArrayList() //конструктор
        {
            sizeBuffer = 2;
            buffer = null;
            count = 0;
        }

        Stopwatch stopwatch = new Stopwatch();

        void Expand()
        {
            stopwatch.Start();
            if (buffer == null)
            {
                buffer = new T[sizeBuffer];
            }
            else
            {
                sizeBuffer = buffer.Length + (int)Math.Pow(2, 2);
                T[] newBuffer = new T[sizeBuffer];
                int i = 0;
                foreach (T el in buffer)
                {
                    newBuffer[i++] = el;
                }
                buffer = newBuffer;
            }
            stopwatch.Stop();
        }

        public override void Add(T item)
        {
            stopwatch.Start();
            if (buffer == null)
            {
                buffer = new T[sizeBuffer];
            }
            if (count == sizeBuffer)
            {
                Expand();
            }

            buffer[count] = item;
            count++;
            OnListMethod();
            stopwatch.Stop();
        }

        public override void Delete(int position)
        {
            stopwatch.Start();
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
            stopwatch.Stop();
        }

        public override void Insert(int position, T item)
        {
            stopwatch.Start();
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
            stopwatch.Stop();
        }

        public override void Clear()
        {
            stopwatch.Start();
            buffer = new T[sizeBuffer];
            count = 0;
            stopwatch.Stop();
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

        public override void Time()
        {
            Console.WriteLine("\nВремя выполнения Array:" + stopwatch.ElapsedMilliseconds);
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


Chain:
using System.Diagnostics;
using System.Text;

namespace laba3_TP2
{
    public class ChainList<T> : BaseList<T> where T : IComparable<T>
    {
        public class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }

            public Node(T data) //конструктор
            {
                Data = data;
                Next = null;
            }
        }

        Node head;
        int count;

        Stopwatch stopwatch = new Stopwatch();

        public ChainList()
        {
            head = null;
            count = 0;
        }

        public override void Time()
        {
            Console.WriteLine("Время выполнения Chain:" + stopwatch.ElapsedMilliseconds);
        }

        public Node Find(int position)
        {
            if (position >= count)
                return null;

            int i = 0;
            Node P = head; // бегущий узёл

            while (P != null && i < position)
            {
                P = P.Next;
                i++;
            }

            if (i == position)
                return P;
            else
                return null;
        }

        public override void Add(T item)
        {
            stopwatch.Start();
            if (head == null)
            {
                head = new Node(item);
            }
            else
            {
                Node lastNode = Find(count - 1); //поиск последнего узла в списке
                lastNode.Next = new Node(item);
            }
            count++;
            OnListMethod();
            stopwatch.Stop();
        }

        public override T this[int index]
        {
            get
            {
                if (index < 0)
                {
                    throw new Exceptions.BadIndexException("Позиция имеет отрицательное значение");
                }

                if (index >= count)
                {
                    throw new Exceptions.BadIndexException("Позиция выходит за рамки листа");
                }

                Node current = Find(index);
                return current.Data;
            }
            set
            {
                if (index < 0)
                {
                    throw new Exceptions.BadIndexException("Позиция имеет отрицательное значение");
                }

                if (index >= count)
                {
                    throw new Exceptions.BadIndexException("Позиция выходит за рамки листа");
                }

                Node current = Find(index);
                current.Data = value;
            }
        }

        public override void Insert(int position, T item)
        {
            stopwatch.Start();
            if (position < 0 || position > count)
            {
                return;
            }

            if (position == 0)
            {
                Node newNode = new Node(item);
                newNode.Next = head;
                head = newNode;
            }
            else
            {
                Node previous = Find(position - 1);
                Node newNode = new Node(item);
                newNode.Next = previous.Next;
                previous.Next = newNode;
            }
            count++;
            OnListMethod();
            stopwatch.Stop();
        }

        public override void Delete(int position)
        {
            stopwatch.Start();
            if (position < 0 || position >= count)
            {
                return;
            }

            if (position == 0)
            {
                head = head.Next;
            }
            else
            {
                Node previous = Find(position - 1);
                previous.Next = previous.Next.Next;
            }
            count--;
            OnListMethod();
            stopwatch.Stop();
        }

        public override void Clear()
        {
            stopwatch.Start();
            head = null;
            count = 0;
            stopwatch.Stop();
        }

        public override void Print()
        {
            Node current = head;
            Console.Write("[");
            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Next;
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
            Node cur = head;
            if (cur != null)
            {
                while (cur.Next != null)
                {
                    sb.Append($"[{cur.Data}], ");
                    cur = cur.Next;
                }
                sb.Append($"[{cur.Data}].\n ");
                return sb.ToString();
            }
            else
            {
                sb.Append("Нет элементов в chain листе");
                return sb.ToString();
            }
        }
        //Сортиоовка пузырьком
        //public override void Sort()
        //{
        //    Node P = head;
        //    int n = count;
        //    while (n > 1)
        //    {
        //        Node last = null;
        //        Node current = P;
        //        while (current.Next != null && current.Next != last)
        //        {
        //            if (current.Data > current.Next.Data)
        //            {
        //                int temp = current.Data;
        //                current.Data = current.Next.Data;
        //                current.Next.Data = temp;
        //                last = current;
        //            }
        //            current = current.Next;
        //        }
        //        n--;
        //    }
        //}

        public override void Sort()
        {
            //stopwatch.Start();
            Node P = head;
            int n = count;
            while (n > 1)
            {
                Node last = null;
                Node current = P;
                while (current.Next != null && current.Next != last)
                {
                    if (current.Data.CompareTo(current.Next.Data) > 0)
                    {
                        T temp = current.Data;
                        current.Data = current.Next.Data;
                        current.Next.Data = temp;
                        last = current;
                    }
                    current = current.Next;
                }
                n--;
            }
            //stopwatch.Stop();
        }
        protected override BaseList<T> EmptyClone() => new ChainList<T>();
    }
}


Base:
using System.Collections;
namespace laba3_TP2
{
    public abstract class BaseList<T> : IEnumerable<T> where T : IComparable<T>  // IEnumerable<T> - простой перебор элементов в указанной коллекции
                                                                                 // IComparable<T>- Определяет обобщенный метод сравнения, который реализуется типом значения или классом для создания метода сравнения с целью упорядочения или сортировки экземпляров.
    {
        protected int count;

        public virtual int Count { get { return count; } }

        public abstract void Add(T item);

        public abstract void Insert(int position, T item);

        public abstract void Delete(int position);

        public abstract void Clear();

        public abstract void Time();

        public abstract T this[int index] { get; set; }

        public abstract void Print();

        protected abstract BaseList<T> EmptyClone();

        public delegate void MethodListener();
        public event MethodListener Activated;
        protected void OnListMethod()
        {
            Activated?.Invoke();
        }

        public void Assign(BaseList<T> source) // A = 1, 2    B = 3, 4, 5  A1 = 1,2
        {
            Clear();
            for (int i = 0; i < source.Count; i++)
            {
                Add(source[i]);
            }
        } // A = 3, 4, 5    B = 3, 4, 5

        public void AssignTo(BaseList<T> dest) // A = 1, 2    B = 3, 4, 5
        {
            dest.Assign(this);
        } // A = 1, 2    B = 1, 2

        public BaseList<T> Clone()
        {
            BaseList<T> cloneList = EmptyClone();
            cloneList.Assign(this);
            return cloneList;
        }

        //Быстрая
        private void qSort(int low, int high)
        {
            if (low < high)
            {
                int pi = (low + high) / 2;
                T pivot = this[pi];
                int i = low;
                int j = high;
                while (i <= j)
                {
                    while (this[i].CompareTo(pivot) < 0)
                    {
                        i++;
                    }
                    while (this[j].CompareTo(pivot) > 0)
                    {
                        j--;
                    }
                    if (i <= j)
                    {
                        T temp = this[i];
                        this[i] = this[j];
                        this[j] = temp;
                        i++;
                        j--;
                    }
                }
                if (low < j)
                {
                    qSort(low, j);
                }
                if (i < high)
                {
                    qSort(i, high);
                }
            }
        }

        public virtual void Sort()
        {
            qSort(0, count - 1);
        }

        public bool Equals(BaseList<T> list)
        {
            if (this.Count > 0)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].CompareTo(list[i]) != 0) return false;
                }
                return true;
            }
            else if (this.Count == 0) return true;
            else return false;
        }

        public void SaveToFile(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(this.ToString());
            }
        }

        public void LoadFromFile(string path)
        {
            this.Clear();
            using (StreamReader reader = new StreamReader(path))
            {
                try
                {
                    string list = reader.ReadToEnd();

                    list = list.Replace("[", "").Replace("]", "").Replace(".", "").Replace("\n", "");
                    string[] elems = list.Split(',');
                    foreach (string el in elems)
                    {
                        string trimmed = el.Trim();
                        T conv_el = (T)Convert.ChangeType(trimmed, typeof(T));
                        this.Add(conv_el);
                    }
                }
                catch (FormatException)
                {
                    throw new Exceptions.BadFileException("Неверный формат данных в файле");
                }
                finally
                {
                    this.Clear();
                }
            }
        }

        //Объединение списков (+)
        public static BaseList<T> operator +(BaseList<T> left, BaseList<T> right)
        {
            BaseList<T> together = left.Clone();
            for (int i = 0; i < right.Count; i++)
            {
                together.Add(right[i]);
            }
            return together;
        }

        //Сравнение списков на равенство (==)
        public static bool operator ==(BaseList<T> left, BaseList<T> right)
        {
            if (left.Equals(right)) return true;
            else return false;
        }

        //Сравнение списков на неравенство (!=)
        public static bool operator !=(BaseList<T> left, BaseList<T> right)
        {
            if (left.Equals(right)) return false;
            else return true;
        }
        //IEnumerable просит реализции IEnumerator а в нём move next и тд
        //IEnumerable возвращает оббект для взаимодействия списка внутри for

        IEnumerator IEnumerable.GetEnumerator() //они оба хранят методы которые нужно реадизовать чтлбы реализовать эти интрефейсы
        {
            return GetEnumerator();  
        }

        //Создание перечислителя?
        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnum(this);
        }

        protected class ListEnum : IEnumerator<T>
        {
            private readonly BaseList<T> list;
            private int index;

            public ListEnum(BaseList<T> list)
            {
                this.list = list;
                index = -1;
            }

            public T Current
            {
                get { return list[index]; }
            }

            object IEnumerator.Current { get { return Current; } }
            public bool MoveNext()
            {
                if (index < list.Count - 1)
                {
                    index++;
                    return true;
                }
                else return false;
            }

            public void Reset() { index = -1; } 

            public void Dispose() { }       //нет никаких ресурсов, которые требуется освободить
        }

        //Отсортирован ли?
        //public bool Sorted()
        //{
        //    for (int i = 0; i < this.count - 1; i++)
        //    {
        //        if (this[i] > this[i + 1]) return false;
        //    }
        //    return true;
        //}
    }
}

Exception

namespace laba3_TP2
{
    class Exceptions
    {
        public class BadIndexException : Exception //Для плохого индекса
        {
            public BadIndexException(string message) : base(message)
            {

            }
        }

        public class BadFileException : FormatException //Плохой тип данных в файле
        {
            public BadFileException(string message) : base(message)
            {

            }
        }
    }
}
