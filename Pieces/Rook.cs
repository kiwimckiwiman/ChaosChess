using System;
using SplashKitSDK;

namespace CC
{
    public class RookW:Piece{
        public RookW(int x, int y):base(x,y){
            Colour = SColour.w;
            Type = PieceType.Rook;
        }

        public override void Draw(){
            Bitmap RW = new Bitmap("RW", "rook_w.png");

            SplashKit.DrawBitmap(RW, X, Y);

        }
    }

    public class RookB:Piece{
        public RookB(int x, int y):base(x, y){
            Colour = SColour.b;
            Type = PieceType.Rook;
        }

        public override void Draw(){
            Bitmap RB = new Bitmap("RB", "rook_b.png");

            SplashKit.DrawBitmap(RB, X, Y);

        }
    }
}