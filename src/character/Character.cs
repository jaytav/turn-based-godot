using Godot;

public partial class Character : Node2D
{
    [Signal]
    public delegate void TurnStartedEventHandler(Character character);

    [Signal]
    public delegate void TurnEndedEventHandler(Character character);

    public CharacterData Data
    {
        get { return _data; }
        set { setData(value); }
    }

    private CharacterData _data;

    public void StartTurn()
    {
        EmitSignal(nameof(TurnStarted), this);
    }

    public void EndTurn()
    {
        EmitSignal(nameof(TurnEnded), this);
    }

    private void setData(CharacterData data)
    {
        _data = data;

        // set character properties using character data
        Name = Data.Name;
        Position = Data.Position;
        GetNode<Sprite2D>("Sprite2D").Modulate = Data.Modulate;
    }
}
