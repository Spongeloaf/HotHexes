using GdCore;
using HotHexes;

public partial class HexRunner : Node2D
{
    private ResPath toSave = "res://hexResults.tscn";

    public override void _Ready()
    {
        var child = new HexagonComboTile();
        AddChild(child);
        

        PackedScene packedScene = new PackedScene();

        // Pack the scene (the node and its children)
        Error error = packedScene.Pack(child);
        if (error != Error.Ok)
        {
            GD.PrintErr($"Failed to pack scene: {error}");
            return;
        }


        ResourceSaver.Save(packedScene, toSave.ToString());
        GetTree().Quit();
    }
}
