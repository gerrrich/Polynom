using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        public static void Main(string[] args)
        {
            // создание многочлена без параметров: результат - пустой многочлен
            Polynom p0 = new Polynom();
            Console.WriteLine("Empty polynom: ");
            Console.WriteLine(p0);  // печать многочлена

            // создание многочлена с использованием массива
            double[] mas1 = { 3, 0, -1 };
            Polynom p1 = new Polynom();
            p1 = new Polynom(mas1);

            Console.WriteLine("Polynom 1: ");
            Console.WriteLine(p1);  // печать многочлена

            double[] mas2;
            mas2 = new double[3];
            mas2[0] = 3;
            mas2[1] = 2;
            mas2[2] = 1;

            // создание многочлена со значениями, хранящимися в массиве
            Polynom p2 = new Polynom(mas2);

            Console.WriteLine("Polynom 2: ");
            Console.WriteLine(p2);  // печать многочлена

            Console.Write("Value at Point (2) = ");
            Console.WriteLine(p2.ValueAtPoint(2));  // значение многочлена в точке
            Console.WriteLine();

            // создание многочлена при вводе коэффициентов с консоли
            Console.WriteLine("Enter the Coefficients of the Polinom in the standart form (a*x^n + b*x^(n-1) + ... + c)");
            Console.WriteLine("Input: 'a;b;...;c'  ");
            //string s = Console.ReadLine();
            string s = "5;-0.2; 11,45; -7";

            Console.WriteLine();
            Console.WriteLine("Polynom 3 from console: ");
            Polynom p3 = new Polynom(s);
            Console.WriteLine(p3);  // печать многочлена
            Console.WriteLine();

            // нахождение суммы многочленов
            Console.WriteLine("Polynom 1 + Polynom 2 = ");
            Console.WriteLine(p1 + p2);   // сумма двух многочленов

            // нахождение разности многочленов
            Console.WriteLine("Polynom 1 - Polynom 2 = ");
            Console.WriteLine(p1 - p2);   // разность двух многочленов

            // нахождение произведения многочленов
            Console.WriteLine("Polynom 1 * Polynom 2 = ");
            Console.WriteLine(p1 * p2);

            // нахождение производной многочлена
            Console.WriteLine("The First Derivative of the Polynom 3 = ");
            Console.WriteLine(p3.Derivative());
        }
    }
}
