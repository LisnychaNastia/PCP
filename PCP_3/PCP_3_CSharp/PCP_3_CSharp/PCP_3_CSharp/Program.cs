using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PCP_3_CSharp
{
    internal class Program
    {
        private static Random random = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine("Мiсткiсть складу: ");
            int size = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Кiлькiсть предметiв: ");
            int needProdQuantity = Convert.ToInt32(Console.ReadLine());

            Storage st = new Storage(size);
            for (int i = 0; i < random.Next(5, 10); i++)
            {
                new Thread(() => Fabricator("Виробник " + i, needProdQuantity, st)).Start();
                new Thread(() => Consumer("Споживач " + i, needProdQuantity, st)).Start();
            }
        }
        static void Fabricator(string name, int needProdQuantity, Storage st)
        {
            for (int i = 0; i < needProdQuantity; i++)
            {
                st.notFull.WaitOne();
                st.access.WaitOne();
                Thread.Sleep(1000);
                st.products.Add("Продукт");
                Console.WriteLine(name + " Виробник поклав на складi: " + st.products.Count);
                st.notEmpty.Release();
                st.access.Release();
            }
        }
        static void Consumer(string name, int needProdQuantity, Storage st)
        {
            for (int i = 0; i < needProdQuantity; i++)
            {
                st.notEmpty.WaitOne();
                st.access.WaitOne();
                Thread.Sleep(1000);
                st.products.RemoveAt(0);
                Console.WriteLine(name + " Споживач забрав зi складу: " + st.products.Count);
                st.notFull.Release();
                st.access.Release();
            }
        }
    }

    class Storage
    {
        public Semaphore access = new Semaphore(1, 1);
        public Semaphore notEmpty;
        public Semaphore notFull;

        public List<string> products = new List<string>();

        public Storage(int size)
        {
            this.notEmpty = new Semaphore(0, size);
            this.notFull = new Semaphore(size, size);
        }
    }
}