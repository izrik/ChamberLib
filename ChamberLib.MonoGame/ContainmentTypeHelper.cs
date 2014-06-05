using System;

namespace ChamberLib
{
    public static class ContainmentTypeHelper
    {
        public static Microsoft.Xna.Framework.ContainmentType ToXna(this ChamberLib.ContainmentType value)
        {
            switch (value)
            {
                case ChamberLib.ContainmentType.Disjoint:
                    return Microsoft.Xna.Framework.ContainmentType.Disjoint;
                case ChamberLib.ContainmentType.Contains:
                    return Microsoft.Xna.Framework.ContainmentType.Contains;
                case ChamberLib.ContainmentType.Intersects:
                    return Microsoft.Xna.Framework.ContainmentType.Intersects;
            }

            throw new ArgumentException();
        }

        public static ChamberLib.ContainmentType ToChamber(this Microsoft.Xna.Framework.ContainmentType value)
        {
            switch (value)
            {
                case Microsoft.Xna.Framework.ContainmentType.Disjoint:
                    return ChamberLib.ContainmentType.Disjoint;
                case Microsoft.Xna.Framework.ContainmentType.Contains:
                    return ChamberLib.ContainmentType.Contains;
                case Microsoft.Xna.Framework.ContainmentType.Intersects:
                    return ChamberLib.ContainmentType.Intersects;
            }

            throw new ArgumentException();
        }
    }
}

