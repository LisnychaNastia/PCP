import java.util.Random;
import java.util.concurrent.Semaphore;

 public class Manager {
     Philosopher[] philosophers = new Philosopher[5];
     Fork[] forks = new Fork[5];

    public Manager()
    {
        Start();
    }

    private void Start()
    {
        for(int i = 0; i < 5; i++)
            forks[i] = new Fork(i+1);
        Random random = new Random();
        int rand_index = random.nextInt(1, 5);
        Semaphore permission = new Semaphore(rand_index);
        for(int i = 0; i < 5; i++) {
            philosophers[i] = new Philosopher(i + 1,forks[i],forks[(i + 1) % forks.length], permission);
            philosophers[i].start();
        }
    }
}
