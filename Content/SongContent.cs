using System;

namespace ChamberLib.Content
{
    public class SongContent
    {
        public SongContent(SoundEffectContent content)
        {
            if (content == null) throw new ArgumentNullException("content");

            Content = content;
        }

        public SoundEffectContent Content;
    }
}

