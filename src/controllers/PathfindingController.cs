using Godot;
using Godot.Collections;

// PathfindingController
// handles pathfinding for movement
public partial class PathfindingController : Controller
{
    private AStar2D _astar = new AStar2D();
    private TileMap _tilemap;

    private Dictionary<Vector2, long> _cellPointMap = new Dictionary<Vector2, long>();
    private Dictionary<long, Vector2> _pointCellMap = new Dictionary<long, Vector2>();

	public override void Run()
	{
        _tilemap = GetNode<TileMap>("/root/Main/World/TileMap");
        refreshAstar();
	}

    public Array<Vector2> GetPath(Vector2 from, Vector2 to)
    {
        Array<Vector2> path = new Array<Vector2>();

        if (_cellPointMap.ContainsKey(from) && _cellPointMap.ContainsKey(to))
        {
            Vector2[] pathCells = _astar.GetPointPath(_cellPointMap[from], _cellPointMap[to]);

            foreach (Vector2 pathCell in pathCells)
            {
                path.Add(pathCell);
            }
        }

        return path;
    }

    private void refreshAstar()
    {
        _astar.Clear();

        foreach (Vector2 cell in _tilemap.GetUsedCells((int)TileMapController.TileMapLayer.Floor))
        {
            long point = _astar.GetAvailablePointId();
            _astar.AddPoint(point, cell);
            _cellPointMap[cell] = point;
            _pointCellMap[point] = cell;
        }

        foreach (long point in _astar.GetPointIds())
        {
            Vector2 cell = _pointCellMap[point];

            Array<Vector2> relativeCells = new Array<Vector2>() {
                cell + Vector2.Left,
                cell + Vector2.Right,
                cell + Vector2.Up,
                cell + Vector2.Down,
            };

            foreach (Vector2 relativeCell in relativeCells)
            {
                // not in cell point map, floor doesn't exist
                if (!_cellPointMap.ContainsKey(relativeCell))
                {
                    continue;
                }

                long relativePoint = _cellPointMap[relativeCell];
                _astar.ConnectPoints(point, relativePoint);
            }
        }
    }
}
