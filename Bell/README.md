# Creating a Bell State in Q#
Official Reference taken from [Writing a Quantum Program](https://docs.microsoft.com/en-us/quantum/quickstart?view=qsharp-preview&tabs=tabid-vscode)

## How  to run Bell
Enter the `Bell` folder and run

```sh
dotnet run
```

# Description
Our goal is to create a [Bell State](https://en.wikipedia.org/wiki/Bell_state) showing entanglement. We will build this up piece by piece to show the concepts of qubit state, gates and measurement.

The operation `Set` set a qubit in a known state (Zero or One). We measure the qubit, if it's in the state we want, we leave it alone, otherwise, we flip it with the X gate. 

```C#
operation Set (desired: Result, q1: Qubit) : Unit
    {
        let current = M(q1);
        if (desired != current)
        {
            X(q1);
        }
    }
```

This code defines a `Q#` operation. An operation is the basic unit of quantum execution in `Q#`. It is roughly equivalent to a function in `C` or `C++` or `Python`, or a static method in `C#` or `Java`.

```C#
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
```

The operation `BellTest` will loop for count iterations, set a specified initial value on a qubit and them measure (`M`) the result. It will gather statistics on how many zeros and ones we've measured and return them to the caller. It performs one other necessary operation. It resets the qubit to a known state (`Zero`) before returning it allowing others to allocate this qubit in a known state. This is required by the using statement.

All of these calls use primitive quantum operations that are defined in the `Microsoft.Quantum.Primitive` namespace. For instance, the `M` operation measures its argument qubit in the computational (`Z`) basis, and `X` applies a state flip around the x axis to its argument qubit.

## Creating Superposition
Now we want to manipulate the qubit. First we'll just try to flip it. This is accomplished by performing an `X` gate before we measure it in BellTest

```C#
X(qubit);
let res = M (qubit);
```

Let's get a quantum result. All we need to do is replace the `X` gate in the previous run with an `H` or Hadamard gate. Instead of flipping the qubit all the way from 0 to 1, we will only flip it halfway. The replaced lines in BellTest now look like:


```C#
H(qubit);
let res = M (qubit);
```

Every time we measure, we ask for a classical value, but the qubit is halfway between 0 and 1, so we get (statistically) 0 half the time and 1 half the time. This is known as **superposition** and gives us our first real view into a quantum state.

## Entanglement
 To show off entanglement the first thing we'll need to do is allocate 2 qubits instead of one in BellTest:

```C#
using (qubits = Qubit[2])
        {
            for (test in 1..count)
            {
                Set (initial, qubits[0]);
                Set (Zero, qubits[1]);

                H(qubits[0]);
                CNOT(qubits[0],qubits[1]);
                let res = M (qubits[0]);
```
We've added another Set operation to initialize qubit 1 to make sure that it's always in the Zero state when we start. We also need to reset the second qubit before releasing it (this could also be done with a for loop). We'll add a line after qubit 0 is reset:

```C#
Set(Zero, qubits[0]);
Set(Zero, qubits[1]);
```+

There is now a new return value (agree) that will keep track of every time the measurement from the first qubit matches the measurement of the second qubit.

If we run this, we'll get exactly the same 50-50 result we got before. However, what we're really interested in is how the second qubit reacts to the first being measured. We'll add this statistic with a new version of the BellTest operation:


```C#
operation BellTest (count : Int, initial: Result) : (Int, Int, Int)
{
    mutable numOnes = 0;
    mutable agree = 0;
    using (qubits = Qubit[2])
    {
        for (test in 1..count)
        {
            Set (initial, qubits[0]);
            Set (Zero, qubits[1]);

            H(qubits[0]);
            CNOT(qubits[0], qubits[1]);
            let res = M (qubits[0]);

            if (M (qubits[1]) == res) 
            {
                set agree = agree + 1;
            }

            // Count the number of ones we saw:
            if (res == One)
            {
                set numOnes = numOnes + 1;
            }
        }

        Set(Zero, qubits[0]);
        Set(Zero, qubits[1]);
    }

    // Return number of times we saw a |0> and number of times we saw a |1>
    return (count-numOnes, numOnes, agree);
}
```

There is now a new return value (`agree`) that will keep track of every time the measurement from the first qubit matches the measurement of the second qubit.


Now when we run, we get something pretty amazing:

```sh
Init:Zero 0s=499  1s=501  agree=1000
Init:One  0s=490  1s=510  agree=1000
```

Our statistics for the first qubit haven't changed (50-50 chance of a 0 or a 1), but now when we measure the second qubit, it is always the same as what we measured for the first qubit. Our `CNOT` has entangled the two qubits, so that whatever happens to one of them, happens to the other. If you reversed the measurements (did the second qubit before the first), the same thing would happen. The first measurement would be random and the second would be in lock step with whatever was discovered for the first (!).

## Putting All Together
Running all the examples together we will get the `BellTestClassical` with qubits measurements for the classical measurement, `BellTestPauli` for the Pauli X gate (x), `BellTestHadamard` for Hadamard (H) gate (for superposition), `BellTestEntanglement` for the entanglement experiment:

```sh
Classical - Init:Zero 0s=1000 1s=0
Classical - Init:One  0s=0    1s=1000
Pauli - Init:Zero 0s=0    1s=1000
Pauli - Init:One  0s=1000 1s=0
Hadamard - Init:Zero 0s=516  1s=484
Hadamard - Init:One  0s=486  1s=514
Entanglement - Init:Zero 0s=468  1s=532  agree=1000
Entanglement - Init:One  0s=525  1s=475  agree=1000
```

# Quantum Theory
For more infor about the Pauli - `X` gate, the `H` - Hadamard gate
- [Quantum Basic Logic Gates](https://en.wikipedia.org/wiki/Quantum_logic_gate)