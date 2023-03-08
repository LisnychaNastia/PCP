import java.util.Scanner;

public class Main {

    private final static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        System.out.print("Enter number of Threads: ");
        int count = sc.nextInt();

        BreakThread breakThread = new BreakThread();
        for (int i = 0; i < count; i++) {
            new MyThread(i, i + 2, breakThread).start();
        }
        new Thread(breakThread).start();
    }
}