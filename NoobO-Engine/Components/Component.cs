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
using NoobO_Engine.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobO_Engine.Components
{
    public class Component
    {
        private GameObject _go;
        public GameObject GameObject { get { return _go; } private set { _go = value; } }
        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void EarlyUpdate() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        internal virtual void Render(Rect viewport) { }

        internal void AssignToGameObject(GameObject go)
        {
            if (GameObject != null)
            {
                Debug.Warn("This component is already assigned to a game object!");
            }
            else
            {
                Remove();
            }
            GameObject = go;
        }

        public void Remove()
        {
            if (GameObject != null)
            {
                GameObject.RemoveComponent(this);
            }
        }
    }
}
