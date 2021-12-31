using System;
using ChamberLib.Content;

namespace ChamberLib.OpenTK.Content
{
    public class BasicSongImporter
    {
        public BasicSongImporter(SongImporter next=null)
        {
            this.next = next;
        }

        readonly SongImporter next;

        public SongContent ImportSong(string filename, IContentImporter importer)
        {
            var se = importer.ImportSoundEffect(filename);
            return new SongContent(se);
        }
    }
}

