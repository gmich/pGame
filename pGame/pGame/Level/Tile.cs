﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzlePrototype.Level
{
    public interface Tile
    {
        int ID
        {
            get;
            set;
        }

        bool Passable
        {
            get;
            set;
        }

        void Update(GameTime gameTime);
        
    }
}
