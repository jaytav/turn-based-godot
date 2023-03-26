using Godot;
using Godot.Collections;

// Character Data
// Handles character save data
public partial class CharacterData : Resource
{
    [Export]
    public string Name;

    [Export]
    public Vector2 Position = new Vector2();

    [Export]
    public Color Modulate = Colors.White;

    [Export]
    public Dictionary<string, Stat> Stats = new Dictionary<string, Stat>();

    [Export]
    public Dictionary<string, Effect> Effects = new Dictionary<string, Effect>();
}
