namespace Quantum.Bell
{
    open Microsoft.Quantum.Primitive;
    open Microsoft.Quantum.Canon;

    operation Set (desired: Result, q1: Qubit) : Unit
    {
        let current = M(q1);
        if (desired != current)
        {
            X(q1);
        }
    }

    operation BellTest (count : Int, initial: Result) : (Int, Int)
    {
        mutable numOnes = 0;
        using (qubit = Qubit())
        {
            for (test in 1..count)
            {
                Set (initial, qubit);

                let res = M (qubit);

                // Count the number of ones we saw:
                if (res == One)
                {
                    set numOnes = numOnes + 1;
                }
            }
            Set(Zero, qubit);
        }

        // Return number of times we saw a |0> and number of times we saw a |1>
        return (count-numOnes, numOnes);
    }
}