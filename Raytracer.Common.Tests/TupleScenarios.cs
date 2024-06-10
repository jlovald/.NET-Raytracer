using FluentAssertions;
using FluentAssertions.Execution;

namespace Raytracer.Common.Tests;

public class TupleScenarios
{
    [Theory]
    [InlineData(4.3, 4.2, 3.1, 1.0, TupleType.Point)]
    public void When_w_equals_one_should_be_point(double x, double y, double z, double w, TupleType expectedType)
    {
        var actualTuple = new TupleBuilder().WithX(x).WithY(y).WithZ(z).WithW(w).Build();
        
        actualTuple.Type.Should().Be(expectedType);
    }

    [Theory]
    [InlineData(4.3, 4.2, 3.1, 1.0)]
    public void When_initializing_should_set_correct_values(double expectedX, double expectedY, double expectedZ, double expectedW)
    {
        var actualTuple = new TupleBuilder().WithX(expectedX).WithY(expectedY).WithZ(expectedZ).WithW(expectedW).Build();
        using (new AssertionScope())
        {
            actualTuple.X.Should().Be(expectedX);
            actualTuple.Y.Should().Be(expectedY);
            actualTuple.Z.Should().Be(expectedZ);
            actualTuple.W.Should().Be(expectedW);
        }
    }
}

public class Tuple(double x = default, double y = default, double z = default, double w = default)
{
    public double X { get; private set; } = x;
    public double Y { get; private set; } = y;
    public double Z { get; private set; } = z;
    public double W { get; private set; } = w;

    public TupleType Type => Math.Abs(w - 1.0) < 1e-6 ? TupleType.Point : TupleType.Vector;
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