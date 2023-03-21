using Godot;

public partial class BoundStat : Stat
{
    [Signal]
    public delegate void MaxValueUpdatedEventHandler();

    [Signal]
    public delegate void MinValueUpdatedEventHandler();

    [Export]
    public int MaxValue
    {
        get { return _maxValue; }
        set { setMaxValue(value); }
    }

    [Export]
    public int MinValue
    {
        get { return _minValue; }
        set { setMinValue(value); }
    }

    private int _maxValue;
    private int _minValue;

    public override void _Ready()
    {
        // if MaxValue set, set Value to MaxValue
        if (MaxValue > 0)
        {
            Value = MaxValue;
            return;
        }

        base._Ready();
    }

    protected override void setValue(int value)
    {
        base.setValue(Mathf.Clamp(value, MinValue, MaxValue));
    }

    private void setMaxValue(int maxValue)
    {
        _maxValue = maxValue;
        EmitSignal(nameof(MaxValueUpdated), MaxValue);
    }

    private void setMinValue(int minValue)
    {
        _minValue = minValue;
        EmitSignal(nameof(MinValueUpdated), MinValue);
    }
}
