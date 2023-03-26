using Godot;
using Godot.Collections;

// Game Data
// Handles game save data
public partial class GameData : Resource
{
    [Export]
    public string Name;

    [Export]
    public Array<CharacterData> Characters = new Array<CharacterData>();
}
