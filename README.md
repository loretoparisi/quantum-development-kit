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



