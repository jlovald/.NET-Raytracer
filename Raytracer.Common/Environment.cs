namespace Raytracer.Common;

public class Environment
{
    public Environment(Tuple gravity, Tuple wind)
    {
        Gravity = gravity;
        Wind = wind;
    }
    
    public Tuple Gravity { get; private set; }
    public Tuple Wind { get; private set; }
}

public class Projectile
{
    public Projectile(Tuple position, Tuple velocity)
    {
        Position = position;
        Velocity = velocity;
    }

    public Projectile Tick(Environment environment)
    {
        return new Projectile(Position + Velocity, Velocity + environment.Gravity + environment.Wind);
    }

    public Tuple Position { get; set; }
    public Tuple Velocity { get; private set; }
}