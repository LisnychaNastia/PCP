public class Main {
    public static void main(String[] args) {
        BreakThread breakThread = new BreakThread();
        new MyThread(1, 2, breakThread).start();
        new MyThread(2, 3, breakThread).start();
        new MyThread(3, 4, breakThread).start();
        new Thread(breakThread).start();
    }
}