using Godot;

public partial class ReplenishStat : Effect
{
    public string Stat;

    public override void Do(Character character)
    {
        character.Data.Stats[Stat].Value = character.Data.Stats[Stat].MaxValue;
    }
}
