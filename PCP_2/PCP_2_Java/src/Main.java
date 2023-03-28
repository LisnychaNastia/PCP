public class Main {
    public static void main(String[] args) {
        int dim = 10;
        int threadNum = 3;
        ArrClass arrClass = new ArrClass(dim, threadNum);
        arrClass.print();

        int min = arrClass.threadMin();
        System.out.println("Мінімальним елементом серед масиву є: " + (min));
        System.out.println("Мінімальне значення під індексом: " + arrClass.index_min(min));
    }
}
