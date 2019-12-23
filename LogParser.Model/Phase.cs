using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.Model
{
    public class Phase
    {
        public static bool IsImbalance(int firstValue, int secondValue, int thirdValue)
        {
            if (firstValue > secondValue && firstValue > thirdValue)
                return IsCheckingForImbalance(firstValue, secondValue, thirdValue);
            else if (secondValue > thirdValue)
                return IsCheckingForImbalance(secondValue, firstValue, thirdValue);
            else
                return IsCheckingForImbalance(thirdValue, firstValue, secondValue);
        }

        private static bool IsCheckingForImbalance(int maxAmperage, int secondAmperage, int thirdAmperage)
        {
            bool isPhaseImbalance = true;

            double persentMaxAmperage = 100;
            double persentSecondAmperage = (double)secondAmperage / maxAmperage * 100;
            double persentThirdAmperage = (double)thirdAmperage / maxAmperage * 100;

            int phaseImbalancePersent = 20;

            if (persentMaxAmperage - persentSecondAmperage > phaseImbalancePersent || persentMaxAmperage - persentThirdAmperage > phaseImbalancePersent)
            {
                return isPhaseImbalance;
            }
            else
            {
                return !isPhaseImbalance;
            }
        }
    }
}
