using System;

using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;

namespace  Quantum.Bell
{
    class Driver
    {
        static void Main(string[] args)
        {
            using (var qsim = new QuantumSimulator())
            {
                Result[] initials;
                // Try initial values
                initials = new Result[] { Result.Zero, Result.One };
                foreach (Result initial in initials)
                {
                    var res = BellTestClassical.Run(qsim, 1000, initial).Result;
                    var (numZeros, numOnes) = res;
                    System.Console.WriteLine(
                        $"Classical - Init:{initial,-4} 0s={numZeros,-4} 1s={numOnes,-4}");
                }
                // reset to initial values
                initials = new Result[] { Result.Zero, Result.One };
                foreach (Result initial in initials)
                {
                    var res = BellTestPauli.Run(qsim, 1000, initial).Result;
                    var (numZeros, numOnes) = res;
                    System.Console.WriteLine(
                        $"Pauli - Init:{initial,-4} 0s={numZeros,-4} 1s={numOnes,-4}");
                }
                // reset to initial values
                initials = new Result[] { Result.Zero, Result.One };
                foreach (Result initial in initials)
                {
                    var res = BellTestHadamard.Run(qsim, 1000, initial).Result;
                    var (numZeros, numOnes) = res;
                    System.Console.WriteLine(
                        $"Hadamard - Init:{initial,-4} 0s={numZeros,-4} 1s={numOnes,-4}");
                }
                // reset to initial values
                initials = new Result[] { Result.Zero, Result.One };
                foreach (Result initial in initials)
                {
                    var res = BellTestEntanglement.Run(qsim, 1000, initial).Result;
                    var (numZeros, numOnes, agree) = res;
                    System.Console.WriteLine(
                        $"Entanglement - Init:{initial,-4} 0s={numZeros,-4} 1s={numOnes,-4} agree={agree,-4}");
                }
            }
            
            //System.Console.WriteLine("Press any key to continue...");
            //System.Console.ReadKey();
        }
    }
}