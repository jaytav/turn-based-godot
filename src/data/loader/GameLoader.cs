using Godot;
using System;

public partial class GameLoader
{
    public static void Load(Main main)
    {
        // get game data
        GameData gameData = ResourceLoader.Load<GameData>("data/Main.tres");
        GD.Print($"GameLoader: GameData Loaded ({gameData.Name})");

        // load game data
        // load character data
        foreach (CharacterData characterData in gameData.Characters)
        {
            Character character = CharacterLoader.LoadCharacter(characterData);
            main.GetNode("World/Characters").AddChild(character);
            GD.Print($"GameLoader: Spawning Character ({characterData.Name}) in Position {characterData.Position}");
        }
    }
}
