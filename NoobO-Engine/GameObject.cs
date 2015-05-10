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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameEngine
{
    public class GameObject
    {
        LinkedList<Component> components;

        public GameObject()
        {
            components = new LinkedList<Component>();
        }

        internal void Awake()
        {
            foreach (Component component in components)
            {
                component.Awake();
            }
        }
        internal void Start()
        {
            foreach (Component component in components)
            {
                component.Start();
            }
        }
        internal void EarlyUpdate()
        {
            foreach (Component component in components)
            {
                component.EarlyUpdate();
            }
        }
        internal void Update()
        {
            foreach (Component component in components)
            {
                component.Update();
            }
        }
        internal void LateUpdate()
        {
            foreach (Component component in components)
            {
                component.LateUpdate();
            }
        }
        internal void Render()
        {
            foreach (Component component in components)
            {
                component.Render();
            }
        }

        public void AddComponent(Component component)
        {
            components.AddLast(component);
        }

        public T[] GetComponents<T>() where T : Component
        {
            List<T> list = new List<T>();
            foreach (Component component in components)
            {
                if (component.GetType() == typeof(T))
                {
                    list.Add(component as T);
                }
            }
            return list.ToArray();
        }

        internal void RemoveComponent(Component component)
        {
            components.Remove(component);
        }

        public void Destroy()
        {

        }
    }
}