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

    private double CalcMaxWater(int rows)
    {
        double maxWater = 0.0;
        for (int i = 0; i <= rows; i++)
        {
            maxWater += Math.Pow(2, i);
        }
        return maxWater;
    }

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
