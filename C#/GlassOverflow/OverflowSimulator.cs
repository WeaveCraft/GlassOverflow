    /*
     * Class which simulates a pyramid of glasses where water is poured into the top glass and then gradually trickles down
     * to the lower glasses, further and further as each glass fills up and overflows.
     */
public class OverflowSimulator
{
    private int maxRows;
    private double maxWater;

    private List<List<double>> waterPerGlass = new List<List<double>>();

    public OverflowSimulator(int maxRows)
    {
        this.maxRows = maxRows;
        this.maxWater = CalcMaxWater(maxRows);

        Setup();
    }

    /*
     * Creates and initializes a data structure that simulates the glass pyramid. Optionally may also pre-compute fill
     * times for each glass and store in a second data structure.
     */
    private void Setup()
    {
        for (int r = 0; r < maxRows; r++)
        {
            waterPerGlass.Add(new List<double>());

            for (int g = 0; g <= r; g++)
            {
                waterPerGlass[r].Add(0.0);
            }
        }
    }

    /*
     * Calculates the total amount of water required to fill every glass. It's based on the amount of water
     * required to fill the glasses at the corners of the final row, which take the longest.
     *
     *   rows - The number of rows in the glass pyramid.
     */
    private double CalcMaxWater(int rows)
    {
        double maxWater = 0.0;
        for (int i = 0; i <= rows; i++)
        {
            maxWater += Math.Pow(2, i);
        }
        return maxWater;
    }


    /*
     * Resets the water in each glass to 0 in between runs of the simulation.
     */
    private void ResetWaterPerGlass()
    {
        for (int r = 0; r < maxRows; r++)
        {
            for (int g = 0; g <= r; g++)
            {
                waterPerGlass[r][g] = 0.0;
            }
        }
    }

    /*
     * Calculates the time it takes for a given glass to fill up.
     *   row - The row of the glass.
     *   glass - The position of the glass on its row.
     *
     *   returns - The time (in seconds) it takes for the glass to fill up, with a margin of error of +- 0.001 seconds.
     */
    public double GetFillTime(int row, int glass)
    {
        double lowerBound = 0.0;
        double upperBound = maxWater;
        double water = 0.0;
        bool repeatBounds = false;

        while (Math.Abs(upperBound - lowerBound) > 0.0001 && !repeatBounds)
        {
            ResetWaterPerGlass();
            water = (lowerBound / 2 + upperBound / 2);
            waterPerGlass[0][0] = water;

            for (int r = 0; r < maxRows; r++)
            {
                for (int g = 0; g <= r; g++)
                {
                    if (waterPerGlass[r][g] > 1.0)
                    {
                        double waterForNext = waterPerGlass[r][g] - 1.0;
                        if (r < maxRows - 1)
                        {
                            waterPerGlass[r + 1][g] += waterForNext / 2;
                            waterPerGlass[r + 1][g + 1] += waterForNext / 2;
                        }
                    }
                }
            }

            if (waterPerGlass[row - 1][glass - 1] >= 1.0)
            {
                if (upperBound == water)
                {
                    repeatBounds = true;
                }
                upperBound = water;
            }
            else
            {
                if (lowerBound == water)
                {
                    repeatBounds = true;
                }
                lowerBound = water;
            }
        }

        return water * 10;
    }

}
