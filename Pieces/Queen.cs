using System;
using SplashKitSDK;

namespace CC
{
    public class QueenW:Piece{
        public QueenW(int x, int y):base(x,y){
            Colour = SColour.w;
            Type = PieceType.Queen;
        }

        public override void Draw(){
            Bitmap QW = new Bitmap("QW", "queen_w.png");

            SplashKit.DrawBitmap(QW, X, Y);

        }
    }

    public class QueenB:Piece{
        public QueenB(int x, int y):base(x, y){
            Colour = SColour.b;
            Type = PieceType.Queen;
        }

        public override void Draw(){
            Bitmap QB = new Bitmap("QB", "queen_b.png");

            SplashKit.DrawBitmap(QB, X, Y);

        }
    }
}