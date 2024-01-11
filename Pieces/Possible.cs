using System;
using SplashKitSDK;

namespace CC
{
    public class Possible:Piece{
        public Possible(int x, int y):base(x,y){

        }
        public override void Draw(){
            SplashKit.FillRectangle(Color.RGBAColor(116, 230, 87, 185), X, Y, 100, 100);
        }
    }
}