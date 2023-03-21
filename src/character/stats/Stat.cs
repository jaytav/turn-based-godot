using Godot;

public partial class Stat : Node
{
    [Signal]
    public delegate void ValueUpdatedEventHandler();

    [Export]
    public int Value
    {
        get { return _value; }
        set { setValue(value); }
    }

    private int _value;

    protected virtual void setValue(int value)
    {
        _value = value;
        EmitSignal(nameof(ValueUpdated));
    }
}
