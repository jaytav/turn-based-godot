using Godot;
using Godot.Collections;

public partial class Move : Action
{
    private PathfindingController _pathfindingController;
    private TileMap _tilemap;

    public override void _Ready()
    {
        base._Ready();
        _pathfindingController = GetNode<PathfindingController>("/root/Main/Controllers/PathfindingController");
        _tilemap = GetNode<TileMap>("/root/Main/World/TileMap");
    }

    public override void Do(Dictionary context)
    {
        Character.Position = (Vector2)context["LocalPosition"];
    }

    public override int Cost(Dictionary context)
    {
        Vector2 characterCell = _tilemap.LocalToMap(Character.Position);
        Array<Vector2> path = _pathfindingController.GetPath(characterCell, (Vector2)context["MapPosition"]);

        // cost is path count - character cell
        return path.Count - 1;
    }

    public override Array<Vector2> GetCells()
    {
        Array<Vector2> cells = new Array<Vector2>();
        Vector2 characterCell = _tilemap.LocalToMap(Character.Position);

        foreach (Vector2 moveableCell in _pathfindingController.MovableCells)
        {
            Array<Vector2> path = _pathfindingController.GetPath(characterCell, moveableCell);

            // path longer than available action points
            if (path.Count - 1 > ActionPoints.Value)
            {
                continue;
            }

            cells.Add(moveableCell);
        }

        return cells;
    }

    public override Array<Vector2> GetSecondaryCells(Vector2 position)
    {
        Vector2 characterCell = _tilemap.LocalToMap(Character.Position);
        Array<Vector2> cells = _pathfindingController.GetPath(characterCell, position);
        cells.Remove(characterCell);

        return cells;
    }
}
