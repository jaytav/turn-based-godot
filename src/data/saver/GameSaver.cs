using Godot;
using System;

public partial class GameSaver
{
    public static void Save(GameData data)
    {
        ResourceSaver.Save(data, $"data/{data.Name}.tres");
        GD.Print($"GameSaver: Saving Game ({data.Name})");
    }
}
