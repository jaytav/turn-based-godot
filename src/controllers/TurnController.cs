using Godot;
using Godot.Collections;

// TurnController
// handles the turns of characters
public partial class TurnController : Controller
{
	private Character _activeCharacter;
    private Array<Character> _characters = new Array<Character>();

    public override void Run()
    {
        Node characters = GetNode("/root/Main/World/Characters");
        characters.ChildEnteredTree += onCharactersChildEnteredTree;
        characters.ChildExitingTree += onCharactersChildExitingTree;

        foreach (Character character in characters.GetChildren())
        {
            _characters.Add(character);
        }

        startNextTurn();
    }

    private void startNextTurn()
    {
        if (_activeCharacter != null)
        {
            _characters.Add(_activeCharacter);
            _activeCharacter.TurnEnded -= onActiveCharacterTurnEnded;
        }

        _activeCharacter = _characters[0];
        _characters.Remove(_activeCharacter);
        _activeCharacter.TurnEnded += onActiveCharacterTurnEnded;
        GD.Print($"TurnController: {_activeCharacter.Name} StartTurn");
        _activeCharacter.StartTurn();
    }

    private void onCharactersChildEnteredTree(Node node)
    {
        _characters.Add((Character)node);
    }

    private void onCharactersChildExitingTree(Node node)
    {
        _characters.Remove((Character)node);
    }

    private void onActiveCharacterTurnEnded(Character character)
    {
        GD.Print($"TurnController: {_activeCharacter.Name} EndTurn");
        startNextTurn();
    }
}
