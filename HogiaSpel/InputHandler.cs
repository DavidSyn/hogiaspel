using HogiaSpel.Enums;
using HogiaSpel.Events;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace HogiaSpel
{
    public class InputHandler
    {
        private KeyboardState _keyboardState;

        public InputHandler()
        {
            _keyboardState = Keyboard.GetState();
        }

        public List<IEvent> HandleInputs()
        {
            var result = new List<IEvent>();
            var keyboardEvents = HandleKeyboard();
            result.AddRange(keyboardEvents);

            return result;
        }

        public List<IEvent> HandleKeyboard()
        {
            _keyboardState = Keyboard.GetState();
            var result = new List<IEvent>();

            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                result.Add(new MoveEvent(DirectionEnum.Up));
            }

            if (_keyboardState.IsKeyDown(Keys.Down))
            {
                result.Add(new MoveEvent(DirectionEnum.Down));
            }

            if (_keyboardState.IsKeyDown(Keys.Left))
            {
                result.Add(new MoveEvent(DirectionEnum.Left));
            }

            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                result.Add(new MoveEvent(DirectionEnum.Right));
            }
            return result;
        }
    }
}
