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
using _2DGameEngine.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameEngine.Components
{
    public class Camera : Component
    {
        private class CameraComparer : IComparer<Camera>
        {
            public int Compare(Camera c1, Camera c2)
            {
                return c1.Depth - c2.Depth;
            }
        }
        private int _width = (int)Graphics.WindowSize.X;
        private int _height = (int)Graphics.WindowSize.Y;
        public int Width { get { return _width; } set { _width = value; RecreateInternalTexture(); } }
        public int Height { get { return _height; } set { _height = value; RecreateInternalTexture(); } }
        private RectF _screen = new RectF(0,0,1,1);
        public RectF Screen {get {return _screen;} set {_screen = value;}}
        public int Depth { get; set; }
        private bool _active = true;
        public bool Active { get { return GameObject != null && _active; } set { _active = value; } }

        public Texture RenderTexture { get; private set; }

        public Camera(int depth = 0)
        {
            Width = (int)Graphics.WindowSize.X;
            Height = (int)Graphics.WindowSize.Y;

            RecreateInternalTexture();

            allCameras.Add(this);
        }

        private void RecreateInternalTexture()
        {
            RenderTexture = new Texture(Width, Height); 
        }

        internal override void Render()
        {
            Graphics.SetRenderTarget(IntPtr.Zero);
            RectF dstRect = new RectF(Screen.X * Graphics.WindowSize.X, Screen.Y * Graphics.WindowSize.Y, Screen.Width * Graphics.WindowSize.X, Screen.Height * Graphics.WindowSize.Y);
            RenderTexture.Draw(dstRect);
        }

        ~Camera()
        {
            allCameras.Remove(this);
        }

        private static SortedSet<Camera> allCameras = new SortedSet<Camera>(new CameraComparer());

    }
}
