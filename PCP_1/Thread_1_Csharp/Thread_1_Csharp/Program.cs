using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thread_1_Csharp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            (new Program()).Start();
        }

        void Start()
        {
            Console.Write("Enter ur number of Threads: ");
            int count = int.Parse(Console.ReadLine());

            BreakThread breakThread = new BreakThread();
            for (int i = 0; i < count; i++)
			{
                new Thread(new MainThread(i, i + 2, breakThread).Calculator).Start();
			}
            new Thread(breakThread.Stopper).Start();
            Console.ReadLine();
        }

        class MainThread
        {
            private int id;
            private int step;
            private BreakThread breakThread;

            public MainThread(int id, int step, BreakThread breakThread)
            {
                this.id = id;
                this.step = step;
                this.breakThread = breakThread;
            }

            public void Calculator()
            {
                long count = 0;
                long sum = 0;
                do
                {
                    sum += step;
                    count++;

                } while (!breakThread.CanStop);

                Console.WriteLine("id: {0}, sum: {1},  count: {2}", id, sum, count);
            }
        }

        class BreakThread
        {
            private bool canStop = false;

            public bool CanStop { get => canStop; }

            public void Stopper()
            {
                Thread.Sleep(10 * 1000);
                canStop = true;
            }
        }
    }
}
