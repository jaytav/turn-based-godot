using Godot;

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

    public TileMap TileMap;

    private Vector2I _currentMapPosition;
    private Node2D _world;

	public override void Run()
	{
        TileMap = GetNode<TileMap>("/root/Main/World/TileMap");
        _world = GetNode<Node2D>("/root/Main/World");
	}

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion)
        {
            Vector2 mouseLocalPosition = _world.GetGlobalMousePosition();
            Vector2I mouseMapPosition = TileMap.LocalToMap(mouseLocalPosition);

            if (mouseMapPosition != _currentMapPosition)
            {
                _currentMapPosition = mouseMapPosition;
                TileMap.ClearLayer((int)TileMapLayer.Select);
                TileMap.SetCell((int)TileMapLayer.Select, mouseMapPosition, (int)TileMapSource.Select, Vector2I.Zero);
            }
        }
    }
}
