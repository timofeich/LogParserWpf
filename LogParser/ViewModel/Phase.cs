using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.ViewModel
{
    public static class Phase
    {
        public static bool IsImbalance(int AmperageA, int AmperageB, int AmperageC)
        {
            if (AmperageA > AmperageB && AmperageA > AmperageC)
                return IsCheckingForImbalance(AmperageA, AmperageB, AmperageC);
            else if (AmperageB > AmperageC)
                return IsCheckingForImbalance(AmperageB, AmperageA, AmperageC);
            else
                return IsCheckingForImbalance(AmperageC, AmperageA, AmperageB);
        }

        private static bool IsCheckingForImbalance(int maxAmperage, int secondAmperage, int thirdAmperage)
        {
            bool isPhaseImbalance = true;

            double persentMaxAmperage= 100;
            double persentSecondAmperage =(double)secondAmperage / maxAmperage * 100;
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
