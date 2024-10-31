using System.Text;


namespace Raytracer.Common;

public class Canvas
{
    public Canvas(int width, int height)
    {
        Width = width;
        Height = height;
        Pixels = new Color[width, height];
        Fill(Color.Black);
    }

    public void Fill(Color color)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Pixels[x, y] = new Color(color);
            }
        }
    }

    public string ToPpm()
    {

        var sb = new StringBuilder();
        sb.AppendLine("P3");
        sb.AppendLine($"{Width} {Height}");
        sb.AppendLine("255");

        for (int y = 0; y < Height; y++)
        {
            var line = new StringBuilder();

            for (int x = 0; x < Width; x++)
            {
                var color = Pixels[x, y];
                var red = int.Clamp((int)Math.Round((double)(color.Red * 255)), 0, 255);
                var green = int.Clamp((int)Math.Round((double)(color.Green * 255)), 0, 255);
                var blue = int.Clamp((int)Math.Round((double)(color.Blue * 255)), 0, 255);
                var contentString = x > 0 ? $" {red} {green} {blue}" : $"{red} {green} {blue}";

                // Check if adding the new contentString would exceed 70 characters
                if (line.Length + contentString.Length > 70)
                {
                    // Collect indices of all spaces in the contentString
                    var spaceIndexes = new List<int>();
                    for (int i = 0; i < contentString.Length; i++)
                    {
                        if (contentString[i] == ' ')
                        {
                            spaceIndexes.Add(i);
                        }
                    }

                    // Find the last space that can be swapped with a newline
                    int lastValidSpaceIndex = -1;
                    foreach (var index in spaceIndexes)
                    {
                        if (line.Length + index < 70)
                        {
                            lastValidSpaceIndex = index;
                        }
                    }

                    if (lastValidSpaceIndex > -1)
                    {
                        // Append the current line up to the last valid space
                        line.Append(contentString.Substring(0, lastValidSpaceIndex).TrimEnd());
                        sb.AppendLine(line.ToString());
                        line.Clear();
                        // Append the valid content with a newline
                        line.Append(contentString.Substring(lastValidSpaceIndex + 1));
                    }
                    else
                    {
                        // If no valid space is found (fallback, although this should not happen)
                        sb.AppendLine(line.ToString().TrimEnd());
                        sb.AppendLine(contentString);
                        line.Clear();
                    }
                }
                else
                {
                    // If it fits, just append the content
                    line.Append(contentString);
                }
            }

            sb.AppendLine(line.Length > 0 ? line.ToString() : string.Empty);
        }
        return sb.ToString();

    }

    
    public int Width { get; private set; }
    public int Height { get; private set; }
    
    
    public void WritePixel(int x, int y, Color color)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            //  Do we really need to copy this?
            //  probably
            Pixels[x, y] = new Color(color);
        }
    }

    public Color PixelAt(int x, int y)
    {
        return (x >= 0 && x < Width && y >= 0 && y < Height) ? Pixels[x, y] : new Color(0,0,0);
    }

    public void Clear(Color color)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Pixels[x, y] = color;
            }
        }
    }
    
    public Color[,] Pixels { get; private set; }
    
    
}