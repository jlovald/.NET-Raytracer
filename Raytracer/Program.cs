
using System.Diagnostics;
using Raytracer.Common;
using Tuple = Raytracer.Common.Tuple;
using Environment = Raytracer.Common.Environment;

namespace Raytracer;

class Program
{
    static void Main(string[] args)
    {
        var environment = new Environment(Tuple.Vector(0, -0.1, 0), Tuple.Vector(-0.01, 0, 0));
        var projectile = new Projectile(Tuple.Point(0, 1, 0), Tuple.Vector(1, 1.8, 0).Normalize() * 11.25);
        var tickCounter = 0;
        var canvas = new Canvas(900, 500);
        while (projectile.Position.Y > 0)
        {
            Console.WriteLine(
                $"At tick: {tickCounter++} X: {projectile.Position.X}, Y: {projectile.Position.Y}, Z: {projectile.Position.Z}");
            canvas.WritePixel((int)projectile.Position.X,  canvas.Height - (int)projectile.Position.Y,
                new Color(1, 0, 0));
            projectile = projectile.Tick(environment);
        }

        var ppm = canvas.ToPpm();

        // Define a temporary file path (you may change the directory as needed)
        string tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName()) + ".ppm";

        // Write the PPM data to the temporary file
        File.WriteAllText(tempFilePath, ppm);

        // Open the file using the default associated application
        Process.Start(new ProcessStartInfo
        {
            FileName = tempFilePath,
            UseShellExecute = true // Use this to open the file with the associated application
        });
    }
}

