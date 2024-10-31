using FluentAssertions;
using FluentAssertions.Execution;

namespace Raytracer.Common.Tests;

public class ColorScenarios
{
    [Fact]
    public void Colors_should_be_correctly_represented()
    {
        var color = new Color(-0.5, 0.4, 1.7);
        using var assertionScope = new AssertionScope();
        color.Red.Should().Be(-0.5);
        color.Green.Should().Be(0.4);
        color.Blue.Should().Be(1.7);
        
    }

    [Fact]
    public void Adding_colors()
    {
        var colorA = new Color(0.9, 0.6, 0.75);
        var colorB = new Color(0.7, 0.1, 0.25);

        var result = colorA + colorB;
        using var assertionScope = new AssertionScope();
        result.Should().BeEquivalentTo(new Color(1.6, 0.7, 1));
   
    }
    
    [Fact]
    public void Subtracting_colors()
    {
        var colorA = new Color(0.9, 0.6, 0.75);
        var colorB = new Color(0.7, 0.1, 0.25);

        var result = colorA - colorB;
        using var assertionScope = new AssertionScope();
        var expectedColor = new Color(0.2, 0.5, 0.5);

        ShouldBeApproximatelyEquivalent(result, expectedColor);

    }
    
    [Fact]
    public void Multiplying_a_color_with_scalars()
    {
        var colorA = new Color(0.2, 0.3, 0.4);
        var result = colorA * 2;
        using var assertionScope = new AssertionScope();
        var expectedColor = new Color(0.4, 0.6, 0.8);

        ShouldBeApproximatelyEquivalent(result, expectedColor);

    }
    
    [Fact]
    public void Multiplying_a_color_with_scalars_double()
    {
        var colorA = new Color(0.2, 0.3, 0.4);
        var result = colorA * 2.0;
        using var assertionScope = new AssertionScope();
        var expectedColor = new Color(0.4, 0.6, 0.8);

        ShouldBeApproximatelyEquivalent(result, expectedColor);

    }
    
    [Fact]
    public void Multiplying_colors()
    {
        var colorA = new Color(1, 0.2, 0.4);
        var colorB = new Color(0.9, 1, 0.1);
        var result = colorA * colorB;
        using var assertionScope = new AssertionScope();
        var expectedColor = new Color(0.9, 0.2, 0.04);

        ShouldBeApproximatelyEquivalent(result, expectedColor);

    }

    public void ShouldBeApproximatelyEquivalent(Color a, Color b)
    {
        using var assertionScope = new AssertionScope();
        a.Red.Should().BeApproximately(b.Red, 0.001);
        a.Blue.Should().BeApproximately(b.Blue, 0.001);
        a.Green.Should().BeApproximately(b.Green, 0.001);
    }
   
}