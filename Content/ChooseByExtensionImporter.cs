using System;
using ChamberLib.Content;
using System.Collections.Generic;
using System.IO;


namespace ChamberLib.Content
{
    public delegate U Importer<U>(string filename, IContentImporter importer);

    public class ChooseByExtensionImporter<T>
    {

        public ChooseByExtensionImporter(IDictionary<string, Importer<T>> importersByExtension, Importer<T> next=null)
        {
            _importersByExtension = new Dictionary<string, Importer<T>>(importersByExtension);
            _next = next;
        }

        readonly Dictionary<string, Importer<T>> _importersByExtension;
        readonly Importer<T> _next;

        public T Import(string filename, IContentImporter importer)
        {
            var ext = Path.GetExtension(filename);
            while (ext.StartsWith("."))
            {
                ext = ext.Substring(1);
            }

            if (_importersByExtension.ContainsKey(ext))
            {
                return _importersByExtension[ext](filename, importer);
            }

            if (_next != null)
            {
                return _next(filename, importer);
            }

            throw new NotImplementedException(
                string.Format(
                    "No importer specified for extension \"{0}\"", ext));
        }
    }
}

