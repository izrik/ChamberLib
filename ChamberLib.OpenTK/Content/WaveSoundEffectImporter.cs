﻿
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
using System.IO;
using NAudio.Wave;
using ChamberLib.Content;

namespace ChamberLib.OpenTK.Content
{
    public class WaveSoundEffectImporter
    {
        public WaveSoundEffectImporter(SoundEffectImporter next=null)
        {
            this.next = next;
        }

        readonly SoundEffectImporter next;

        public SoundEffectContent ImportSoundEffect(string filename, IContentImporter importer)
        {
            if (File.Exists(filename))
            {
            }
            else if (File.Exists(filename + ".wav"))
            {
                filename += ".wav";
            }
            else if (next != null)
            {
                return next(filename, importer);
            }
            else
            {
                throw new FileNotFoundException("The sound file could not be found.", filename);
            }

            var stream = File.Open(filename, FileMode.Open);

            int numChannels;
            int bitsPerSample;
            int samplesPerSecond;
            byte [] audioData;

            var wfr = new WaveFileReader(stream);

            numChannels = wfr.WaveFormat.Channels;
            bitsPerSample = wfr.WaveFormat.BitsPerSample;
            samplesPerSecond = wfr.WaveFormat.SampleRate;

            if (numChannels != 1 && numChannels != 2) throw new NotSupportedException("The sound format is not supported");
            if (bitsPerSample != 8 && bitsPerSample != 16) throw new NotSupportedException("The sound format is not supported");

            int length = (int)(wfr.BlockAlign * (wfr.Length / wfr.BlockAlign)); //TODO: length might be more than MAX_INT
            audioData = new byte[length];
            wfr.Read(audioData, 0, length);

            return new SoundEffectContent(filename, numChannels, bitsPerSample,
                samplesPerSecond, audioData);
        }
    }
}

