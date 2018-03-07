using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HogiaSpel
{
    public class SpriteHandler
    {
        public Dictionary<string, Animation> Animations { get; protected set; }
        public Vector2 Position { get; set; }
        public int CurrentFrameHeight { get { return (!string.IsNullOrEmpty(_currentState)) ? Animations[_currentState].FrameHeight : 0; } }
        public int CurrentFrameWidth { get { return (!string.IsNullOrEmpty(_currentState)) ? Animations[_currentState].FrameWidth : 0; } }

        private string _currentState;

        public SpriteHandler(Vector2 position)
        {
            Animations = new Dictionary<string, Animation>();
            Position = position;
            _currentState = string.Empty;
        }

        public void InitializeAnimation(string state, Texture2D sprite, int frameWidth, int frameHeight, int frameCount, int frametime, Color color, float scale, bool looping)
        {
            var anime = new Animation(sprite, Position, frameWidth, frameHeight, frameCount, frametime, color, scale, looping);
            Animations.Add(state, anime);
        }

        public void Initialize(string startState)
        {
            _currentState = startState;
            Animations[_currentState].Active = true;
        }

        public void ChangeState(string newState)
        {
            Animations[_currentState].Active = false;
            _currentState = newState;
            Animations[_currentState].Active = true;
        }

        public void Update(GameTime gameTime)
        {
            Animations[_currentState].Update(gameTime, Position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Animations[_currentState].Draw(spriteBatch);
        }
    }
}
