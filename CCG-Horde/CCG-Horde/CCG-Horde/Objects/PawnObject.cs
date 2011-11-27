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
    public class PawnObject : GameObjectAbstract
    {


        public PawnObject(Game game, SpriteBatch givenSpriteBatch)
            : base(game, givenSpriteBatch)
        {

        }


        protected override void LoadContent()
        {

            texture = TextureManager.sharedTextureManager.getTexture("clown");


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


            base.Draw(gameTime);
        }


        //Custom functions

        public override void SingleTurn()
        {
            if (CheckIfTileFree(this.orientation))
            {
                MoveSingleStep();
            }
            else
            {
                ///kill unit on destination tile
            }


            base.SingleTurn();
        }


    }





}
