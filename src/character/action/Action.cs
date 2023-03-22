using Godot;
using Godot.Collections;

public abstract partial class Action : Node
{
    [Export]
    private int _cost;

    public Character Character;
    public BoundStat ActionPoints;

    public abstract void Do(Dictionary context);

    public abstract Array<Vector2> GetCells();

    public override void _Ready()
    {
        Character = GetNode<Character>("../../");
        ActionPoints = Character.GetNode<BoundStat>("Stats/ActionPoints");
    }

    public virtual int Cost(Dictionary context)
    {
        return _cost;
    }
}
