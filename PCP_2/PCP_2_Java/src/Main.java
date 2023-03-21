public class Main {
    public static void main(String[] args) {
        int dim = 10;
        int threadNum = 3;
        ArrClass arrClass = new ArrClass(dim, threadNum);
        arrClass.print();
        System.out.println(arrClass.minPart(0, dim));
        int min = arrClass.threadMin();
        System.out.println(min);
    }
}