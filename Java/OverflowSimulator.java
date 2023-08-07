import java.util.ArrayList;

/*
 * Class which simulates a pyramid of glasses where water is poured into the top glass and then gradually trickles down
 * to the lower glasses, further and further as each glass fills up and overflows.
 */
public class OverflowSimulator {
    private int maxRows;
    private double maxWater;

    private ArrayList<ArrayList<Double>> waterPerGlass = new ArrayList<>();

    public OverflowSimulator(int maxRows) {
        this.maxRows = maxRows;
        this.maxWater = calcMaxWater(maxRows);

        setup();
    }

    /*
     * Creates and initializes a data structure that simulates the glass pyramid. Optionally may also pre-compute fill
     * times for each glass and store in a second data structure.
     */
    private void setup() {
        for(int r = 0; r < maxRows; r++) {
            waterPerGlass.add(new ArrayList<>());

            for(int g = 0; g <= r; g++) {
                waterPerGlass.get(r).add(0.0);
            }
        }
    }

    /*
     * Calculates the total amount of water required to fill every glass. It's based on the amount of water
     * required to fill the glasses at the corners of the final row, which take the longest.
     *
     *   rows - The number of rows in the glass pyramid.
     */
    private double calcMaxWater(int rows) {
        double maxWater = 0.0;
        for(int i = 0; i <= rows; i++) {
            maxWater += Math.pow(2,i);
        }
        return maxWater;
    }

    /*
     * Resets the water in each glass to 0 in between runs of the simulation.
     */
    private void resetWaterPerGlass() {
        for(int r = 0; r < maxRows; r++) {
            for(int g = 0; g <= r; g++) {
                waterPerGlass.get(r).set(g, 0.0);
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
    public double getFillTime(int row, int glass) {
        double lowerBound = 0.0;
        double upperBound = maxWater;
        double water = 0.0;
        boolean repeatBounds = false;

        while(Math.abs(upperBound - lowerBound) > 0.0001 && !repeatBounds) {
            resetWaterPerGlass();
            water = (lowerBound / 2 + upperBound / 2);
            waterPerGlass.get(0).set(0, water);

            for(int r = 0; r < maxRows; r++) {
                for(int g = 0; g <= r; g++) {
                    if(waterPerGlass.get(r).get(g) > 1.0) {
                        double waterForNext = waterPerGlass.get(r).get(g) - 1.0;
                        if(r < maxRows-1) {
                            waterPerGlass.get(r+1).set(g, waterPerGlass.get(r+1).get(g) + waterForNext / 2);
                            waterPerGlass.get(r+1).set(g+1, waterPerGlass.get(r+1).get(g+1) + waterForNext / 2);
                        }
                    }
                }
            }

            if(waterPerGlass.get(row-1).get(glass-1) >= 1.0) {
                if(upperBound == water) {
                    repeatBounds = true;
                }
                upperBound = water;
            }
            else {
                if(lowerBound == water) {
                    repeatBounds = true;
                }
                lowerBound = water;
            }
        }

        return water * 10;
    }

}
