using Godot;

// CameraController
// handles camera movement and zoom
public partial class CameraController : Controller
{
    [Export]
    private int moveSpeed = 500;

    [Export]
    private int maxZoom = 5;

    [Export]
    private int minZoom = 1;

    public enum CameraMode
    {
        FreeMove,
        FollowNode,
    }

    private Camera2D _camera;
    private CameraMode _cameraMode = CameraMode.FreeMove;
    private Node2D _followNode;

	public override void Run()
	{
        _camera = GetNode<Camera2D>("/root/Main/World/Camera2D");

        Node characters = GetNode("/root/Main/World/Characters");
        characters.ChildEnteredTree += onCharactersChildEnteredTree;

        foreach (Character character in characters.GetChildren())
        {
            character.TurnStarted += onCharacterTurnStarted;
        }
	}

    public override void _Process(double delta)
    {
        Vector2 cameraTranslate = new Vector2();

        if (Input.IsActionPressed("CameraMoveLeft"))
        {
            cameraTranslate.X -= 1;
        }

        if (Input.IsActionPressed("CameraMoveRight"))
        {
            cameraTranslate.X += 1;
        }

        if (Input.IsActionPressed("CameraMoveUp"))
        {
            cameraTranslate.Y -= 1;
        }

        if (Input.IsActionPressed("CameraMoveDown"))
        {
            cameraTranslate.Y += 1;
        }

        if (cameraTranslate != Vector2.Zero)
        {
            _cameraMode = CameraMode.FreeMove;
        }

        if (_cameraMode == CameraMode.FreeMove)
        {
            _camera.Translate(cameraTranslate * (moveSpeed / _camera.Zoom.X) * (float)delta);
        }
        else if (_cameraMode == CameraMode.FollowNode)
        {
            _camera.Position = _followNode.Position;
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("CameraZoomIn"))
        {
            float zoom = Mathf.Min(_camera.Zoom.X + 0.5f, maxZoom);
            _camera.Zoom = new Vector2(zoom, zoom);
        }
        else if  (@event.IsActionPressed("CameraZoomOut"))
        {
            float zoom = Mathf.Max(_camera.Zoom.X - 0.5f, minZoom);
            _camera.Zoom = new Vector2(zoom, zoom);
        }
    }

    private void onCharactersChildEnteredTree(Node node)
    {
        ((Character)node).TurnStarted += onCharacterTurnStarted;
    }

    private void onCharacterTurnStarted(Character character)
    {
        _followNode = character;
        _cameraMode = CameraMode.FollowNode;
    }
}
