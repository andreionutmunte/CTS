
import java.io.FileWriter;
import java.io.IOException;
import java.util.Random;
import java.util.concurrent.Semaphore;

public class Writer extends Thread {
    int num;
    FileClass file;
    Random rand;

    Writer(FileClass file, int num) {
        this.file = file;
        this.num = num;
        this.rand = new Random();
    }

    public void run() {
        while (num > 0) {
            num--;

            int r1 = rand.nextInt(1000);
            int r2 = rand.nextInt(1000);
            String op = "";
            int o = Math.abs(rand.nextInt(4));

            switch (o) {
                case 0:
                    op = "+";
                    break;
                case 1:
                    op = "-";
                    break;
                case 2:
                    op = "*";
                    break;
                case 3:
                    op = "/";
                    break;
            }
            System.out.println(r1 + " " + op + " " + r2 + "\n");
            file.append(r1 + " " + op + " " + r2 + "\n");
        }
    }
}
