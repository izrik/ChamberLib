using System;

namespace ChamberLib
{
    public interface ILoadable
    {
        void LoadContents(IContentManager contentManager);
    }
}

