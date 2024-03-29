using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PCP_2_Csharp
{
    internal class Program
    {
        private static readonly int dim = 20;
        private static readonly int threadNum = 3;

        private readonly Thread[] thread = new Thread[threadNum];

        static void Main(string[] args)
        {
            Program main = new Program();
            main.InitArr();
            main.Print();

            long min = main.ParallelMin();
            Console.WriteLine("Мiнiмальним елементом серед масиву є: " + min);
            Console.WriteLine("Iндекс мiнiмального числа: " + main.IndexMin((int)min));
            Console.ReadKey();
        }
       
        private void Print()
        {
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        private int threadCount = 0;

        private long ParallelMin()
        {
            int step = dim / threadNum;
            for (int i = 0; i < threadNum - 1; i++)
            {
                thread[i] = new Thread(StarterThread);
                thread[i].Start(new Bound(i * step, (i + 1) * step));
            }
            thread[threadNum - 1] = new Thread(StarterThread);
            thread[threadNum - 1].Start(new Bound((threadNum - 1) * step, dim));

            lock (lockerForCount)
            {
                while (threadCount < threadNum)
                {
                    Monitor.Wait(lockerForCount);
                }
            }
            return min;
        }

        private readonly int[] arr = new int[dim];

        private void InitArr()
        {
            Random r = new Random();
            for (int i = 0; i < dim; i++)
            {
                arr[i] = r.Next(0, 20);
            }
            arr[r.Next(0, dim - 1)] = -1;

        }

        private int IndexMin(int min)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == min)
                {
                    return i;
                }
            }
            return -1;
        }

        class Bound
        {
            public Bound(int startIndex, int finishIndex)
            {
                StartIndex = startIndex;
                FinishIndex = finishIndex;
            }

            public int StartIndex { get; set; }
            public int FinishIndex { get; set; }
        }

        private readonly object lockerForMin = new object();
        private void StarterThread(object param)
        {
            if (param is Bound)
            {
                long min = PartMin((param as Bound).StartIndex, (param as Bound).FinishIndex);

                lock (lockerForMin)
                {
                    CollectMin(min);
                }
                IncThreadCount();
            }
        }

        private readonly object lockerForCount = new object();
        private void IncThreadCount()
        {
            lock (lockerForCount)
            {
                threadCount++;
                Monitor.Pulse(lockerForCount);
            }
        }

        private long min = int.MaxValue;
        public void CollectMin(long min)
        {
            if (min < this.min)
                this.min = min;
        }

        public long PartMin(int startIndex, int finishIndex)
        {
            int min = int.MaxValue;
            for (int i = startIndex; i < finishIndex; i++)
            {
                if (arr[i] < min)
                    min = arr[i];
            }
            return min;
        }
    }
}
