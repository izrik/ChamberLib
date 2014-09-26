﻿
using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChamberLib
{
    public static class GLHelper
    {
        public static ErrorCode CheckError(bool throwException=true)
        {
            var error = GL.GetError();
            if (error != ErrorCode.NoError)
            {
                var message = string.Format("OpenGL Error: {0}", error);

                if (throwException)
                {
                    throw new InvalidOperationException(message);
                }
                else
                {
//                    System.Diagnostics.Debug.WriteLine(message);
                }
            }

            return error;
        }


        public static void CheckState(string label = null)
        {
            if (string.IsNullOrEmpty(label))
            {
                Debug.WriteLine("CheckState");
            }
            else
            {
                Debug.WriteLine(string.Format("CheckState: {0}", label));
            }

            var vab = GL.GetInteger(GetPName.VertexArrayBinding);
            var vabe = GLHelper.CheckError(false);
            Debug.WriteLine(string.Format("  VertexArrayBinding: {0}, {1}", vab, vabe));
            var abb = GL.GetInteger(GetPName.ArrayBufferBinding);
            var abbe = GLHelper.CheckError(false);
            Debug.WriteLine(string.Format("  ArrayBufferBinding: {0}, {1}", abb, abbe));
            var eabb = GL.GetInteger(GetPName.ElementArrayBufferBinding);
            var eabbe = GLHelper.CheckError(false);
            Debug.WriteLine(string.Format("  ElementArrayBufferBinding: {0}, {1}", eabb, eabbe));

            int max = GL.GetInteger(GetPName.MaxVertexAttribs);
            var maxe = GLHelper.CheckError(false);
            Debug.WriteLine(string.Format("  MaxVertexAttribs: {0}, {1}", max, maxe));
            int i;

            var currents = new List<Vector4>();
            var currentse = new List<ErrorCode>();
            for (i = 0; i < max; i++)
            {
                var p = new float[4];
                GL.GetVertexAttrib(i, VertexAttribParameter.CurrentVertexAttrib, p);
                var v = new Vector4(p[0], p[1], p[2], p[3]);
                currents.Add(v);
                var e = GLHelper.CheckError(false);
                currentse.Add(e);
                Debug.WriteLine(string.Format("  CurrentVertexAttrib[{5}]: {0}, {1}, {2}, {3}, {4}", p[0], p[1], p[2], p[3], e, i));

            }

            var enableds = new List<int>();
            var enabledse = new List<ErrorCode>();
            for (i = 0; i < max; i++)
            {
                int value = 0;
                GL.GetVertexAttrib(i, VertexAttribParameter.ArrayEnabled, out value);
                enableds.Add(value);
                var e = GLHelper.CheckError(false);
                enabledse.Add(e);
                Debug.WriteLine(string.Format("  ArrayEnabled[{2}]: {0}, {1}", value, e, i));
            }

            max = 0;

            Debug.WriteLine("");
            Debug.WriteLine("");

        }

    }
}

