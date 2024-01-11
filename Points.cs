using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace CC
{
    public class Points{
        private int _bChecks;

        public Points(){
            _bChecks = 15;
        }

        /// <summary>
        /// If black places white in a check, black gains a life while white loses one
        /// </summary>
        public void WhiteCheck(){
            _bChecks++;
        }

        /// <summary>
        /// If white places black in a check, white gains a life while black loses one
        /// </summary>
        public void BlackCheck(){
            _bChecks--;
        }
        
        /// <summary>
        /// Resets the lives when the reset button is pressed
        /// </summary>
        public void ResetChaos(){
            SplashKit.FillRectangle(Color.Orange, 1180, 280, 300, 100);
            SplashKit.DrawText("Reset", Color.RGBColor(33, 0 , 127), "Gothic", 30, 1285, 310);
            SplashKit.FillRectangle(Color.Orange, 1180, 450, 300, 100);
            SplashKit.DrawText("Chaos", Color.RGBColor(33, 0 , 127), "Gothic", 30, 1285, 480);

            if(SplashKit.MouseClicked(MouseButton.LeftButton)){
                if(SplashKit.MouseX() > 1180 && SplashKit.MouseX() < 1480 && SplashKit.MouseY() > 280 && SplashKit.MouseY() < 380){
                    _bChecks = 15;

                }else if(SplashKit.MouseX() > 1180 && SplashKit.MouseX() < 1480 && SplashKit.MouseY() > 450 && SplashKit.MouseY() < 550){
                    _bChecks = 15;
                }
            }  
        }

        /// <summary>
        /// Visually displays how many lives a player has
        /// </summary>
        public void CheckBar(){
            SplashKit.FillRectangle(Color.White, 260, 125, 25, 600);
            SplashKit.FillRectangle(Color.Black, 260, 125, 25, (_bChecks * 600/30));
            SplashKit.DrawText("Lives: " + _bChecks, Color.RGBColor(33, 0 , 127), "Gothic", 20, 230, 70);
            SplashKit.DrawText("Lives: " + (30 - _bChecks), Color.RGBColor(33, 0 , 127), "Gothic", 20, 230, 750);
        }

        /// <summary>
        /// Declares the player with 30 lives the winner
        /// </summary>
        public void PointsWinner(){
            Game _game = new Game();
            if(_bChecks == 30){
                SplashKit.FillRectangle(Color.Orange, 490, 315, 520, 220);
                SplashKit.FillRectangle(Color.Wheat, 500, 325, 500, 200);
                SplashKit.DrawText("Black Wins!", Color.RGBColor(33, 0 , 127), "Gothic", 50, 630, 390);
            }else if(_bChecks == 0){
                SplashKit.FillRectangle(Color.Orange, 490, 315, 520, 220);
                SplashKit.FillRectangle(Color.Wheat, 500, 325, 500, 200);
                SplashKit.DrawText("White Wins!", Color.RGBColor(33, 0 , 127), "Gothic", 50, 620, 390);
            }
        }
    }
}