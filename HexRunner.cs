using Godot;
using System;
using GdCore;
using HotHexes.addons.HexTools;

public partial class HexRunner : Node2D
{
    private ResPath toSave = "res://hexResults.tscn";

    public override void _Ready()
    {
        HexTools.GenerateHexGrid(this, new HexGridParameters());

        PackedScene packedScene = new PackedScene();

        // Pack the scene (the node and its children)
        Error error = packedScene.Pack(this);
        if (error != Error.Ok)
        {
            GD.PrintErr($"Failed to pack scene: {error}");
            return;
        }


        ResourceSaver.Save(packedScene, toSave.ToString());
        GetTree().Quit();
    }
}
