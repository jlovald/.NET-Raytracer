using System.Drawing;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualBasic.CompilerServices;
using Raytracer.Common.Tests;

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

    [Theory]
    [InlineData(3, -2, 5, 1, -2, 3, 1, 0, TupleType.Point)]
    [InlineData(3, -2, 5, 0, -2, 3, 1, 0, TupleType.Vector)]
    public void When_adding_a_vector_to_a_vector_components_should_be_added(double x1, double y1, double z1, double w1,
        double x2, double y2, double z2, double w2, TupleType expectedType)
    {
        var from = new TupleBuilder().WithX(x1).WithY(y1).WithZ(z1).WithW(w1).Build();
        var to = new TupleBuilder().WithX(x2).WithY(y2).WithZ(z2).WithW(w2).Build();

        var actualResult = from + to;
        using (new AssertionScope())
        {
            actualResult.X.Should().Be(x1 + x2);
            actualResult.Y.Should().Be(y1 + y2);
            actualResult.Z.Should().Be(z1 + z2);
            actualResult.W.Should().Be(w1 + w2);
            actualResult.Type.Should().Be(expectedType);
        }
    }

    [Fact]
    public void Subtracting_two_points_results_in_vector()
    {
        var a = Tuple.Point(3, 2, 1);
        var b = Tuple.Point(5, 6, 7);

        var result = a - b;
        using (new AssertionScope())
        {
            result.X.Should().Be(-2);
            result.Y.Should().Be(-4);
            result.Z.Should().Be(-6);
            result.W.Should().Be(0);
            result.Type.Should().Be(TupleType.Vector);
        }
    }

    [Fact]
    public void Subtracting_vector_from_point_should_be_point()
    {
        var a = Tuple.Point(3, 2, 1);
        var b = Tuple.Vector(5, 6, 7);

        var result = a - b;
        using (new AssertionScope())
        {
            result.X.Should().Be(-2);
            result.Y.Should().Be(-4);
            result.Z.Should().Be(-6);
            result.W.Should().Be(1);
            result.Type.Should().Be(TupleType.Point);
        }
    }

    [Fact]
    public void Subtracting_vector_from_vector_should_be_vector()
    {
        var a = Tuple.Vector(3, 2, 1);
        var b = Tuple.Vector(5, 6, 7);

        var result = a - b;
        using (new AssertionScope())
        {
            result.X.Should().Be(-2);
            result.Y.Should().Be(-4);
            result.Z.Should().Be(-6);
            result.W.Should().Be(0);
            result.Type.Should().Be(TupleType.Vector);
        }
    }

    [Fact]
    public void Subtracting_point_from_vector_should_throw_exception()
    {
        var a = Tuple.Vector(3, 2, 1);
        var b = Tuple.Point(5, 6, 7);

        var action = () => (a - b);
        action.Should().Throw<InvalidTupleSubtractionException>();
    }

    [Fact]
    public void Adding_two_points_should_throw_exception()
    {

        Action act = () => { _ = (Tuple.Point() + Tuple.Point()); };

        act.Should().Throw<InvalidTupleAdditionException>().WithMessage("Cannot add two points.");
    }

    [Fact]
    public void Subtracting_a_vector_from_the_zero_vector_inverts_it()
    {
        var vector = Tuple.Vector(1, -2, 3);
        var zeroVector = Tuple.Zero();

        var resultVector = zeroVector - vector;

        using (new AssertionScope())
        {
            resultVector.X.Should().Be(-1);
            resultVector.Y.Should().Be(2);
            resultVector.Z.Should().Be(-3);
            resultVector.W.Should().Be(0);
            resultVector.Type.Should().Be(TupleType.Vector);
        }
    }

    [Fact]
    public void Negating_a_tuple_inverts_it()
    {
        var vector = Tuple.Vector(1, -2, 3);

        var resultVector = -vector;

        using (new AssertionScope())
        {
            resultVector.X.Should().Be(-1);
            resultVector.Y.Should().Be(2);
            resultVector.Z.Should().Be(-3);
            resultVector.W.Should().Be(0);
            resultVector.Type.Should().Be(TupleType.Vector);
        }
    }

    [Fact]
    public void Multiplying_a_tuple_by_a_scalar()
    {
        var vector = new Tuple(1, -2, 3, -4);

        var resultingTuple = vector * 3.5;

        using (new AssertionScope())
        {
            resultingTuple.X.Should().Be(3.5);
            resultingTuple.Y.Should().Be(-7);
            resultingTuple.Z.Should().Be(10.5);
            resultingTuple.W.Should().Be(-14);
            resultingTuple.Type.Should().Be(TupleType.Vector);
        }
    }

    [Fact]
    public void Multiplying_a_tuple_by_a_fraction()
    {
        var vector = new Tuple(1, -2, 3, -4);

        var resultingTuple = vector * 0.5;

        using (new AssertionScope())
        {
            resultingTuple.X.Should().Be(0.5);
            resultingTuple.Y.Should().Be(-1);
            resultingTuple.Z.Should().Be(1.5);
            resultingTuple.W.Should().Be(-2);
            resultingTuple.Type.Should().Be(TupleType.Vector);
        }
    }

    [Fact]
    public void Dividing_a_tuple_by_a_scalar()
    {
        var tuple = new Tuple(1, -2, 3, -4);

        var resultingTuple = tuple / 2;

        using (new AssertionScope())
        {
            resultingTuple.X.Should().Be(0.5);
            resultingTuple.Y.Should().Be(-1);
            resultingTuple.Z.Should().Be(1.5);
            resultingTuple.W.Should().Be(-2);
            resultingTuple.Type.Should().Be(TupleType.Vector);
        }
    }

    //  TODO throw exception if doing magnitude of point
    [Theory]
    [InlineData(1, 0, 0, 1)]
    [InlineData(0, 1, 0, 1)]
    [InlineData(0, 0, 1, 1)]
    [InlineData(1, 2, 3, 3.741657)]
    [InlineData(-1, -2, -3, 3.741657)]
    public void Computing_the_magnitude_of_a_vector(double x, double y, double z, double expectedMagnitude)
    {
        var vector = Tuple.Vector(x, y, z);

        var actualMagnitude = vector.Magnitude();

        actualMagnitude.Should().BeApproximately(expectedMagnitude, 0.001);
    }

    // Normalization
    [Theory]
    [InlineData(4,0,0, 1, 0, 0)]
    [InlineData(1,2,3, 0.26726, 0.53452, 0.80178)]
    public void Normalizing_Vector(double x, double y, double z, double normalizedX, double normalizedY, double normalizedZ)
    {
        var vector = Tuple.Vector(x, y, z);
        var actualNormalizedVector = vector.Normalize();
        var expectedVector = Tuple.Vector(normalizedX, normalizedY, normalizedZ);

        actualNormalizedVector.ShouldBeApproximately(expectedVector);
    }
    
    [Theory]
    [InlineData(1,2,3, 0.26726, 0.53452, 0.80178, 1)]
    public void Normalizing_Vector_Should_Get_Magnitude(double x, double y, double z, double normalizedX, double normalizedY, double normalizedZ, double magnitude)
    {
        var vector = Tuple.Vector(x, y, z);
        var actualNormalizedVector = vector.Normalize();
        var expectedVector = Tuple.Vector(normalizedX, normalizedY, normalizedZ);
        
        actualNormalizedVector.ShouldBeApproximately(expectedVector);
        actualNormalizedVector.Magnitude().Should().Be(1);
    }

    [Fact]
    public void Dot_Product_Of_Two_Tuples()
    {
        var vectorA = new Tuple(1, 2, 3);
        var vectorB = new Tuple(2, 3, 4);

        var dot = vectorA.Dot(vectorB);
        dot.Should().Be(20);
    }

    [Fact]
    public void Cross_Product_Of_Two_Vectors()
    {
        var vectorA = new Tuple(1, 2, 3);
        var vectorB = new Tuple(2, 3, 4);
        
        var aXb = vectorA.Cross(vectorB);
        var bXa = vectorB.Cross(vectorA);
        aXb.ShouldBeApproximately(new Tuple(-1, 2, -1));
        bXa.ShouldBeApproximately(new Tuple(1, -2, 1));
    }
}

public static class Extensions
{
    public static void ShouldBeApproximately(this Tuple t1, Tuple t2, double tolerance = 0.001)
    {
        t1.X.Should().BeApproximately(t2.X, tolerance);
        t1.Y.Should().BeApproximately(t2.Y, tolerance);
        t1.Z.Should().BeApproximately(t2.Z, tolerance);
        t1.W.Should().BeApproximately(t2.W, tolerance);
    }
}

internal class TupleBuilder
{

    private double _x;
    private double _y;
    private double _z;
    private double _w;
    
   
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