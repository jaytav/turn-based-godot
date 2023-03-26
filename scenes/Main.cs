using Godot;

public partial class Main : Node
{
	public override void _Ready()
	{
        GD.Print("Main: _Ready()");
        GameLoader.Load(this);
        runControllers();
	}

    private void runControllers()
    {
        GD.Print("Main: runControllers()");

        foreach (Controller controller in GetNode("Controllers").GetChildren())
        {
            GD.Print($"{controller.Name}: Run()");
            controller.Run();
        }
    }
}
