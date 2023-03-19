using Godot;
using System;

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

    private void setValue(int value)
    {
        _value = value;
        EmitSignal(nameof(ValueUpdated));
    }
}
