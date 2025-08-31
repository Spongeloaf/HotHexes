
namespace HotHexes.addons.hextools;

/// <summary>
/// Wraps around a base Hexagon class with drawing and node functionality.
/// </summary>
[Tool]
public partial class Hexagon2D : Node2D
{
    //////////////////////////////////////////////////////////
    // Please do not add any hexagon specific code here!    //
    // Put it all in the base hexagon class and expose      //
    // it by forwarding the properties here.                //
    //////////////////////////////////////////////////////////

    private Hexagon hexagon = new Hexagon();
    private int counter = 0;

    [Export]
    public Vector2[] Vertices
    {
        get => hexagon.Vertices;
        set => hexagon.Vertices = value;
    }

    public override void _Draw()
    {
        for (int i = 0; i < Vertices.Length; i++)
        {
            Vector2 start = Vertices[i];
            Vector2 end = Vertices[(i + 1) % Vertices.Length]; // Loop back to first vertex
            DrawLine(start, end, Colors.White, 1);
        }
    }
}