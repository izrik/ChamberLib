using System;
using ChamberLib.Content;

namespace ChamberLib.OpenTK.Content
{
    public class BuiltinFontImporter
    {
        public BuiltinFontImporter(FontImporter next=null)
        {
            this.next = next;
        }

        readonly FontImporter next;

        public FontContent ImportFont(string filename, IContentImporter importer)
        {
            return new FontContent();
        }
    }
}

