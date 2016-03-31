using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP {
    class State : IComparable<State>
    {

        double lowerBound;
        double[,] reducedCostMatrix;       // NOTE: This can be an array of double because the actual cities are irrelevent ... and are processed outside state in the solution
        List<int> path;                     
        HashSet<int> remainingCitiesIndexes;      // NOTE: this is used to generate the children of this state

        public State(double[,] parentMatrix, List<int> parentPath, double parentLowerBound, int nextCityIndex, HashSet<int> remainingCitiesIndexes) {
            
            // add the inputed nextCity to the local path
            path = parentPath;
            path.Add(nextCityIndex);
            // remove the nextCity from the set of remaining cities
            this.remainingCitiesIndexes = remainingCitiesIndexes;
            this.remainingCitiesIndexes.Remove(nextCityIndex);


            // generate this state's reduced cost matrix from parentMatrix, parentLowerBound, and nextCity
            reducedCostMatrix = parentMatrix;
            lowerBound = parentLowerBound;


            //if the path is larger than 1
            if (path.Count > 1) {
                int prevCityIndex = path[path.Count - 1];

                //if the next element in the reducedCostMatrix is inifinity, set the lowerBound for this state to infinity and return
                if (double.IsPositiveInfinity(reducedCostMatrix[prevCityIndex, nextCityIndex])) {
                    lowerBound = double.PositiveInfinity;
                    return;
                }

                //grab the value at the next element
                double additionToLowerBound = reducedCostMatrix[prevCityIndex, nextCityIndex];
                    
                //make the row and column elements associated with the next element all infinity

                //check for zeros and adjust accordingly


                // Search row by row, find smallest index, reduce all in row by index value, add indexValue to initialLower
                for (int y = 0; y < reducedCostMatrix.GetLength(1); y++) {
                    int smallestIndex = 0;
                    double smallestIndexValue = double.PositiveInfinity;

                    //find the smallest index
                    for (int x = 0; x < reducedCostMatrix.GetLength(0); x++) {
                        if (reducedCostMatrix[x, y] < smallestIndexValue) {
                            smallestIndex = x;
                            smallestIndexValue = reducedCostMatrix[x, y];
                        }
                    }
                    //add index value to initialLower
                    lowerBound = lowerBound + smallestIndexValue;
                    //reduce all in row by index value
                    for (int x = 0; x < reducedCostMatrix.GetLength(0); x++) {
                        reducedCostMatrix[x, y] = reducedCostMatrix[x, y] - smallestIndexValue;
                    }
                }

                // Search column by column, find smallest index in column, reduce all in column by index value, add indexValue to initialLower
                for (int x = 0; x < reducedCostMatrix.GetLength(0); x++) {
                    int smallestIndex = 0;
                    double smallestIndexValue = double.PositiveInfinity;

                    //find the smallest index (if the smallest index is zero, nothing will change, as it should be)
                    for (int y = 0; y < reducedCostMatrix.GetLength(1); y++) {
                        if (reducedCostMatrix[x, y] < smallestIndexValue) {
                            smallestIndex = y;
                            smallestIndexValue = reducedCostMatrix[x, y];
                        }
                    }

                    //if there wasn't a zero in the column
                    if (smallestIndexValue != 0) {
                        //add index value to initialLower
                        lowerBound = lowerBound + smallestIndexValue;
                        for (int y = 0; y < reducedCostMatrix.GetLength(1); y++) {
                            reducedCostMatrix[x, y] = reducedCostMatrix[x, y] - smallestIndexValue;
                        }
                    }
                }
            }
            else {
                //case where this is the first state in the simulation

                //Nothing else to do
            }
            








           
        }

        public int CompareTo(State otherState)
        {
            double pathCountModifier = 1.25;
            double lowerBoundModifier = 1.5;

            /* WEIGHT LOGIC
            * 1. Which has the lower lowerBound
            * 2. Length of the Path so far
            */
            double localWeight = (path.Count * pathCountModifier) - (lowerBound * lowerBoundModifier);
            double otherWeight = (otherState.path.Count * pathCountModifier) - (otherState.lowerBound * lowerBoundModifier);

            if (localWeight > otherWeight)
                return 1;
            else if (localWeight < otherWeight)
                return -1;
            else
                return 0;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            for (int i = 0; i < path.Count; i++)
                sb.Append(path[i].ToString() + ", ");
            sb.Append("]\n");

            return sb.ToString();
        }

        public double getLowerBound() { return lowerBound; }

        public HashSet<int> getRemainingCities() { return remainingCitiesIndexes; }

        public double[,] getReducedCostMatrix() { return reducedCostMatrix; }

        public List<int> getPath() { return path; }

    }
}
