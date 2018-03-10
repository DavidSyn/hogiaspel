using HogiaSpel.Enums;
using HogiaSpel.Events;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace HogiaSpel
{
    public class InputHandler
    {
        public List<IEvent> OldEvents { get; protected set; }
        public List<IEvent> NewEvents { get; protected set; }
        private KeyboardState _currentKeyboardState;

        public InputHandler()
        {
            _currentKeyboardState = Keyboard.GetState();
            OldEvents = new List<IEvent>();
            NewEvents = new List<IEvent>();
        }

        public List<IEvent> HandleInputs()
        {
            OldEvents = NewEvents;
            NewEvents = new List<IEvent>();
            var keyboardEvents = HandleKeyboard();
            NewEvents.AddRange(keyboardEvents);

            return NewEvents;
        }

        public List<IEvent> HandleKeyboard()
        {
            _currentKeyboardState = Keyboard.GetState();

            var result = new List<IEvent>();
            result = AddEvents();
            return result;
        }

        private List<IEvent> AddEvents()
        {
            var temp = new List<IEvent>();
            //if (_currentKeyboardState.IsKeyDown(Keys.Up))
            //{
            //    temp.Add(new MoveEvent(DirectionEnum.Up));
            //}

            //if (_currentKeyboardState.IsKeyDown(Keys.Down))
            //{
            //    temp.Add(new MoveEvent(DirectionEnum.Down));
            //}

            if (_currentKeyboardState.IsKeyDown(Keys.Right))
            {
                temp.Add(new MoveEvent(DirectionEnum.Right));
            }
            else if (_currentKeyboardState.IsKeyDown(Keys.Left))
            {
                temp.Add(new MoveEvent(DirectionEnum.Left));
            }

            if (_currentKeyboardState.IsKeyDown(Keys.Space))
            {
                temp.Add(new JumpEvent());
            }
            return temp;
        }
    }
}
