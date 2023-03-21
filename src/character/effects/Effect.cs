using Godot;

public abstract partial class Effect : Node
{
    public abstract void Do();

    public override void _Ready()
    {
        GetNode<Character>("../../").TurnEnded += onCharacterTurnEnded;
    }

    private void onCharacterTurnEnded(Character character)
    {
        Do();
    }
}
