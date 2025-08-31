using System;
using System.Collections.Generic;
using System.Linq;
using GdCore;

namespace HotHexes;

public enum HexOrientation
{
    FlatTop,
    PointyTop,
}

public class HexGridParameters
{
    public int GridWidth = 12; // Number of hexagons in width
    public int GridHeight = 4; // Number of hexagons in height
    public float Circumradius = 50f; // Radius of each hexagon
    public HexOrientation HexOrientation = HexOrientation.FlatTop;
}

public static class HexTools
{
    public static Path2D GenerateHexGridPath2D(HexGridParameters parameters)
    {
        // Clear existing curve if any
        Curve2D curve = new Curve2D();

        // Calculate hexagon vertices (6 points for a regular hexagon)
        Vector2[] hexVertices = new Vector2[6];
        for (int i = 0; i < 6; i++)
        {
            float angle = Mathf.DegToRad(60 * i);
            hexVertices[i] = new Vector2(
                parameters.Circumradius * Mathf.Cos(angle),
                parameters.Circumradius * Mathf.Sin(angle)
            );
        }

        // Generate grid
        for (int q = 0; q < parameters.GridWidth; q++)
        {
            for (int r = 0; r < parameters.GridHeight; r++)
            {
                // Calculate center of current hexagon using axial coordinates
                float x = parameters.Circumradius * 3f / 2f * q;
                float y = parameters.Circumradius * Mathf.Sqrt(3) * (r + (q % 2) * 0.5f);
                Vector2 center = new Vector2(x, y);

                // Add hexagon points to Path2D
                for (int i = 0; i <= 6; i++) // <= 6 to close the hexagon
                {
                    Vector2 point = center + hexVertices[i % 6];
                    curve.AddPoint(point);
                }
            }
        }

        var result = new Path2D();
        result.Curve = curve;
        return result;
    }

    public static Hexagon2D BuildHexagon2D(float circumradius)
    {
        Hexagon2D result = new Hexagon2D();
        result.Vertices = BuildHexVertices(circumradius, HexOrientation.FlatTop);
        return result;
    }

    public static Polygon2D[] GenerateHexGrid(Node parent, HexGridParameters hexParams)
    {
        if (hexParams.HexOrientation == HexOrientation.PointyTop)
            throw new NotImplementedException("Only flat top hexes supported right now");

        List<Polygon2D> hexagons = new List<Polygon2D>();
        Vector2[] hexVertices = BuildHexVertices(hexParams.Circumradius, hexParams.HexOrientation);
        float horizontalSpacing = 1.5f * hexParams.Circumradius;
        float verticalSpacing = MathF.Sqrt(3) * hexParams.Circumradius;
        for (int x = 0; x < hexParams.GridWidth; x++)
        {
            for (int y = 0; y < hexParams.GridHeight; y++)
            {
                float offset;
                if (x % 2 != 0)
                    offset = verticalSpacing / 2f;
                else
                    offset = 0;


                GD.Print("Hex tools adding polygon;");
                Vector2 position = new Vector2((x * horizontalSpacing), (y * verticalSpacing + offset));
                Hexagon2D hex = new Hexagon2D();
                parent.AddChild(hex);
                hex.Vertices = hexVertices.ToArray();
                hex.Position = position;
                hex.Owner = parent;
                hex.Name = $"hex {x}-{y}";
            }
        }

        return hexagons.ToArray();
    }

    /// <summary>
    /// Creates an array of six points in a hexagonal shape, centered around 0,0.
    /// </summary>
    /// <param name="circumradius"></param>
    /// <returns></returns>
    public static Vector2[] BuildHexVertices(float circumradius, HexOrientation orientation)
    {
        // Rotate by 30 degrees for pointy-top
        float rotation = orientation == HexOrientation.FlatTop ? 0 : 30;
        Vector2[] hexVertices = new Vector2[6];
        
        for (int i = 0; i < 6; i++)
        {
            float angle = Mathf.DegToRad(60 * i + rotation); 
            hexVertices[i] = new Vector2(
                circumradius * Mathf.Cos(angle),
                circumradius * Mathf.Sin(angle)
            );
        }

        return hexVertices;
    }

}