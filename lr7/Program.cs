using System;                                                
namespace MyProgram                                            
    class Program                                              
    {
        static void Main()                                      
        {
            // Создаем экземпляр класса с помощью конструктора
            Money money = new Money(100, 5);// 100 рублей, 5 купюр
            Console.WriteLine("Исходные данные:");// Подпись
            money.Print();// Печатаем поля
            // 3. Методы
            Console.WriteLine("\n--- Проверка методов ---");// Раздел
            int price = 300;// Стоимость товара
            Console.WriteLine("Хватит ли на товар за {0} руб.? {1}", price, money.CanBuy(price) ? "Да" : "Нет");// Вызов CanBuy
            int itemPrice = 120;// Цена одной штуки
            Console.WriteLine("Сколько штук товара по {0} руб. можно купить? {1}", itemPrice, money.HowManyItems(itemPrice));// Вызов HowManyItems
            // 4. Свойства
            Console.WriteLine("\n--- Проверка свойств ---");// Раздел
            Console.WriteLine("Номинал (через свойство): {0}", money.First);// get свойства First
            Console.WriteLine("Количество (через свойство): {0}", money.Second);// get свойства Second
            Console.WriteLine("Общая сумма (только для чтения): {0}", money.TotalAmount);// get свойства TotalAmount
            // 5. Индексатор
            Console.WriteLine("\n--- Проверка индексатора ---");// Раздел
            Console.WriteLine("money[0] = {0}", money[0]);// money[0] → first
            Console.WriteLine("money[1] = {0}", money[1]);// money[1] → second
            Console.Write("money[2] -> ");// Приглашение
            int errorTest = money[2];// Вызовет сообщение об ошибке (индекс вне диапазона)
            // 6. Перегрузка операторов
            Console.WriteLine("\n--- Проверка перегрузки операторов ---");// Раздел
            // Операция ! (возвращает true, если поле second не нулевое)
            if (!money)// Вызов operator !
                Console.WriteLine("Купюры есть в наличии (!money == true)");// Сообщение
            // Операция ++ (увеличивает оба поля на 1)
            money++;// Вызов operator ++
            Console.WriteLine("\nПосле операции ++:");// Подпись
            money.Print();// Печатаем поля
            // Операция -- (уменьшает оба поля на 1)
            money--;// Вызов operator --
            Console.WriteLine("\nПосле операции --:");// Подпись
            money.Print();// Печатаем поля
            // Бинарный + (добавляет скаляр к полю second)
            money = money + 10;// Вызов operator +
            Console.WriteLine("\nПосле операции + 10 (добавили 10 купюр):");// Подпись
            money.Print();// Печатаем поля
            Console.ReadLine();// Пауза перед закрытием
        }
    }
    class Money                                                  
    {
        // 1. Поля (делаем их закрытыми, доступ через свойства)
        private int first;// Номинал купюры
        private int second;// Количество купюр
        // 2. Конструктор
        public Money(int first, int second)// Параметры конструктора
        {
            this.first = first;// this — обращение к полю (имя совпадает с параметром)
            this.second = second;// Присваиваем поле second
        }
        // 3. Методы
        // Вывод номинала и количества
        public void Print()// Метод вывода
        {
            Console.WriteLine("Номинал: {0} руб., Количество: {1} шт.", first, second);// Печать полей
        }
        // Хватит ли денег на покупку товара на сумму N
        public bool CanBuy(int N)// Возвращает true/false
        {
            return TotalAmount >= N;// Сравниваем общую сумму с ценой
        }
        // Сколько штук товара стоимостью n можно купить
        public int HowManyItems(int n)// Возвращает количество
        {
            if (n <= 0)// Проверка цены
            {
                Console.WriteLine("Стоимость товара должна быть больше нуля!");// Ошибка
                return 0;// Возвращаем 0
            }
            return TotalAmount / n;// Целочисленное деление
        }
        // 4. Свойства
        // Для получения и установки значения поля first
        public int First// Свойство First
        {
            get { return first; }// Чтение поля
            set { first = value; }// Запись поля
        }
        // Для получения и установки значения поля second
        public int Second// Свойство Second
        {
            get { return second; }// Чтение поля
            set { second = value; }// Запись поля
        }
        // Расчет суммы денег (только для чтения)
        public int TotalAmount// Свойство TotalAmount
        {
            get { return first * second; }// Только get — вычисляемое
        }
        // 5. Индексатор
        public int this[int index]// Индексатор: money[i]
        {
            get// Чтение
            {
                if (index == 0) return first;// Индекс 0 → first
                else if (index == 1) return second;// Индекс 1 → second
                else// Иначе
                {
                    Console.WriteLine("Ошибка: недопустимый индекс {0}", index);// Сообщение
                    return 0;// Возвращаем 0
                }
            }
            set// Запись
            {
                if (index == 0) first = value;// Индекс 0 → запись в first
                else if (index == 1) second = value;// Индекс 1 → запись в second
                else// Иначе
                {
                    Console.WriteLine("Ошибка: недопустимый индекс {0}", index);// Сообщение
                }
            }
        }
        // 6. Перегрузка операторов
        // Операция ++ (увеличивает значение полей first и second)
        public static Money operator ++(Money m)// Унарный ++
        {
            return new Money(m.first + 1, m.second + 1);// Возвращаем новый объект
        }
        // Операция -- (уменьшает значение полей first и second)
        public static Money operator --(Money m)// Унарный --
        {
            return new Money(m.first - 1, m.second - 1);// Возвращаем новый объект
        }
        // Операция ! (true, если поле second не нулевое)
        public static bool operator !(Money m)// Унарный !
        {
            return m.second != 0;// true, если купюры есть
        }
        // Бинарный + (добавляет к значению поля second скаляр)
        public static Money operator +(Money m, int scalar)// Бинарный + (Money + int)
        {
            return new Money(m.first, m.second + scalar);// Номинал не меняем, добавляем купюры
        }
    }
}