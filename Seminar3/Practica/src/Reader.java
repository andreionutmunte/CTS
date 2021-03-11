
import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.concurrent.Semaphore;


public class Reader extends Thread {

    private final String outputFile;
    private int num;
    private final FileClass file;

    Reader(FileClass file, String outputFile, int num) {
        this.file = file;
        this.outputFile = outputFile;
        this.num = num;
    }

    public void run() {
        while (num > 0) {
            num--;

            String line = file.extract();

            if (line == null) {
                num++;
                try {
                    synchronized (this) {
                        this.wait(100L);
                    }
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            } else {
                String[] words = line.split(" ");

                int firstOperand = Integer.parseInt(words[0]);
                int secondOperand = Integer.parseInt(words[2]);
                String operator = words[1];
                String output = firstOperand + " " + operator + " " + secondOperand + " = ";
                switch (operator) {
                    case "*":
                        output = output + (firstOperand * secondOperand) + '\n';
                        break;
                    case "+":
                        output = output + (firstOperand + secondOperand) + '\n';
                        break;
                    case "-":
                        output = output + (firstOperand - secondOperand) + '\n';
                        break;
                    case "/":
                        output = output + (firstOperand / secondOperand) + '\n';
                        break;
                }
                try {
                    FileWriter w = new FileWriter(outputFile, true);
                    System.out.println(output);
                    w.write(output);
                    w.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }
}
