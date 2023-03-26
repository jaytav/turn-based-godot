using Godot;
using System;

public partial class CharacterLoader
{
    public static PackedScene Character = ResourceLoader.Load<PackedScene>("src/character/Character.tscn");

    public static Character LoadCharacter(CharacterData data)
    {
        Character character = Character.Instantiate<Character>();
        character.Name = data.Name;
        character.Position = data.Position;
        character.Modulate = data.Modulate;

        return character;
    }
}
