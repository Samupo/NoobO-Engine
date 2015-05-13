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
using NoobO_Engine.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobO_Engine.Components
{
    public class Camera : Component
    {
        private class CameraComparer : IComparer<Camera>
        {
            public int Compare(Camera c1, Camera c2)
            {
                if (c1 == c2) return 0;
                if (c1.ScreenLayer == c2.ScreenLayer) return 1;
                return c1.ScreenLayer - c2.ScreenLayer;
            }
        }
        private RectF _screen = new RectF(0,0,1,1);
        public RectF Screen {get {return _screen;} set {_screen = value;}}
        private int _screenLayer = 0;
        public int ScreenLayer
        {
            get { return _screenLayer; }
            set
            {
                allCameras.Remove(this);
                _screenLayer = value;
                allCameras.Add(this);
            }
        }
        private bool _active = true;
        public bool Active { get { return GameObject != null && _active; } set { _active = value; } }
        private static SortedSet<Camera> allCameras = new SortedSet<Camera>(new CameraComparer());
        internal Transform Transform { get; private set; }

        public static ICollection<Camera> GetAllCameras()
        {
            return allCameras.ToArray();
        }

        public Texture RenderTexture { get; private set; }

        public override void Awake()
        {
            Transform = GameObject.GetComponents<Transform>()[0];
            if (Transform == null)
            {
                Debug.Error("Renderer component requires a Transform component");
            }
            RecreateInternalTexture();
        }


        public Camera()
        {
            allCameras.Add(this);
        }

        private void RecreateInternalTexture()
        {
            RenderTexture = new Texture((int)Transform.Size.X, (int)Transform.Size.Y); 
        }

        internal void CameraRender()
        {
            Graphics.SetRenderTarget(IntPtr.Zero);
            RectF dstRect = new RectF(Screen.X * Graphics.WindowSize.X, Screen.Y * Graphics.WindowSize.Y, Screen.Width * Graphics.WindowSize.X, Screen.Height * Graphics.WindowSize.Y);
            RenderTexture.Draw(dstRect);
        }

        ~Camera()
        {
            allCameras.Remove(this);
        }

        
    }
}
