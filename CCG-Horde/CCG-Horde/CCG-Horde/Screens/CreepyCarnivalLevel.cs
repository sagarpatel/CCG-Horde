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

        public CreepyCarnivalLevel(Game game, SpriteBatch givenSpriteBatch): base(game, givenSpriteBatch)
        {

            pawnCount = 4;
            pawnArray = new PawnObject[pawnCount];
            for (int i = 0; i < pawnCount; ++i)
            {
                pawnArray[i] = new PawnObject(game, givenSpriteBatch);
                Game.Components.Add(pawnArray[i]);
            }


        }



    }






}
