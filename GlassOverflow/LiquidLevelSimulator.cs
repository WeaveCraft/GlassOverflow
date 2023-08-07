public class LiquidLevelSimulator
{
    private const int MAX_ROWS = 50;

    private OverflowSimulator overflowSimulator;

    public LiquidLevelSimulator()
    {
        this.overflowSimulator = new OverflowSimulator(MAX_ROWS);
    }

    public void Run()
    {
        string quit = null;
        do
        {
            int row = -1;
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