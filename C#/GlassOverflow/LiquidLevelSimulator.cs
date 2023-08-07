public class LiquidLevelSimulator
{
    private const int MAX_ROWS = 50;

    private OverflowSimulator overflowSimulator;

    public LiquidLevelSimulator()
    {
        this.overflowSimulator = new OverflowSimulator(MAX_ROWS);
    }

    /*
     * Runs the program, which entails asking the user for a row and a glass-position and printing the time the
     * corresponding glass takes to fill.
     */
    public void Run()
    {
        string quit;
        int row = -1;
        do
        {
            do
            {
                Console.Write("Rad ? (2-" + MAX_ROWS + ") ");
                row = ValidateIntegerInput(2, MAX_ROWS);
            } while (row == -1);

            int glass = -1;
            do
            {
                Console.Write("Glas ? (1-" + row + ") ");
                glass = ValidateIntegerInput(1, row);
            } while (glass == -1);

            double fillTime = overflowSimulator.GetFillTime(row, glass);
            Console.WriteLine("Det tar " + String.Format("{0:F3}", fillTime) + " sekunder.");

            Console.Write("Avsluta? (j/n) ");
            quit = Console.ReadLine();
        } while (!quit.Equals("j", StringComparison.OrdinalIgnoreCase));
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
    private int ValidateIntegerInput(int min, int max)
    {
        if (int.TryParse(Console.ReadLine(), out int input))
        {
            if (input >= min && input <= max)
            {
                return input;
            }
        }

        Console.WriteLine("Ogiltigt input.");
        return -1;
    }
}