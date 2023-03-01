public class MyThread extends Thread{

    private final int id;
    private final int step;
    private final BreakThread breakThread;

    public MyThread(int id, int step, BreakThread breakThread) {
        this.id = id;
        this.step = step;
        this.breakThread = breakThread;
    }

    @Override
    public void run() {
        boolean isStop = false;
        int count = 0;
        int sum = 0;
        do{
            sum += step;
            count++;
            isStop = breakThread.isCanBreak();

        }while (!isStop);
        System.out.printf("id - %d, count - %d, sum - %d\n", id, count, sum);
    }
}
