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
            BreakThread breakThread = new BreakThread();
            new Thread(new MainThread(1, 2, breakThread).Calculator).Start();
            new Thread(new MainThread(2, 3, breakThread).Calculator).Start();
            new Thread(new MainThread(3, 4, breakThread).Calculator).Start();

            Thread thread1 = new Thread(new MainThread(4, 5, breakThread).Calculator);
            thread1.Start();
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
