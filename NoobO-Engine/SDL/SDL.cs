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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2DGameEngine.SDL
{
    internal enum Initializers
    {
        SDL_INIT_TIMER = 0x00000001,
		SDL_INIT_AUDIO = 0x00000010,
		SDL_INIT_VIDEO = 0x00000020,
		SDL_INIT_JOYSTICK = 0x00000200,
		SDL_INIT_HAPTIC = 0x00001000,
		SDL_INIT_GAMECONTROLLER = 0x00002000,
		SDL_INIT_NOPARACHUTE = 0x00100000,
		SDL_INIT_EVERYTHING = SDL_INIT_TIMER | SDL_INIT_AUDIO | SDL_INIT_VIDEO | SDL_INIT_JOYSTICK | SDL_INIT_HAPTIC | SDL_INIT_GAMECONTROLLER
    }

    internal enum ImageMode
    {
        IMG_INIT_JPG = 0x00000001,
        IMG_INIT_PNG = 0x00000002,
        IMG_INIT_TIF = 0x00000004,
        IMG_INIT_WEBP = 0x00000008
    }


    public abstract class SDL
    {

        internal const string NATIVELIB = "SDL2.dll";
        internal const string IMGLIB = "SDL2_image.dll";
        internal const string TTFLIB = "SDL2_ttf.dll";
        internal const string MIXLIB = "SDL2_mixer.dll";

        public static void Run(ISDLThread thread)
        {
            SDL_Init((UInt32)Initializers.SDL_INIT_EVERYTHING);
            IMG_Init((UInt32)(ImageMode.IMG_INIT_PNG | ImageMode.IMG_INIT_JPG));
            Graphics.Initialize(800, 600);

            while (true)
            {
                SDL_Event e;
                SDL_PollEvent(out e);

                thread.Update();
                thread.Render();

                Graphics.Render();
            }
        }

        public static void RunFrom(IntPtr handler, ISDLThread thread)
        {
            SDL_Init((UInt32)Initializers.SDL_INIT_EVERYTHING);
            IMG_Init((UInt32)(ImageMode.IMG_INIT_PNG | ImageMode.IMG_INIT_JPG));
            Graphics.InitializeFrom(handler, 640, 480);

            while (true)
            {
                SDL_Event e;
                SDL_PollEvent(out e);

                thread.Update();
                thread.Render();

                Graphics.Render();
            }

        }


        [DllImport(IMGLIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern int IMG_Init(UInt32 flags);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SDL_PollEvent(out SDL_Event _event);

        [DllImport(NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SDL_Init(UInt32 flags);
    }
}
