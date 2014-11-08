#region File Information
/*
 * 
 * 
 */
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace TheLeash
{
    class Sprite
    {
        Texture2D textureImage;
        Point frameSize;
        Point currentFrame;
        Point sheetSize;
        int collisionOffset;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        const int DEFAULT_MILLISECONDS_PER_FRAME = 16;
        Vector2 speed;
        Vector2 position;

        public Sprite(Texture2D textureImage, Vector2 positon, Point framesize, int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed)
            : this(textureImage, positon, framesize, collisionOffset, currentFrame, sheetSize, speed, DEFAULT_MILLISECONDS_PER_FRAME)
        {
        }

        public Sprite(Texture2D textureImage, Vector2 position, Point framesize, int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, int millisecondsPerFrame)
        {
            this.textureImage = textureImage;
            this.frameSize = framesize;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.collisionOffset = collisionOffset;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.speed = speed;
            this.position = position;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                    {
                        currentFrame.Y = 0;
                    }
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage, 
                position, 
                new Rectangle(currentFrame.X * frameSize.X, 
                    currentFrame.Y * frameSize.Y, 
                    frameSize.X, frameSize.Y),
                Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }
    }
}
