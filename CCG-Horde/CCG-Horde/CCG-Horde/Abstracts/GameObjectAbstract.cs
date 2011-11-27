using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace CCG_Horde
{
    public abstract class GameObjectAbstract : Microsoft.Xna.Framework.DrawableGameComponent
    {

        protected SpriteBatch spriteBatch;
        protected Game game;
        public Texture2D texture;

        public bool isAlive;

        public Vector2 position;
        public Vector2 velocity;

        public Vector2 facing;

        public Vector2 origin;

        public float rotation;
        public float scale;
        public Color color;

        public bool isWallBounce;
        public float wallBounceDampningFactor;

        public int currentHP;
        public int initialHP;

        public float speed;

        public Vector2 tilePosition;

        public struct Orientations
        {
           public Vector2 North;
           public Vector2 NorthEast;
           public Vector2 East;
           public Vector2 SouthEast;
           public Vector2 South;
           public Vector2 SouthWest;
           public Vector2 West;
           public Vector2 NorthWest;

        }

        public Orientations orientationList;
        public Vector2 orientation;

        public enum ObjectType
        {
            Player,
            King,
            Pawn,
            Knight,
            Bishop,
            Rook,
            Queen

        }

        public int turnCooldown;
        int timerCounter;

        public int frameIndex;

        public Rectangle sourceRectangle;
        public int frameCount;

        public GameObjectAbstract(Game givenGameame, SpriteBatch givenSpriteBatch)
            : base(givenGameame)
        {

            spriteBatch = givenSpriteBatch;
            game = givenGameame;

            isAlive = false;
            position = new Vector2(0, 0);
            velocity = new Vector2(0, 0);
            facing = new Vector2(0, 0);

            color = Color.White;
            origin = new Vector2(0, 0);
            rotation = 0f;
            scale = 1f;

            isWallBounce = true;

            initialHP = 100;
            currentHP = initialHP;

            wallBounceDampningFactor = 1f;

            speed = 1.0f;


            orientationList.North = new Vector2(0, 1);
            orientationList.NorthEast = new Vector2(1, 1);
            orientationList.East = new Vector2(1, 0);
            orientationList.SouthEast = new Vector2(1, -1);
            orientationList.South = new Vector2(0, -1);
            orientationList.SouthWest = new Vector2(-1, -1);
            orientationList.West = new Vector2(-1, 0);
            orientationList.NorthWest = new Vector2(-1, 1);

            orientation = orientationList.North;

            turnCooldown = 100;
            timerCounter = 0;

            frameIndex = 0;
            frameCount = 8;
        }


        public override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            origin = new Vector2((texture.Width / 2) * scale, (texture.Height / 2) * scale);

            base.LoadContent();
        }


        protected override void Dispose(bool disposing)
        {
            texture.Dispose();
            spriteBatch.Dispose();
           // game.Dispose();
            base.Dispose(disposing);
        }



        public override void Update(GameTime gameTime)
        {

             sourceRectangle = new Rectangle(frameIndex * texture.Width / frameCount, 0, texture.Width / frameCount, texture.Height);
       

             origin = new Vector2(((texture.Width/frameCount) /2) * scale, (texture.Height / 2) * scale);

            

            if (this.isAlive)
            {


                /*
                if (isWallBounce)
                    wallBounce();


                if (currentHP <= 0)
                    this.isAlive = false;
                 * 
                 * */

                timerCounter += gameTime.ElapsedGameTime.Milliseconds;
                if (timerCounter > turnCooldown)
                {
                    this.SingleTurn();
                    timerCounter = 0;
                }


                //set position relative to tile

                position.X = tilePosition.X * texture.Width / frameCount +200;
                position.Y = tilePosition.Y * texture.Height + 200;

            }

            /*
            if (isAlive == false)
                this.reset();
             * */

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            if (isAlive)
            {
                //spriteBatch.Draw(texture, position, null, color, rotation, origin, scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(texture, position, sourceRectangle, color, 0f, origin, scale, SpriteEffects.None, 0);
            }
            frameIndex++;
            if (frameIndex == frameCount)
                frameIndex = 0;

            base.Draw(gameTime);
        }

        // Custom functions

        public Rectangle getRect()
        {
            Rectangle myRect = new Rectangle((int)(position.X - origin.X), (int)(position.Y - origin.Y), (int)(texture.Width * scale), (int)(texture.Height * scale));
            return myRect;
        }

        public bool isInsideScreen()
        {
            Rectangle myRect = getRect();
            return (Game1.screenRectangle.Contains(myRect));
        }

        public bool isIntersectingScreen()
        {
            Rectangle myRect = getRect();
            return (Game1.screenRectangle.Intersects(myRect));
        }

        // smaller object must passed as otherObjRect
        public bool isCollidingOtherObject(Rectangle otherObjRect)
        {
            Rectangle myRect = getRect();
            if (myRect.Intersects(otherObjRect) || myRect.Contains(otherObjRect))
                return true;
            else
                return false;

        }

        public void reset()
        {
            this.position = new Vector2(0, 0);
            this.velocity = new Vector2(0, 0);
            this.speed = 1.0f;
            this.rotation = 0f;
            this.currentHP = this.initialHP;
            this.isAlive = false;

        }

        private void wallBounce()
        {
            float leftBound = this.position.X - this.texture.Width * this.scale / 2;
            float rightBound = this.position.X + this.texture.Width * this.scale / 2;
            float topBound = this.position.Y - this.texture.Height * this.scale / 2;
            float bottomBound = this.position.Y + this.texture.Height * this.scale / 2;

            if (leftBound <= 0 || rightBound >= Game1.screenWidth)
                this.velocity.X = -this.velocity.X * wallBounceDampningFactor;
            if (topBound <= 0 || bottomBound >= Game1.screenHeight)
                this.velocity.Y = -this.velocity.Y * wallBounceDampningFactor;
        }




        /////Map based functions

        
        public bool CheckIfTileFree(Vector2 directionToCheck)
        {
            if (isNextTileInRing())
            {

                Vector2 tileToCheck = tilePosition + directionToCheck;



                if (GameFlowManager.sharedGameFlowManager.mapArray[(int)tileToCheck.X][(int)tileToCheck.Y] == null)
                {
                    return true;
                }
                else
                    return false;

            }
            else
            {
                return false;
            }

        }

        public void MoveSingleStep()
        {
       

                //delete old postion
                GameFlowManager.sharedGameFlowManager.mapArray[(int)tilePosition.X][(int)tilePosition.Y] = null;
                    //inert new position
                tilePosition = tilePosition + orientation;
              // (GameFlowManager.sharedGameFlowManager.mapArray
                GameFlowManager.sharedGameFlowManager.mapArray[(int)tilePosition.X][(int)tilePosition.Y] = this;

            

        }

        public bool isNextTileInRing()
        {
            Vector2 nextTilePosition = tilePosition + orientation;
            int mapSize = GameFlowManager.sharedGameFlowManager.mapSize;

            if( 
                ((int)nextTilePosition.X >= mapSize || (int)nextTilePosition.X < 0)
                ||
                ((int)nextTilePosition.Y >= mapSize || (int)nextTilePosition.Y < 0)
            )
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        public virtual void SingleTurn()
        {
            //Do one move

        }

    }

}