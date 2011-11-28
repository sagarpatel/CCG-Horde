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
    public class KingObject : GameObjectAbstract
    {



        public KingObject(Game game, SpriteBatch givenSpriteBatch)
            : base(game, givenSpriteBatch)
        {
            myType = ObjectType.King;
        }


        protected override void LoadContent()
        {

            texture = TextureManager.sharedTextureManager.getTexture("king");


            facing = new Vector2(0, 0);
            isAlive = true;
            isWallBounce = false;

            turnCooldown = 500;


            base.LoadContent();
        }



        public override void Update(GameTime gameTime)
        {
            if (this.isAlive)
            {

               
            }
            

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Draw(texture, position, sourceRectangle, color, 0f, origin, scale, SpriteEffects.None, 0);
            
           // base.Draw(gameTime);
        }


        //Custom functions

        public override void SingleTurn()
        {
            /*
            if (CheckIfTileFree(this.orientation))
            {
                MoveSingleStep();
            }
            else
            {
                ///kill unit on destination tile
                if (orientation == orientationList.North)
                {
                    orientation = orientationList.East;
                }
                else if (orientation == orientationList.East)
                {
                    orientation = orientationList.South;
                }
                else if (orientation == orientationList.South)
                {
                    orientation = orientationList.West;
                }
                else if (orientation == orientationList.West)
                {
                    orientation = orientationList.North;
                }


            }


            base.SingleTurn();
             * */
        }




    }
}
