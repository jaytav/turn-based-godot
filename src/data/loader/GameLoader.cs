using Godot;

public partial class GameLoader
{
    public static GameData Load(Main main)
    {
        // get game data
        GameData gameData = ResourceLoader.Load<GameData>("data/Main.tres");
        GD.Print($"GameLoader: Loading ({gameData.Name})");

        // load game data
        // load character data
        foreach (CharacterData characterData in gameData.Characters)
        {
            CharacterLoader.ApplyDefaults(characterData);
            Character character = CharacterLoader.LoadCharacter(characterData);
            main.GetNode("World/Characters").AddChild(character);
        }

        return gameData;
    }
}
