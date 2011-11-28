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
            myType = ObjectType.Pawn;

        }


        protected override void LoadContent()
        {

            texture = TextureManager.sharedTextureManager.getTexture("clown");


            facing = new Vector2(0, 0);
            isAlive = false;    
            isWallBounce = false;

            turnCooldown = 1500;


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
                
                /// check-->kill unit on destination tile
                /// 

                GameObjectAbstract target = this.getObjectAtNextTile(this.orientation);



                if (target != null)
                {
                    if (target.myType == ObjectType.King)
                    {

                        Vector2 targetPosition = target.tilePosition;
                        target.isAlive = false;
                        GameFlowManager.sharedGameFlowManager.mapArray[(int)targetPosition.X][(int)targetPosition.Y] = null;

                        //  GameFlowManager.sharedGameFlowManager.mapArray[(int)targetPosition.X][(int)targetPosition.Y] = this;
                        this.position = targetPosition;
                        game.Exit();
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

            }


            base.SingleTurn();
        }


    }





}
