using Godot;

public partial class CharacterLoader
{
    public static PackedScene Character = ResourceLoader.Load<PackedScene>("src/character/Character.tscn");

    public static Character LoadCharacter(CharacterData data)
    {
        GD.Print($"\tCharacterLoader: Loading ({data.Name})");
        Character character = Character.Instantiate<Character>();
        character.Data = data;

        // connect character turn ended to their effects
        foreach (Effect effect in data.Effects.Values)
        {
            character.TurnEnded += effect.Do;
        }

        return character;
    }

    public static void ApplyDefaults(CharacterData data)
    {
        if (!data.Stats.ContainsKey("HealthPoints"))
        {
            Stat healthPoints = new Stat();
            healthPoints.Value = 3;
            healthPoints.MaxValue = 3;
            data.Stats["HealthPoints"] = healthPoints;
        }

        if (!data.Stats.ContainsKey("ActionPoints"))
        {
            Stat actionPoints = new Stat();
            actionPoints.Value = 3;
            actionPoints.MaxValue = 3;
            data.Stats["ActionPoints"] = actionPoints;
        }

        if (!data.Stats.ContainsKey("ReplenishActionPoints"))
        {
            ReplenishStat effect = new ReplenishStat();
            effect.Stat = "ActionPoints";
            data.Effects["ReplenishActionPoints"] = effect;
        }
    }
}
