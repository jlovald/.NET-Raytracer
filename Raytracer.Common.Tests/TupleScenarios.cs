using System.Drawing;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Raytracer.Common.Tests;

public class TupleScenarios
{
    [Theory]
    [InlineData(4.3, 4.2, 3.1, TupleValues.PointComponent, TupleType.Point)]
    [InlineData(4.3, 4.2, 3.1, TupleValues.VectorComponent, TupleType.Vector)]
    public void When_w_equals_one_should_be_point(double x, double y, double z, double w, TupleType expectedType)
    {
        var actualTuple = new TupleBuilder().WithX(x).WithY(y).WithZ(z).WithW(w).Build();
        
        actualTuple.Type.Should().Be(expectedType);
    }

    [Theory]
    [InlineData(4.3, 4.2, 3.1, 1.0)]
    public void When_initializing_should_set_correct_values(double expectedX, double expectedY, double expectedZ,
        double expectedW)
    {
        var actualTuple = new TupleBuilder().WithX(expectedX).WithY(expectedY).WithZ(expectedZ).WithW(expectedW)
            .Build();
        using (new AssertionScope())
        {
            actualTuple.X.Should().Be(expectedX);
            actualTuple.Y.Should().Be(expectedY);
            actualTuple.Z.Should().Be(expectedZ);
            actualTuple.W.Should().Be(expectedW);
        }
    }

    [Fact]
    public void When_creating_point_should_create_point()
    {
        var actualPoint = Tuple.Point(4, -4, 3);
        using (new AssertionScope())
        {
            actualPoint.X.Should().Be(4);
            actualPoint.Y.Should().Be(-4);
            actualPoint.Z.Should().Be(3);
            actualPoint.W.Should().Be(1.0);
        }
    }
    
    [Fact]
    public void When_creating_vector_should_create_point()
    {
        var actualPoint = Tuple.Vector(4, -4, 3);
        using (new AssertionScope())
        {
            actualPoint.X.Should().Be(4);
            actualPoint.Y.Should().Be(-4);
            actualPoint.Z.Should().Be(3);
            actualPoint.W.Should().Be(0.0);
        }
    }
}

public static class TupleValues
{
    public const double PointComponent = 1.0;
    public const double VectorComponent = 0.0;

}
public class Tuple(double x = default, double y = default, double z = default, double w = default)
{
    private const double EPSILON = 0.00001;
    public double X { get; private set; } = x;
    public double Y { get; private set; } = y;
    public double Z { get; private set; } = z;
    public double W { get; private set; } = w;

    public static Tuple Point(double x = default, double y = default, double z = default)
    {
        return new Tuple(x, y, z, TupleValues.PointComponent);
    }
    
    public static Tuple Vector(double x = default, double y = default, double z = default)
    {
        return new Tuple(x, y, z, TupleValues.VectorComponent);
    }
    
    public TupleType Type => Math.Abs(W - 1.0) < EPSILON ? TupleType.Point : TupleType.Vector;
}

internal class TupleBuilder
{

    private double _x = 0;
    private double _y = 0;
    private double _z = 0;
    private double _w = 0;
    
   
    public TupleBuilder WithX(double x)
    {
        _x = x;
        return this;
    }
    
    public TupleBuilder WithY(double y)
    {
        _y = y;
        return this;
    }
    
    public TupleBuilder WithZ(double z)
    {
        _z = z;
        return this;
    }
    
    public TupleBuilder WithW(double w)
    {
        _w = w;
        return this;
    }

    public Tuple Build()
    {
        return new Tuple(_x, _y, _z, _w);
    }
}

public enum TupleType{
    Vector,
    Point
}