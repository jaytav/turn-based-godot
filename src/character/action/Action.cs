using Godot;
using Godot.Collections;

public abstract partial class Action : Node
{
    [Export]
    private int _cost;

    public Character Character;

    public abstract void Do(Dictionary context);

    public override void _Ready()
    {
        Character = GetNode<Character>("../../");
    }

    public virtual int Cost(Dictionary context)
    {
        return _cost;
    }
}
