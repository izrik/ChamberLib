
//
// ChamberLib, a cross-platform game engine
// Copyright (C) 2021 izrik and Metaphysics Industries, Inc.
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
// USA
//

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

