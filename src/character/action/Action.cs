using Godot;
using Godot.Collections;

public abstract partial class Action : Node
{
    [Export]
    private int _cost;

    public Character Character;
    public BoundStat ActionPoints;

    public TileMapController.TileMapId TileMapId = TileMapController.TileMapId.Move;
    public TileMapController.TileMapId TileMapSecondaryId = TileMapController.TileMapId.MoveSecondary;

    public abstract void Do(Dictionary context);

    public abstract Array<Vector2> GetCells();

    public abstract Array<Vector2> GetSecondaryCells(Vector2 position);

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
