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
using NoobO_Engine.Components;
using NoobO_Engine.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobO_Engine
{
    /// <summary>
    /// This class manages the main game loop.
    /// </summary>
    internal class GameThread : ISDLThread
    {
        /// <summary>
        /// Called after SDL initialization.
        /// </summary>
        public void Start()
        {
            GameObjectManager.Initialize();



            // DEBUG

            /*GameObject go = new GameObject();
            Transform transform = new Transform();
            transform.Position = new VectorF(128, 0);
            transform.Size = new VectorF(128, 128);
            transform.Depth = 15;
            go.AddComponent(transform);
            SpriteRenderer renderer = new SpriteRenderer();
            renderer.texture = new Texture("data/logo.jpeg");
            go.AddComponent(renderer);

            GameObject gotext = new GameObject();
            Transform texttransform = new Transform();
            texttransform.Position = new VectorF(0, 180);
            texttransform.Depth = 25;
            gotext.AddComponent(texttransform);
            TextRenderer trenderer = new TextRenderer();
            trenderer.FontInfo = new FontInfo("consola.ttf", 16);
            trenderer.Text = "testing text and image";
            texttransform.Size = new VectorF(trenderer.Texture.Width, trenderer.Texture.Height);
            gotext.AddComponent(trenderer);

            GameObject gotext2 = new GameObject();
            Transform texttransform2 = new Transform();
            texttransform2.Position = new VectorF(0, 200);
            texttransform2.Depth = 25;
            gotext2.AddComponent(texttransform2);
            TextRenderer trenderer2 = new TextRenderer();
            trenderer2.FontInfo = new FontInfo("consola.ttf", 14);
            trenderer2.Text = "rendering with a random logo";
            texttransform2.Size = new VectorF(trenderer2.Texture.Width, trenderer2.Texture.Height);
            gotext2.AddComponent(trenderer2);

            Camera camera = new Camera();
            GameObject go2 = new GameObject();
            Transform transform2 = new Transform();
            transform2.Position = new VectorF(0, 0);
            transform2.Size = new VectorF(256, 256);
            transform2.Depth = 100;
            go2.AddComponent(transform2);
            go2.AddComponent(camera);

            Camera camera2 = new Camera();
            camera2.Screen = new RectF(0, 0, 0.25f, 0.25f);
            GameObject go3 = new GameObject();
            Transform transform3 = new Transform();
            transform3.Position = new VectorF(0, 0);
            transform3.Size = new VectorF(256, 256);
            transform3.Depth = 120;
            go3.AddComponent(transform3);
            go3.AddComponent(camera2);*/

            // END OF DEBUG
        }
        /// <summary>
        /// Updates the game.
        /// </summary>
        public void Update()
        {
            GameObjectManager.Update();
        }
        /// <summary>
        /// Renders the game.
        /// </summary>
        public void Render()
        {
            GameObjectManager.Render();
        }
    }
}
