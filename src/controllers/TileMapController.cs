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
    }

    public enum TileMapSource
    {
        Select,
        Floor,
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
}
