#region License
/* NoobOSDL License
 *
 * Copyright (c) 2014 Sergio Alonso
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 * 
 * Thanks to Ethan Lee for his work on SDL2-C# port.
 *
 * Permission is granted to anyone to use this software for any non-commercial
 * project as long as the following requirements are met:
*
 * 1. Sergio Alonso must be credited as the original author of NoobOSDL even
 * if you modify it's source files. Thanks must be given to Ethan Lee for his
 * work on SDL2-C# port.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 * 
 * If you want to use this software for a commercial project you need to ask
 * for permission to do so at samupo@noobogames.com
 *
 * Sergio "Samupo" Alonso <samupo@noobogames.com>
 * 
 * 
 * Ethan "flibitijibibo" Lee <flibitijibibo@flibitijibibo.com>
 *
 */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameEngine.SDL
{
    public struct Color {
        private _Color color;
        public byte R { get { if (color == null) return 255; return color.R; } }
        public byte G { get { if (color == null) return 255; return color.G; } }
        public byte B { get { if (color == null) return 255; return color.B; } }
        public byte A { get { if (color == null) return 255; return color.A; } }

        public Color(byte r, byte g, byte b, byte alpha = 255)
        {
            color = new _Color(r,g,b,alpha);
        }

        private class _Color
        {
            public byte R;
            public byte G;
            public byte B;
            public byte A;

            public _Color()
            {
                R = 255;
                G = 255;
                B = 255;
                A = 255;
            }

            public _Color(byte r, byte g, byte b, byte a)
            {
                R = r;
                G = g;
                B = b;
                A = a;
            }
        }
    }
}
