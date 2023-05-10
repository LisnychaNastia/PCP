import java.util.concurrent.Semaphore;

public class Philosopher extends Thread{
    int id;
    Fork leftFork;
    Fork rightFork;
    Semaphore permission;

    public Philosopher(int id,Fork leftFork, Fork rightFork,Semaphore permission)
    {
        this.id = id;
        this.leftFork = leftFork;
        this.rightFork = rightFork;
        this.permission = permission;
    }

    @Override
    public void run()
    {

        for(int i = 0; i < 10; i++) {
            try {
                permission.acquire();
                System.out.println("Philosopher " + id + " is thinking " + (i + 1) + " times");
                leftFork.GetSemaphore().acquire();
                System.out.println("Philosopher " + id + " has taken left fork " + (i + 1) + " times");
                rightFork.GetSemaphore().acquire();
                System.out.println("Philosopher " + id + " has taken right fork " + (i + 1) + " times");

                System.out.println("Philosopher " + id + " is eating " + (i + 1) + " times");

                rightFork.GetSemaphore().release();
                System.out.println("Philosopher " + id + " has put right fork " + (i + 1) + " times");
                leftFork.GetSemaphore().release();
                System.out.println("Philosopher " + id + " has put left fork " + (i + 1) + " times");

                permission.release();

            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
        }
    }
}
