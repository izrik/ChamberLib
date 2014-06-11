using System;

namespace ChamberLib
{
    public class ContentManager : IContentManager
    {
        public T Load<T>(string name)
        {
            return default(T);
        }

        public ITexture2D CreateTexture(int width, int height, Color[] data)
        {
            return null;
        }
    }
}

