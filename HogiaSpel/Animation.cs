using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HogiaSpel
{
    public class Animation
    {
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }
        public bool Active { get; set; }
        public bool Looping { get; set; }
        public Vector2 Position { get; set; }

        private Texture2D _spriteStrip;
        private float _scale;
        private int _elapsedTime; // The time since we last updated the frame
        private int _frameTime; // The time we display a frame until the next one
        private int _frameCount;
        private int _currentFrame;
        private Color _color;
        private Rectangle _sourceRect = new Rectangle();
        private Rectangle _destinationRect = new Rectangle();
        

        public void Initialize(Texture2D spriteStrip, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frametime, Color color, float scale, bool looping)
        {
            _spriteStrip = spriteStrip;
            _frameCount = frameCount;
            _frameTime = frametime;
            _color = color;
            _scale = scale;
            _elapsedTime = 0;
            _currentFrame = 0;

            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            Looping = looping;
            Position = position;
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            if (Active == false) return;

            _elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_elapsedTime > _frameTime)
            { 
                _currentFrame++;

                // If the currentFrame is equal to frameCount reset currentFrame to zero
                if (_currentFrame == _frameCount)
                {
                    _currentFrame = 0;

                    if (Looping == false)
                    {
                        Active = false;
                    }
                }

                // Reset the elapsed time to zero
                _elapsedTime = 0;
            }

            // Grab the correct frame in the image strip by multiplying the currentFrame index by the Frame width
            _sourceRect = new Rectangle(_currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);

            // Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            _destinationRect = new Rectangle(
                (int)Position.X - (int)(FrameWidth * _scale) / 2, 
                (int)Position.Y - (int)(FrameHeight * _scale) / 2, 
                (int)(FrameWidth * _scale), 
                (int)(FrameHeight * _scale));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Draw(_spriteStrip, _destinationRect, _sourceRect, _color);
            }
        }
    }
}
