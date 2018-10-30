# Quantum Computing Examples

# How to install on macOS
- Install .NET 2, https://www.microsoft.com/net/
- Install VisualStudioCode, https://code.visualstudio.com/
- Open VisualStudioCode and install the extension Microsoft **Quantum Development Kit**
- Install the Quantum Template

```sh
dotnet new -i "Microsoft.Quantum.ProjectTemplates::0.3.1810.2508-preview"
```

You should now see the installed templates for Q# like

```sh
Console Application                               console            [C#], F#, Q#, VB      Common/Console
```

# How to Run the Examples
To run the examples, enter the examples folder and run

```sh
dotnet run
```

# How to Create a New Q# Project
To create a new Quantum Develpoment Kit project, enter the projects root folder and enter

```sh
dotnet new console -lang Q# --output Bell
```
This will create the project folder `Bell` in the current folder and the Quantum Simulator for `Q#`.

# .NET Tips & Tricks

## .NET CLI Language
To set the .NET cli messages to english please specify this env

```sh
export DOTNET_CLI_UI_LANGUAGE=en
```

# Quantum Development Kit
- [Writing a Quantum Program](https://docs.microsoft.com/en-us/quantum/quickstart?view=qsharp-preview&tabs=tabid-vscode)

# Quantum Theory
- [Quantum Basic Logic Gates](https://en.wikipedia.org/wiki/Quantum_logic_gate)
- A convenient tool for visualizing the effect of gates on qubit states, [Quirk](http://algassert.com/quirk)






