import java.util.concurrent.Semaphore;

public class Fork
{
    int id;
    private Semaphore semaphore;
    public Fork(int id)
    {
        this.id = id;
        semaphore = new Semaphore(1);
    }

    public Semaphore GetSemaphore(){
        return semaphore;
    }

}
