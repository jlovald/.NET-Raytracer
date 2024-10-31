namespace Raytracer.Common;

public class Tuple(double x = default, double y = default, double z = default, double w = default) : IEquatable<Tuple>
{
    public Tuple() : this(0,0,0,0)
    {
        
    }
    private const double EPSILON = 0.00001;
    public double X { get; } = x;
    public double Y { get; } = y;
    public double Z { get; } = z;
    public double W { get; } = w;

    public static Tuple Point(double x = default, double y = default, double z = default)
    {
        return new Tuple(x, y, z, TupleValues.PointComponent);
    }
    
    public static Tuple Vector(double x = default, double y = default, double z = default)
    {
        return new Tuple(x, y, z, TupleValues.VectorComponent);
    }
    
    public static Tuple Vector(double x = default, double y = default, double z = default, double w = default)
    {
        return new Tuple(x, y, z, TupleValues.VectorComponent);
    }

    public static Tuple Zero()
    {
        return new Tuple();
    }

    
    public TupleType Type => Math.Abs(W - 1.0) < EPSILON ? TupleType.Point : TupleType.Vector;
    public double Magnitude() => Math.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));

    public static Tuple operator +(Tuple a, Tuple b)
    {
        
        var result = new Tuple(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        if (result.W > TupleValues.PointComponent)
        {
            throw new InvalidTupleAdditionException("Cannot add two points.");
        }

        return result;
    }
    
    public static Tuple operator -(Tuple a, Tuple b)
    {
        
        var result = new Tuple(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        if (result.W < TupleValues.VectorComponent)
        {
            throw new InvalidTupleSubtractionException("Cannot subtract a point from a vector.");
        }

        return result;
    }
    
    public static Tuple operator -(Tuple a)
    {
        return Zero() - a;
    }
    
    public static Tuple operator *(Tuple a, double scalar)
    {
        return new Tuple(a.X * scalar, a.Y * scalar, a.Z * scalar, a.W * scalar);
    }
    
    public static Tuple operator *(Tuple a, Tuple b)
    {
        return new Tuple(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
    }
    
    public static Tuple operator /(Tuple a, double scalar)
    {
        return new Tuple(a.X / scalar, a.Y / scalar, a.Z / scalar, a.W / scalar);
    }
    
    public static bool operator ==(Tuple a, Tuple b)
    {
        return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;
    }

    public static bool operator !=(Tuple a, Tuple b)
    {
        return !(a == b);
    }

    public Tuple Normalize()
    {
        var magnitude = Magnitude;
        var vec = Vector(X / magnitude(), Y / magnitude(), Z / magnitude(), w / magnitude());
        return vec;
    }

    public double Dot(Tuple t)
    {
        return (X * t.X) +
               (Y * t.Y) +
               (Z * t.Z) +
               (W * t.W);
    }
    
    /// <summary>
    /// Gets a new vector that is perpendicular to both vectors.
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    /// <remarks>The order is important.</remarks>
    public Tuple Cross(Tuple t)
    {
        return new Tuple(Y * t.Z - Z * t.Y, Z * t.X - X * t.Z, X * t.Y - Y * t.X);
    }

    public bool Equals(Tuple? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Tuple)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z, W);
    }
}