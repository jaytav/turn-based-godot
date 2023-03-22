using Godot;
using Godot.Collections;

// TileMapController
// handle tilemap functions
public partial class TileMapController : Controller
{
    public enum TileMapLayer
    {
        Select,
        Floor,
        Action,
    }

    public enum TileMapSource
    {
        Select,
        Floor,
        Action,
    }

    public Vector2I CurrentMapPosition;
    public Vector2 CurrentLocalPosition;

    private TileMap _tileMap;
    private Node2D _world;

	public override void Run()
	{
        _tileMap = GetNode<TileMap>("/root/Main/World/TileMap");
        _world = GetNode<Node2D>("/root/Main/World");
	}

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion)
        {
            Vector2 mouseLocalPosition = _world.GetGlobalMousePosition();
            Vector2I mouseMapPosition = _tileMap.LocalToMap(mouseLocalPosition);

            if (mouseMapPosition != CurrentMapPosition)
            {
                CurrentMapPosition = mouseMapPosition;
                CurrentLocalPosition = _tileMap.MapToLocal(CurrentMapPosition);
                _tileMap.ClearLayer((int)TileMapLayer.Select);
                _tileMap.SetCell((int)TileMapLayer.Select, mouseMapPosition, (int)TileMapSource.Select, Vector2I.Zero);
            }
        }
    }


    public void SetCells(Array<Vector2> cells, TileMapLayer layer, bool clear)
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
}
