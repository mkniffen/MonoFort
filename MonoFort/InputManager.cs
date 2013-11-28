using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoFort
{
    public class InputManager
    {
        KeyboardState currentKeyState, prevKeyState;

        private static InputManager instance;

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputManager();
                }

                return instance;
            }
        }

        public void Update()
        {
            prevKeyState = currentKeyState;
            if (ScreenManager.Instance.IsTransitioning == false)
            {
                currentKeyState = Keyboard.GetState();
            }
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (currentKeyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                {
                    return true;
                }
            }
            return false;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (currentKeyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                {
                    return true;
                }
            }
            return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (currentKeyState.IsKeyDown(key))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
