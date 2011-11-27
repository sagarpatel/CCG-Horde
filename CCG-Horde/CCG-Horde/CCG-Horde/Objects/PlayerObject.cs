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

    public class PlayerObject : GameObjectAbstract
    {

       // SimpleWeapon simpleWeapon;

        int currentWeaponIndex;
        int maxWeaponIndex;

        public PlayerObject(Game game, SpriteBatch givenSpriteBatch)
            : base(game, givenSpriteBatch)
        {

        }


        protected override void LoadContent()
        {

            texture = TextureManager.sharedTextureManager.getTexture("player");
            
            Vector2 actualPosition = new Vector2(400, 300);
            Rectangle phoneFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
          

            facing = new Vector2(0, 0);
            isAlive = true;
            isWallBounce = false;


           // simpleWeapon = new SimpleWeapon(game, spriteBatch, 50);
            //simpleWeapon.fireCooldown = 75;


            currentWeaponIndex = 0;
            maxWeaponIndex = 0;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.isAlive)
            {
                UpdateInput();

                //Update position
                /*
                Vector2 actualPosition = InputManager.sharedInputManager.getTouchPosition();
                Rectangle phoneFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                position = getEdgePosition_setFacing(actualPosition, phoneFrame);


                //Handle weapons
                if (InputManager.sharedInputManager.getIsDoubleTap())
                {
                    //currentWeaponIndex++;
                    //if (currentWeaponIndex > maxWeaponIndex)
                    //    currentWeaponIndex = 0;
                    if (simpleWeapon.isWallBouncing)
                        simpleWeapon.setWallBounce(false);
                    else
                        simpleWeapon.setWallBounce(true);
                }

                HandleWeapons(gameTime);
                 * */

            }
            base.Update(gameTime);
        }

        float vibrationAmount = 0.0f;











        void UpdateInput(){
            // Get the current gamepad state.
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);
            if (currentState.IsConnected && currentState.DPad.Up ==ButtonState.Pressed){
                // Button A is currently being pressed; add vibration.
                orientation = orientationList.South;
            }if (currentState.IsConnected && currentState.DPad.Right ==ButtonState.Pressed){
                // Button A is currently being pressed; add vibration.
                orientation = orientationList.East;
            }if (currentState.IsConnected && currentState.DPad.Down ==ButtonState.Pressed){
                // Button A is currently being pressed; add vibration.
                orientation = orientationList.North;
            }if (currentState.IsConnected && currentState.DPad.Left ==ButtonState.Pressed){
                // Button A is currently being pressed; add vibration.
                orientation = orientationList.West;
            }

            if (currentState.IsConnected && currentState.DPad.Up ==ButtonState.Pressed && currentState.DPad.Left ==ButtonState.Pressed){
                // Button A is currently being pressed; add vibration.
                orientation = orientationList.SouthWest;
            }
            if (currentState.IsConnected && currentState.DPad.Up == ButtonState.Pressed && currentState.DPad.Right == ButtonState.Pressed)
            {
                // Button A is currently being pressed; add vibration.
                orientation = orientationList.SouthEast;
            }
            if (currentState.IsConnected && currentState.DPad.Down == ButtonState.Pressed && currentState.DPad.Right == ButtonState.Pressed)
            {
                // Button A is currently being pressed; add vibration.
                orientation = orientationList.NorthEast;
            }
            if (currentState.IsConnected && currentState.DPad.Down == ButtonState.Pressed && currentState.DPad.Left == ButtonState.Pressed)
            {
                // Button A is currently being pressed; add vibration.
                orientation = orientationList.NorthWest;
            }
 
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
                    Vector2 targetPosition = target.tilePosition;
                    target.isAlive = false;
                    GameFlowManager.sharedGameFlowManager.mapArray[(int)targetPosition.X][(int)targetPosition.Y] = null;

                  //  GameFlowManager.sharedGameFlowManager.mapArray[(int)targetPosition.X][(int)targetPosition.Y] = this;
                    this.position = targetPosition;


                }



            }
            
            
            base.SingleTurn();
        }




    }




}
