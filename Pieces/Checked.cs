using System;
using SplashKitSDK;

namespace CC
{
    public class Checked:Piece{
        public Checked(int x, int y):base(x,y){

        }
        public override void Draw(){
            SplashKit.FillRectangle(Color.RGBAColor(255, 0, 0, 185), X, Y, 100, 100);
        }
    }
}