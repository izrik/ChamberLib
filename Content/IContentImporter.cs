using System;

namespace ChamberLib.Content
{
    public interface IContentImporter
    {
        ModelContent ImportModel(string name, IContentImporter importer=null);
        TextureContent ImportTexture2D(string name, IContentImporter importer=null);
        FontContent ImportFont(string name, IContentImporter importer=null);
        SongContent ImportSong(string name, IContentImporter importer=null);
        SoundEffectContent ImportSoundEffect(string name, IContentImporter importer=null);
        //ShaderContent ImportShader(string name, IContentImporter importer=null);
        ShaderContent ImportShaderStage(string name, ShaderType type, IContentImporter importer=null);
    }
}

