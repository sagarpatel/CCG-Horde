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
    
    

    public class CreepyCarnivalLevel:ScreenAbstract
    {

        PawnObject[] pawnArray;
        int pawnCount;

        int spawnCounter;
        int spawnCooldown;

        public CreepyCarnivalLevel(Game game, SpriteBatch givenSpriteBatch): base(game, givenSpriteBatch)
        {

            pawnCount = 10;
            pawnArray = new PawnObject[pawnCount];
            for (int i = 0; i < pawnCount; ++i)
            {
                pawnArray[i] = new PawnObject(game, givenSpriteBatch);
                pawnArray[i].isAlive = false;
                Game.Components.Add(pawnArray[i]);
            }


            spawnCooldown = 500;
            spawnCounter = 0;
        }

        public override void Update(GameTime gameTime)
        {
            spawnCounter += gameTime.ElapsedGameTime.Milliseconds;;
            if (spawnCounter >= spawnCooldown)
            {
                SpawnPawn(gameTime);
                
            }



            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            int boardOffsetX = 300;
            int boardOffsetY = 100;

            spriteBatch.Draw(TextureManager.sharedTextureManager.getTexture("board"), new Vector2(boardOffsetX, boardOffsetY),null,Color.White,0f,new Vector2(0,0),1,SpriteEffects.None,0.9f);
            spriteBatch.Draw(TextureManager.sharedTextureManager.getTexture("background"), new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 1, SpriteEffects.None, 1f);
            

            base.Draw(gameTime);
            
        }




        public void SpawnPawn(GameTime gameTime)
        {
            Random rand =  new Random();

            foreach (GameObjectAbstract pawn in pawnArray)
            {
                if (pawn.isAlive == false)
                {
                    Vector2 spawnPosition = new Vector2(0, rand.Next(0, 7));

                    if (GameFlowManager.sharedGameFlowManager.mapArray[(int)spawnPosition.X][(int)spawnPosition.Y] == null)
                    {

                        pawn.tilePosition = spawnPosition;
                        pawn.orientation = pawn.orientationList.East;
                        pawn.isAlive = true;
                        spawnCounter = 0;
                        break;
                    }
                }

            }
          

        }


    }






}
