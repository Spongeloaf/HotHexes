
using System.Collections.Generic;

namespace HotHexes;

[Tool]
[GlobalClass]
public partial class HexagonComboTile : Node2D
{
    public List<Hexagon2D> Hexes = [];

    [Export] public float Circumradius = 50f;

    public override void _Ready()
    {
        Hexagon2D firstHex = HexTools.BuildHexagon2D(Circumradius);
        AddChild(firstHex);
        firstHex.Owner = this;
        firstHex.Position = Vector2.One;
        this.PrintTreePretty();
        int i = 0;
    }
}