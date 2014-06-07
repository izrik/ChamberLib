using System;

namespace ChamberLib
{
    public interface IContentManager
    {
        T Load<T>(string name);

        IModel LoadModel(string name);
        ITexture2D LoadTexture2D(string name);
        IFont LoadFont(string name);
        object LoadSong(string name);
    }
}

