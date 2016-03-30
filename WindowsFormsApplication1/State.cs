using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP {
    class State : IComparable {

        int lowerBound;
        double[][] reducedCostMatrix;
        ArrayList path;
        static int BSSF;

        public State(double[][] parentMatrix, ArrayList parentPath, int parentLowerBound, int nextCity) {
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


        public int CompareTo(object other) {
            State otherState = (State)other;

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

        public static void setBSSF(int newBSSF) { BSSF = newBSSF; }
    }
}
