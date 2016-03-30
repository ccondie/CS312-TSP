using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP {
    class State : IComparable<State>
    {

        int lowerBound;
        double[][] reducedCostMatrix;       // NOTE: This can be an array of double because the actual cities are irrelevent ... and are processed outside state in the solution
        ArrayList path;                     // NOTE: this should be an ArrayList of City objects
        HashSet<City> remainingCities;      // NOTE: this is used to generate the children of this state

        static int BSSF;

        public State(double[][] parentMatrix, ArrayList parentPath, int parentLowerBound, City nextCity) {
            // add the inputed nextCity to the local path
            path = parentPath;
            path.Add(nextCity);

            // generate this state's reduced cost matrix from parentMatrix, parentLowerBound, and nextCity
            reducedCostMatrix = parentMatrix;
            lowerBound = parentLowerBound;

            /************************************************************
            * INCOMPLETE CODE
            *************************************************************/
        }

        public static void setBSSF(int newBSSF) { BSSF = newBSSF; }

        public int CompareTo(State otherState)
        {
            double pathCountModifier = 1.25;
            double distanceToBSSFModifier = 1.05;

            /* WEIGHT LOGIC
            * 1. Distance from the BSSF
            * 2. Length of the Path so far
            */
            double localWeight = (path.Count * pathCountModifier) + ((BSSF - lowerBound) * distanceToBSSFModifier);
            double otherWeight = (otherState.path.Count * pathCountModifier) + ((BSSF - otherState.lowerBound) * distanceToBSSFModifier);

            if (localWeight > otherWeight)
                return 1;
            else if (localWeight < otherWeight)
                return -1;
            else
                return 0;
        }
    }
}
