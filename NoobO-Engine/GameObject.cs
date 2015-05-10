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
using _2DGameEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameEngine
{
    /// <summary>
    /// GameObject base class.
    /// </summary>
    public class GameObject
    {
        /// <summary>
        /// Component list
        /// </summary>
        private LinkedList<Component> components;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        public GameObject()
        {
            components = new LinkedList<Component>();
        }

        /// <summary>
        /// Called when the entity enters the game.
        /// </summary>
        internal void Awake()
        {
            foreach (Component component in components)
            {
                component.Awake();
            }
        }

        /// <summary>
        /// Called afer the Awake method.
        /// </summary>
        internal void Start()
        {
            foreach (Component component in components)
            {
                component.Start();
            }
        }

        /// <summary>
        /// Called every game tick.
        /// </summary>
        internal void EarlyUpdate()
        {
            foreach (Component component in components)
            {
                component.EarlyUpdate();
            }
        }

        /// <summary>
        /// Called every game tick after all EarlyUpdate methods have been called.
        /// </summary>
        internal void Update()
        {
            foreach (Component component in components)
            {
                component.Update();
            }
        }

        /// <summary>
        /// Called every game tick after all Update methods have been called.
        /// </summary>
        internal void LateUpdate()
        {
            foreach (Component component in components)
            {
                component.LateUpdate();
            }
        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        internal void Render()
        {
            foreach (Component component in components)
            {
                component.Render();
            }
        }

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="component">Component.</param>
        public void AddComponent(Component component)
        {
            components.AddLast(component);
        }

        /// <summary>
        /// Gets the components.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// Removes the component.
        /// </summary>
        /// <param name="component">The component.</param>
        internal void RemoveComponent(Component component)
        {
            components.Remove(component);
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void Destroy()
        {

        }
    }
}
