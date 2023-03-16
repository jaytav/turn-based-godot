using Godot;

// CharacterController
// handles character actions
public partial class CharacterController : Controller
{
    private Character _activeCharacter;

    public override void Run()
    {
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
    }

    private void onCharactersChildEnteredTree(Node node)
    {
        ((Character)node).TurnStarted += onCharacterTurnStarted;
    }

    private void onCharacterTurnStarted(Character character)
    {
        _activeCharacter = character;
    }
}
