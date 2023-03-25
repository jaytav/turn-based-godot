using Godot;
using Godot.Collections;

// TileMapController
// handle tilemap functions
public partial class TileMapController : Controller
{
    [Signal]
    public delegate void CurrentMapPositionUpdatedEventHandler(Vector2 position);

    public enum TileMapId
    {
        Select,
        Floor,
        Move,
        MoveSecondary,
    }

    public Vector2 CurrentMapPosition;
    public Vector2 CurrentLocalPosition;

    private TileMap _tileMap;
    private Node2D _world;

	public override void Run()
	{
        _tileMap = GetNode<TileMap>("/root/Main/World/TileMap");
        _world = GetNode<Node2D>("/root/Main/World");
        CurrentMapPositionUpdated += onCurrentMapPositionUpdated;
	}

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion)
        {
            Vector2 mouseLocalPosition = _world.GetGlobalMousePosition();
            Vector2I mouseMapPosition = _tileMap.LocalToMap(mouseLocalPosition);

            if (mouseMapPosition != CurrentMapPosition)
            {
                EmitSignal("CurrentMapPositionUpdated", mouseMapPosition);
            }
        }
    }

    public void SetCells(Array<Vector2> cells, TileMapId layer, bool clear)
    {
        if (clear)
        {
            _tileMap.ClearLayer((int)layer);
        }

        foreach (Vector2 cell in cells)
        {
            _tileMap.SetCell((int)layer, (Vector2I)cell, (int)layer, Vector2I.Zero);
        }
    }

    private void onCurrentMapPositionUpdated(Vector2 position)
    {
        CurrentMapPosition = position;
        CurrentLocalPosition = _tileMap.MapToLocal((Vector2I)CurrentMapPosition);
        _tileMap.ClearLayer((int)TileMapId.Select);
        _tileMap.SetCell((int)TileMapId.Select, (Vector2I)CurrentMapPosition, (int)TileMapId.Select, Vector2I.Zero);
    }
}
