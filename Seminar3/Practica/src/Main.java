import java.util.Random;
import java.util.Scanner;

public class Main {

    public static void main(String[] args) throws InterruptedException {

        Random rand = new Random();
        int f = Math.abs(rand.nextInt()) % 3 + 1;
        System.out.println("Initializam " + f + " obiecte Reader si " + f + " obiecte Writer");

        Writer[] w = new Writer[f];
        Reader[] r = new Reader[f];

        for (int i = 0; i < f; i++) {
            w[i] = new Writer(new FileClass("queue.txt"), 10);
            System.out.println("Obiectul Reader " + i + " scrie rezultatele in output" + i + ".txt");
            r[i] = new Reader(new FileClass("queue.txt"), "queue" + i + ".txt", 10);

        }

        for (int i = 0; i < f; i++) {
            w[i].start();
            r[i].start();
        }
    }
}
