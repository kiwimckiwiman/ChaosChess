using System;
using SplashKitSDK;

namespace CC
{
    public class Selector:Piece{
        public Selector(int x, int y):base(x,y){

        }
        public override void Draw(){
            Bitmap S = new Bitmap("S", "selector.png");
            
            SplashKit.DrawBitmap(S, X, Y);
        }
    }
}