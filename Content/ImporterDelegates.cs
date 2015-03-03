using System;

namespace ChamberLib.Content
{
    public delegate ModelContent ModelImporter(string filename, IContentImporter importer);
    public delegate TextureContent TextureImporter(string filename, IContentImporter importer);
    public delegate ShaderContent ShaderImporter(string filename, IContentImporter importer);
    public delegate FontContent FontImporter(string filename, IContentImporter importer);
    public delegate SongContent SongImporter(string filename, IContentImporter importer);
    public delegate SoundEffectContent SoundEffectImporter(string filename, IContentImporter importer);
}

