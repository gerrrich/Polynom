using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class Polynom
    {
        List<double> list;      // список коэффициентов многочлена

        public Polynom()    // конструктор для создания пустого многочлена
        {
            list = new List<double>();
        }

        public Polynom(params double[] b)    // конструктор для создания многочлена с использованием данных из массива
        {
            list = new List<double>(b);
        }

        private double DoubleNumber(string s)
        {
            s.Trim();
            bool minus = false || s.Contains("-");
            double d;

            if (s.Contains(",") || s.Contains("."))
            {
                string[] split = s.Split(new Char[] { ',', '.' });
                d = int.Parse(split[0]) + (int.Parse(split[1]) / Math.Pow(10, split[1].Length));
            }
            else
                d = int.Parse(s);
            if (minus == true && d >= 0)
                d *= -1;
            return d;
        }

        public Polynom(string s)    // конструктор для создания многочлена вводом из строки
        {
            string[] numbers = s.Split(new Char[] { ';', '\t' });

            list = new List<double>();
            foreach (string n in numbers)
            {
                if ((s.Trim() != "") && (n != " "))
                {
                    list.Add(DoubleNumber(n));
                }
            }
        }

        public override string ToString()    // печать многочлена
        {
            string result = "";
            double eps = 0.00001;

            if (list.Count == 0 || (list.Count == 1 && Math.Abs(list[0]) < eps))
                result = "0";
            else
            {
                if (list[0] < 0)
                    result += "-";

                for (int i = 0; i < list.Count - 1; i++)
                {
                    if (Math.Abs(list[i]) > eps)
                    {
                        result += Math.Abs(list[i]).ToString() + "*x^" + (list.Count - i - 1).ToString();
                    }

                    if (list[i + 1] > 0)
                        result += " + ";
                    else
                        if (list[i + 1] < 0)
                        result += " - ";
                    else
                        continue;
                }

                if (Math.Abs(list[list.Count - 1]) > eps)
                    result += Math.Abs(list[list.Count - 1]).ToString();
            }

            return result;
        }



        public double ValueAtPoint(int c)   // нахождение значения многочлена в точке
        {
            double result = 0;
            int Xpow = 1;

            for (int i = list.Count - 1; i >= 0; i--)
            {
                result += list[i] * Xpow;
                Xpow *= c;
            }

            return result;
        }

        private Polynom Addition(Polynom q)     // функция нахождения суммы многочленов
        {
            if (list.Capacity == 0)
                return q;
            if (q.list.Capacity == 0)
                return this;

            double[] array = new double[list.Count];
            list.CopyTo(array);

            Polynom sum = new Polynom(array);
            for (int i = 0; i < list.Count; i++)
                sum.list[i] += q.list[i];

            return sum;
        }

        public static Polynom operator +(Polynom p, Polynom q)    // переопределение оператора "+"
        {
            return p.Addition(q);
        }

        private Polynom Subtraction(Polynom q)    // функция нахождения разности многочленов
        {
            if (q.list.Capacity == 0)
                return this;
            if (list.Capacity == 0)
                return q.DigMult(-1);

            double[] array = new double[list.Count];
            list.CopyTo(array);

            Polynom dif = new Polynom(array);
            for (int i = 0; i < list.Count; i++)
                dif.list[i] -= q.list[i];

            /*if (Math.Abs(dif.list[0]) < 0.000001)
                while (Math.Abs(dif.list[0]) < 0.000001)
            {
                dif.list.RemoveAt(0);
            }*/

            return dif;
        }

        public static Polynom operator -(Polynom p, Polynom q)    // переопределение оператора "-"
        {
            return p.Subtraction(q);
        }

        private Polynom DigMult(double c)    // функция умножения многочлена на число
        {
            {
                for (int i = 0; i < list.Count; i++)
                    list[i] *= c;
            }
            return this;
        }

        private Polynom Shift()     // сдвиг степеней многочлена (добавление нулей в конец списка)
        {
            list.Add(0);
            return this;
        }

        public static Polynom operator *(Polynom p, Polynom q)    // переопределение оператора "*"
        {
            if (p.list.Capacity == 0)
                return p;
            if (q.list.Capacity == 0)
                return q;

            double[] array = new double[p.list.Count];

            Polynom mult = new Polynom();
            for (int i = 0; i < (p.list.Count + q.list.Count - 1); i++)
                mult.list.Add(0);    //создание полинома произведения со значениями = 0


            p.list.CopyTo(array);   // копирование первого многочлена массив
            Polynom temp;    // создание временного многочлена

            int power = q.list.Count - 1;    // переменная для показателя степени второго многочлена

            for (int i = 0; i < q.list.Count; i++)   // цикл умножения первого многочлена на коэффициенты второго (многочлены в стандартном виде)
            {
                temp = new Polynom(array);    // временный многочлен принимает значения первого многочлена

                temp.DigMult(q.list[i]);    // умножаем временный многочлен на коэффициент второго многочлена
                for (int j = power; j > 0; j--)   // сдвиг степеней многочлена
                    temp.Shift();
                power--;

                // сложение многочлена произведения с временным
                for (int k = mult.list.Count - 1; k >= (mult.list.Count - temp.list.Count); k--)
                    mult.list[k] += temp.list[k - (mult.list.Count - temp.list.Count)];
            }

            return mult;
        }

        public Polynom Derivative()    // нахождение производной многочлена
        {
            Polynom der;    // многочлен производной

            if (list.Capacity == 0)
            {
                der = new Polynom();
                return der;
            }

            double[] array = new double[list.Count];
            list.CopyTo(array);

            der = new Polynom(array);

            for (int i = 0; i < list.Count; i++)
            {
                der.list[i] *= der.list.Count - 1 - i;
            }
            der.list.RemoveAt(der.list.Count - 1);

            return der;
        }
    }
}