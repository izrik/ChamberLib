using System;
using XSong = Microsoft.Xna.Framework.Media.Song;
using System.Collections.Generic;

namespace ChamberLib
{
    public class SongAdapter : ISong
    {
        protected static readonly Dictionary<XSong, SongAdapter> _cache = new Dictionary<XSong, SongAdapter>();

        public static ISong GetAdapter(XSong song)
        {
            if (_cache.ContainsKey(song))
            {
                return _cache[song];
            }

            var adapter = new SongAdapter(song);
            _cache[song] = adapter;

            return adapter;
        }

        protected SongAdapter(XSong song)
        {
            Song = song;
        }

        public readonly XSong Song;
    }
}

