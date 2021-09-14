using System;

namespace ChamberLib
{
    public interface IMesh
    {
        Sphere BoundingSphere { get; set; }
        string Name { get; set; }

        void Draw(GameTime gameTime, Matrix world, 
            ComponentCollection components,
            Overrides overrides=default(Overrides));
    }
}

