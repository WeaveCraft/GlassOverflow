import java.util.Scanner;

public class LiquidLevelSimulator {
    private final int MAX_ROWS = 50;    // Height of the simulated glass pyramid.

    private Scanner scanner = new Scanner(System.in);
    private OverflowSimulator overflowSimulator;

    public LiquidLevelSimulator() {
        this.overflowSimulator = new OverflowSimulator(MAX_ROWS);
    }

    /*
     * Runs the program, which entails asking the user for a row and a glass-position and printing the time the
     * corresponding glass takes to fill.
     */
    public void run() {
        String quit = null;
        do {
            int row = -1;
            do {
                System.out.print("Rad ? (2-" + MAX_ROWS + ") ");
                row = validateIntegerInput(2, MAX_ROWS);
            } while(row == -1);


            int glass = -1;
            do {
                System.out.print("Glas ? (1-" + row + ") ");
                glass = validateIntegerInput(1, row);
            } while(glass == -1);

            double fillTime = overflowSimulator.getFillTime(row,glass);
            System.out.println("Det tar " + String.format("%.3f", fillTime) + " sekunder.");

            System.out.print("Avsluta? (j/n) ");
            quit = scanner.nextLine();
        } while(!quit.equals("j"));
    }

    /*
     * Accepts and validates an input to the scanner to confirm that it is an integer and that it falls within the
     * desired range.
     *
     *   min - The lowest acceptable integer input.
     *   max - The highest acceptable integer input.
     *
     *   returns - The integer input if it is valid; -1 otherwise.
     */
    private int validateIntegerInput(int min, int max) {
        if(scanner.hasNextInt()) {
            int input = scanner.nextInt();
            if(input >= min && input <= max) {
                scanner.nextLine();
                return input;
            }
        }

        System.out.println("Ogiltigt input.");
        scanner.nextLine();
        return -1;
    }
}
