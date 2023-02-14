using System;

namespace ChamberLib
{
    public interface IMesh
    {
        Sphere BoundingSphere { get; set; }
        string Name { get; set; }

        void Draw(GameTime gameTime, Matrix world, Matrix view, Matrix projection,
                    LightingData lighting, Overrides overrides=default(Overrides));
    }
}

