using System;

namespace ChamberLib
{
    public class ViewportSource
    {
        public ViewportSource(Func<Viewport> func)
        {
            _func = func;
        }

        Func<Viewport> _func;

        public Viewport Viewport
        {
            get { return _func(); }
        }

    }
}
