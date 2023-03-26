using Godot;

public partial class Stat : Resource
{
    [Signal]
    public delegate void ValueUpdatedEventHandler();

    [Signal]
    public delegate void MaxValueUpdatedEventHandler();

    [Export]
    public int Value
    {
        get { return _value; }
        set { setValue(value); }
    }

    [Export]
    public int MaxValue
    {
        get { return _maxValue; }
        set { setMaxValue(value); }
    }

    private int _value;
    private int _maxValue;

    private void setValue(int value)
    {
        // only clamp if MaxValue is set
        if (MaxValue > 0 && value > MaxValue)
        {
            value = MaxValue;
        }

        _value = value;
        EmitSignal(nameof(ValueUpdated));
    }

    private void setMaxValue(int maxValue)
    {
        _maxValue = maxValue;
        EmitSignal(nameof(MaxValueUpdated), MaxValue);

        // if MaxValue decreases below Value, update Value
        if (Value > MaxValue)
        {
            Value = MaxValue;
        }
    }
}
