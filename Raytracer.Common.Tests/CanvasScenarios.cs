using FluentAssertions;
using FluentAssertions.Execution;

namespace Raytracer.Common.Tests;

public class CanvasScenarios
{
    [Fact]
    public void Crating_canvas()
    {
        var c = new Canvas(10, 20);
        using var assertionScope = new AssertionScope();
        assertionScope.FormattingOptions.MaxDepth = 21;
        for (int i = 0; i < c.Width; i++)
        {
            for (int k = 0; k < c.Height; k++)
            {
                var pixel = c.Pixels[i, k];
                pixel.Red.Should().Be(0);
                pixel.Blue.Should().Be(0);
                pixel.Green.Should().Be(0);
            }
        }
    }
    
    [Fact]
    public void Writing_pixels_to_a_canvas()
    {
        var c = new Canvas(10, 20);
        using var assertionScope = new AssertionScope();
        assertionScope.FormattingOptions.MaxDepth = 21;
        var red = new Color(1, 0, 0);
        c.WritePixel(2,3, red);
        var pixel = c.PixelAt(2, 3);
        pixel.Red.Should().Be(1);
        pixel.Blue.Should().Be(0);
        pixel.Green.Should().Be(0);
    }
    
    [Fact]
    public void Canvas_to_ppm_should_have_valid_header()
    {
        var c = new Canvas(5, 3);
        using var assertionScope = new AssertionScope();
        var ppm = c.ToPpm();
        ppm.Should().Contain(@"P3
5 3
255");
    }
    
    [Fact]
    public void Canvas_to_ppm_should_have_valid_content()
    {
        var canvas = new Canvas(5, 3);
        var color1 = new Color(1.5, 0, 0);
        var color2 = new Color(0, 0.5, 0);
        var color3 = new Color(-0.5, 0, 1);
        
        canvas.WritePixel(0,0, color1);
        canvas.WritePixel(2,1, color2);
        canvas.WritePixel(4,2, color3);
        using var assertionScope = new AssertionScope();
        var ppm = canvas.ToPpm();
        ppm.Should().Contain(@"255 0 0 0 0 0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 128 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0 0 0 0 0 255");
    }

    [Fact]
    public void Canvas_to_ppm_should_have_valid_lineWidth()
    {
        var canvas = new Canvas(10, 3);
        var color1 = new Color(1, 0.8, 0.6);
        canvas.Fill(color1);
        
        using var assertionScope = new AssertionScope();
        var ppm = canvas.ToPpm();
        ppm.Should().Contain(@"255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204
153 255 204 153 255 204 153 255 204 153 255 204 153
255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204
153 255 204 153 255 204 153 255 204 153 255 204 153");
    }
    
    [Fact]
    public void Canvas_to_ppm_ends_with_new_line()
    {
        var canvas = new Canvas(10, 3);
        var color1 = new Color(1, 0.8, 0.6);
        canvas.Fill(color1);
        
        using var assertionScope = new AssertionScope();
        var ppm = canvas.ToPpm();
        ppm.Should().EndWith(System.Environment.NewLine);
    }
    
    
}