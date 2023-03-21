using Godot;

public partial class ReplenishBoundStat : Effect
{
    [Export]
    private BoundStat _stat;

    public override void Do()
    {
        _stat.Value = _stat.MaxValue;
    }
}
