namespace Raytracer.Common;

public class Color
{
    private Tuple _rgb;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="red">0-1</param>
    /// <param name="green">0-1</param>
    /// <param name="blue">0-1</param>
    public Color(double red, double green, double blue)
    {
        _rgb = Tuple.Vector(red, green, blue);
    }

    public Color(Color c)
    {
        var vector = c.GetColorTuple;
        _rgb = Tuple.Vector(vector.X, vector.Y, vector.Z);
    }
    
    public Color(Tuple color)
    {
        _rgb = Tuple.Vector(color.X, color.Y, color.Z);
    }

    public static Color Black => new Color(0, 0, 0);
    
    public static Color operator +(Color a, Color b)
    {
        return new Color(a.GetColorTuple + b.GetColorTuple);
    }
    
    public static Color operator -(Color a, Color b)
    {
        return new Color(a.GetColorTuple - b.GetColorTuple);
    }
    
    public static Color operator *(Color a, double d)
    {
        return new Color(a.GetColorTuple * d);
    }
    
    public static Color operator *(Color a, int i)
    {
        return new Color(a.GetColorTuple * i);
    }
    
    public static Color operator *(Color a, Color b)
    {
        return new Color(a.GetColorTuple * b.GetColorTuple);
    }
  
    internal Tuple GetColorTuple => _rgb;

    public double Red => _rgb.X;
    public double Green => _rgb.Y;
    public double Blue => _rgb.Z;
}