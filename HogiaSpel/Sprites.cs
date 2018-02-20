using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HogiaSpel
{
    public sealed class Sprites
    {
        private static readonly Lazy<Sprites> _lazy = new Lazy<Sprites>(() => new Sprites());
        private Dictionary<string, Texture2D> _assets;

        public static Sprites Instance { get { return _lazy.Value; } }

        private Sprites()
        {
            _assets = new Dictionary<string, Texture2D>();
        }

        public void Load(string spriteName, Texture2D content)
        {
            _assets.Add(spriteName, content);
        }

        public Texture2D GetSprite(string spriteName)
        {
            return _assets[spriteName];
        }
    }
}
