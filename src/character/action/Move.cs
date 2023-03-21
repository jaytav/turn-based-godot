using Godot;
using Godot.Collections;

public partial class Move : Action
{
    public override void Do(Dictionary context)
    {
        Character.Position = (Vector2)context["LocalPosition"];
    }
}
