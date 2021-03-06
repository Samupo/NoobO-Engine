﻿#region License
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
    internal abstract class GameObjectManager
    {
        /// <summary>
        /// Comparer to sort by depth for rendering purposes.
        /// </summary>
        private class SortByDepth : IComparer<Transform>
        {
            public int Compare(Transform t1, Transform t2)
            {
                if (t1 == t2) return 0;
                if (t1.Depth == t2.Depth) return 1;
                return (int)(t1.Depth - t2.Depth);
            }
        }

        /// <summary>
        /// Arbirtrary value for maximum depth.
        /// </summary>
        public const int MAX_DEPTH = 1000;
        /// <summary>
        /// The game objects in scene.
        /// </summary>
        private static HashSet<GameObject> gameObjectsInScene = new HashSet<GameObject>();
        /// <summary>
        /// The transforms.
        /// </summary>
        private static SortedSet<Transform>[] transforms = new SortedSet<Transform>[MAX_DEPTH];

        /// <summary>
        /// Initializes the manager.
        /// </summary>
        public static void Initialize()
        {
            for (int i = 0; i < MAX_DEPTH; i++)
            {
                transforms[i] = new SortedSet<Transform>(new SortByDepth());
            }
        }

        internal static void AddGameObject(GameObject go)
        {
            gameObjectsInScene.Add(go);
        }

        internal static void RemoveGameObject(GameObject go)
        {
            gameObjectsInScene.Remove(go);
        }

        internal static void AddTransform(Transform tr) {
            transforms[(int)tr.Depth].Add(tr);
        }

        internal static void RemoveTransform(Transform tr)
        {
            transforms[(int)tr.Depth].Remove(tr);
        }

        /// <summary>
        /// Updates the current scene.
        /// </summary>
        public static void Update()
        {
            foreach (GameObject go in gameObjectsInScene)
            {
                go.DoStartRoutine();
            }
            foreach (GameObject go in gameObjectsInScene)
            {
                go.EarlyUpdate();
            }
            foreach (GameObject go in gameObjectsInScene)
            {
                go.Update();
            }
            foreach (GameObject go in gameObjectsInScene)
            {
                go.LateUpdate();
            }
        }

        /// <summary>
        /// Renders the scene.
        /// </summary>
        public static void Render()
        {
            foreach (Camera camera in Camera.GetAllCameras())
            {
                Graphics.SetRenderTarget(camera.RenderTexture.TexturePtr);
                for (int i = 0; i < MAX_DEPTH; i++)
                {
                    foreach (Transform tr in transforms[i])
                    {
                        tr.GameObject.Render(camera.Transform.ToRectF());
                    }
                }
            }
            foreach (Camera camera in Camera.GetAllCameras())
            {
                camera.CameraRender();
            }
        }
    }
}
