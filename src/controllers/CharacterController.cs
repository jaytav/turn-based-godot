using Godot;
using Godot.Collections;

// CharacterController
// handles character actions
public partial class CharacterController : Controller
{
    private Character _activeCharacter;
    private Action _activeAction;
    private TileMapController _tileMapController;
    private TileMap _tilemap;

    public override void Run()
    {
        _tileMapController = GetNode<TileMapController>("/root/Main/Controllers/TileMapController");
        _tilemap = GetNode<TileMap>("/root/Main/World/TileMap");

        Node characters = GetNode("/root/Main/World/Characters");
        characters.ChildEnteredTree += onCharactersChildEnteredTree;

        foreach (Character character in characters.GetChildren())
        {
            character.TurnStarted += onCharacterTurnStarted;
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (_activeCharacter == null)
        {
            return;
        }

        if (@event.IsActionPressed("EndTurn"))
        {
            _activeCharacter.EndTurn();
        }
        else if (@event.IsActionPressed("PrimaryAction"))
        {
            Array<Vector2> cells = _activeAction.GetCells();
            Dictionary context = getActionContext();
            int actionCost = _activeAction.Cost(context);

            if (!cells.Contains((Vector2)context["MapPosition"]))
            {
                GD.Print($"CharacterController: action not within range");
                return;
            }

            if (actionCost > _activeAction.ActionPoints.Value) {
                GD.Print($"CharacterController: Not enough action points to do action: {_activeAction.Name}");
                return;
            }

            _activeAction.Do(context);

            // remove action cost from action points
            _activeAction.ActionPoints.Value -= actionCost;

            // draw action range
            _tileMapController.SetCells(_activeAction.GetCells(), TileMapController.TileMapLayer.Action, true);
        }
    }

    private Dictionary getActionContext()
    {
        return new Dictionary() {
            {"LocalPosition", _tileMapController.CurrentLocalPosition},
            {"MapPosition", _tileMapController.CurrentMapPosition},
        };
    }

    private void onCharactersChildEnteredTree(Node node)
    {
        ((Character)node).TurnStarted += onCharacterTurnStarted;
    }

    private void onCharacterTurnStarted(Character character)
    {
        _activeCharacter = character;

        // default active action is the first action in the actions node tree
        _activeAction = _activeCharacter.GetNode("Actions").GetChild<Action>(0);

        // draw default action range
        _tileMapController.SetCells(_activeAction.GetCells(), TileMapController.TileMapLayer.Action, true);
    }
}
