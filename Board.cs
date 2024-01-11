using System;
using SplashKitSDK;

namespace CC
{
    public class Board{

        private bool _colour = false;

        public Board(){

        }

        /// <summary>
        /// Visually draws the chess board on screen with alternating colours
        /// </summary>
        public void Draw(){
            SplashKit.FillRectangle(Color.Orange, 340, 15, 820, 820);
            for(int i = 0; i < 8; i++){
                for(int j = 0; j < 8; j++){
                    if(_colour){
                        SplashKit.FillRectangle(Color.RGBColor(66, 0, 105), 350 + (i*100), 25 + (j*100), 100, 100);
                        Colour();
                    }else{
                        SplashKit.FillRectangle(Color.White, 350 + (i*100), 25 + (j*100), 100, 100);
                        Colour();
                    }
                }
                Colour();
            }
        }

        /// <summary>
        /// Switches the colour based on a bool value to help alternate colours on the chess board
        /// </summary>
        public void Colour(){
            if(_colour){
                _colour = false;
            }else{
                _colour = true;
            }
        }
    }
}