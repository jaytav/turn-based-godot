using Godot;

public partial class Main : Node
{
    private GameData _gameData;

	public override void _Ready()
	{
        GD.Print("Main: _Ready()");
        _gameData = GameLoader.Load(this);
        runControllers();
	}

    private void runControllers()
    {
        GD.Print("Main: runControllers()");

        foreach (Controller controller in GetNode("Controllers").GetChildren())
        {
            GD.Print($"\t{controller.Name}: Run()");
            controller.Run();
        }
    }

    public override void _Notification(int what)
    {
        if (what == NotificationWMCloseRequest)
        {
            GameSaver.Save(_gameData);
        }
    }
}
