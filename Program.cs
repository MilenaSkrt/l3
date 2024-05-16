using System;
class Program
{
    static void Main(string[] args)
    {
         BaseList<string> arrList = new ArrList<string>();
         BaseList<string> chainList = new ChainList<string>();

        // // Добавление элементов
        Console.WriteLine("*** ТЕСТИРОВКА ADD*** \n");
        arrList.Add("1");
        arrList.Add("2");
        arrList.Add("3");

        chainList.Add("1");
        chainList.Add("2");
        chainList.Add("3");

        Console.WriteLine("Список ArrList после добавления элементов:");
        arrList.Print();
        Console.WriteLine("Список ChainList после добавления элементов:");
        chainList.Print();
        Console.WriteLine();

        // // Вставка элемента
         Console.WriteLine("*** ТЕСТИРОВКА Insert***\n");
         arrList.Insert(1, "4");
         chainList.Insert(1, "4");

         Console.WriteLine("Список ArrList после вставки элемента:");
         arrList.Print();
        Console.WriteLine("Список ChainList после вставки элемента:");
       chainList.Print();
        Console.WriteLine();

        // // Удаление элемента
         Console.WriteLine("*** ТЕСТИРОВКА Delete***\n");
        arrList.Delete(2);
         chainList.Delete(2);

         Console.WriteLine("Список ArrList после удаления элемента:");
         arrList.Print();
         Console.WriteLine("Список ChainList после удаления элемента:");
         chainList.Print();
         Console.WriteLine();

        // // Очистка списков
         Console.WriteLine("*** ТЕСТИРОВКА Clear***\n");
         arrList.Clear();
         chainList.Clear();

         Console.WriteLine("Список ArrList после очистки:");
         arrList.Print();
         Console.WriteLine("Список ChainList после очистки:");
         chainList.Print();
         Console.WriteLine();

        // // Проверка на равенство списков
         Console.WriteLine("*** ТЕСТИРОВКА Equals***\n");
         arrList.Add("1");
         arrList.Add("2");
         arrList.Add("3");

         chainList.Add("1");
         chainList.Add("2");
         chainList.Add("3");

         bool areListsEqual = arrList.Equals(chainList);
         Console.WriteLine($"Списки {(areListsEqual ? "одинаковы" : "различны")}.");
         Console.WriteLine();


         Console.WriteLine("*** ТЕСТИРОВКА CHANGE***\n");

         Console.WriteLine($"Количество изменений ArrList: {arrList.ChangeCount}");
         Console.WriteLine($"Количество изменений ChainList: {chainList.ChangeCount}");

         Console.WriteLine();

        // // Проверка метода Assign
         Console.WriteLine("*** ТЕСТИРОВКА Assign***\n");
         BaseList<string> assignedList = new ArrList<string>();

         assignedList.Add("5");
         assignedList.Add("6");

         Console.WriteLine("Исходный список dynamicList:");
         arrList.Print();
         Console.WriteLine("Исходный список assignedList:");
         assignedList.Print();

         arrList.Assign(assignedList);

         Console.WriteLine("Список ArrList после применения метода Assign:");
         arrList.Print();
         Console.WriteLine("Список AssignedList после применения метода Assign:");
         assignedList.Print();
         Console.WriteLine();

        // // Проверка метода AssignTo
         Console.WriteLine("*** ТЕСТИРОВКА AssignTo***\n");
         arrList.Add("8");
         BaseList<string> assignedToList = new ArrList<string>();

         assignedToList.Add("9");
         assignedToList.Add("0");

         Console.WriteLine("Список ArrList после применения метода Assign:");
         arrList.Print();
         Console.WriteLine("Список AssignedToList после применения метода Assign:");
         assignedToList.Print();

         arrList.AssignTo(assignedToList);
         Console.WriteLine();

         Console.WriteLine("Список ArrList после применения метода AssignTo:");
         arrList.Print();
         Console.WriteLine("Список AssignedToList после применения метода AssignTo:");
         assignedToList.Print();
         Console.WriteLine();

        // // Тестирование методов SaveToFile и LoadFromFile
         Console.WriteLine("*** ТЕСТИРОВКА SaveToFile и LoadFromFile ***\n");

         string dynamicListFile = "ArrList.txt";
         string linkedListFile = "ChainList.txt";

         arrList.SaveToFile(dynamicListFile);
         chainList.SaveToFile(linkedListFile);

         arrList.Clear();
         chainList.Clear();

         arrList.LoadFromFile(dynamicListFile);
         chainList.LoadFromFile(linkedListFile);

         Console.WriteLine("Список DynamicList после загрузки из файла:");
         arrList.Print();
         Console.WriteLine("Список ChainList после загрузки из файла:");
         chainList.Print();
         Console.WriteLine();

        // // Тестирование метода ForEach
         Console.WriteLine("*** ТЕСТИРОВКА ForEach ***\n");

         BaseList<string> arrListNew = new ArrList<string>();
         BaseList<string> chainListNew = new ChainList<string>();

         arrListNew .Add("1");
         arrListNew .Add("2");
         arrListNew .Add("3");

         chainListNew.Add("1");
         chainListNew.Add("2");
         chainListNew.Add("3");

         arrListNew .ForEach(arrListNew );
         Console.WriteLine();

         chainListNew.ForEach(chainListNew);
         Console.WriteLine();

        // // Объединяем списки
         Console.WriteLine("*** ТЕСТИРОВКА оператора '+' ***\n");
         BaseList<string> mergedList = arrList + chainList;

        // // Выводим объединенный список
         Console.WriteLine("Объединенный список:");
         mergedList.Print();

         Console.WriteLine("*** ТЕСТИРОВКА метода Sort() для ArrList***\n");
         BaseList<int> numbers = new ArrList<int>();
         numbers.Add(5);
         numbers.Add(2);
         numbers.Add(8);
         numbers.Add(1);

         Console.WriteLine("Исходный список чисел:");
         numbers.Print();

         numbers.Sort();

         Console.WriteLine("Список чисел после сортировки:");
         numbers.Print();

         BaseList<string> strings = new ArrList<string>();
         strings.Add("1");
         strings.Add("2");
         strings.Add("3");
         strings.Add("4");

         Console.WriteLine("Исходный список строк:");
         strings.Print();

         strings.Sort();

         Console.WriteLine("Список строк после сортировки:");
         strings.Print();


         Console.WriteLine("*** ТЕСТИРОВКА метода Sort() для ChainList***\n");
         BaseList<int> numbersList = new ChainList<int>();
         numbersList.Add(5);
         numbersList.Add(2);
         numbersList.Add(8);
         numbersList.Add(1);

         Console.WriteLine("Исходный список чисел:");
         numbersList.Print();

         numbersList.Sort();

         Console.WriteLine("Список чисел после сортировки:");
         numbersList.Print();

         BaseList<string> stringsList = new ChainList<string>();
         stringsList.Add("1");
         stringsList.Add("2");
         stringsList.Add("3");
         stringsList.Add("4");

         Console.WriteLine("Исходный список строк:");
         stringsList.Print();

         stringsList.Sort();

        Console.WriteLine("Список строк после сортировки:");
         stringsList.Print();

         Console.WriteLine("*** ВЫЗОВ МЕТОДА ТЕСТИРОВКИ ***");
         Tester.Test();
        BaseList<int> list1 = new ArrList<int>();
        BaseList<int> list2 = new ArrList<int>();
        list1.Add(1);
        list1.Add(1);
        list1.Add(1);
        list2.Add(2);
        list2.Add(2);
        Console.WriteLine("$***ПРОВЕРКА ОПЕРАТОРА >***");
        if (list1 > list2)
        {
            Console.WriteLine(list1 > list2); // true
            Console.WriteLine("$Кол-во элментов в list1 " + list1.Count);
            Console.WriteLine("$Кол-во элментов в list2 " + list2.Count);
        }
    }
}

internal class Tester
{
    internal static void Test()
    {
        throw new NotImplementedException();
    }
}
//для списков доступны  операции > и < сравниваются по каунтам 
