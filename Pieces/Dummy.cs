using System;
using SplashKitSDK;

namespace CC
{
    public class Dummy:Piece{
        public Dummy(int x, int y):base(x,y){
            Colour = SColour.w;
        }

        public override void Draw(){
            Bitmap D = new Bitmap("D", "king_w.png");

            SplashKit.DrawBitmap(D, X, Y);
        }
    }
}