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

namespace NoobO_Engine
{
    public struct VectorF
    {
        public float X;
        public float Y;

        public float Length
        {
            get
            {
                return Mathf.Sqrt((X * X) + (Y * Y));
            }
        }

        public VectorF(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public static float Distance(VectorF v1, VectorF v2)
        {
            return (v1 - v2).Length;
        }

        public static VectorF operator +(VectorF v1, VectorF v2)
        {
            return new VectorF(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static VectorF operator -(VectorF v1, VectorF v2)
        {
            return new VectorF(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static VectorF operator*(VectorF v, float f) {
            return new VectorF(v.X * f, v.Y *f);
        }

        public static VectorF operator /(VectorF v, float f)
        {
            return new VectorF(v.X / f, v.Y / f);
        }
    }
}
