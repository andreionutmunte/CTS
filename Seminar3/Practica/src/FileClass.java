import java.io.*;
import java.nio.Buffer;
import java.util.concurrent.Semaphore;

public class FileClass {
    private static Semaphore semaphore;
    private final String file;

    public FileClass(String file) {
        semaphore = new Semaphore(1);
        this.file = file;
        File f = new File(file);
    }

    public void append(String text) {
        System.out.println("append " + text);
        try {
            synchronized (this) {
                semaphore.acquire();
                System.out.println("append " + text);
                FileWriter w = new FileWriter(file, true);
                w.write(text);
                w.close();
                semaphore.release();
            }
        } catch (InterruptedException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public String extract() {
        System.out.println("extract");
        String nextLine;
        String[] lines = new String[1000];
        Integer i = 0;
        try {
            semaphore.acquire();
            BufferedReader r = new BufferedReader(new FileReader(file));
            while ((nextLine = r.readLine()) != null) {
                lines[i++] = nextLine;
            }
            r.close();
            if (i == 0) {
                semaphore.release();
                return null;
            }
            if (i == 1 && lines[0].trim().equals("")) {
                semaphore.release();
                return null;
            }
            BufferedWriter w = new BufferedWriter(new FileWriter(file));
            for (int j = 1; j < i; j++) {
                w.write(lines[j] + '\n');
            }
            w.close();
            semaphore.release();
            return lines[0];
        } catch (InterruptedException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        semaphore.release();
        return null;
    }
}
