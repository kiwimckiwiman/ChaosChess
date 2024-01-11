using System;
using SplashKitSDK;

namespace CC
{
    public class KnightW:Piece{
        public KnightW(int x, int y):base(x,y){
            Colour = SColour.w;
            Type = PieceType.Knight;
        }

        public override void Draw(){
            Bitmap KnW = new Bitmap("KnW", "knight_w.png");

            SplashKit.DrawBitmap(KnW, X, Y);

        }
    }

    public class KnightB:Piece{
        public KnightB(int x, int y):base(x, y){
            Colour = SColour.b;
            Type = PieceType.Knight;
        }

        public override void Draw(){
            Bitmap KnB = new Bitmap("KnB", "knight_b.png");

            SplashKit.DrawBitmap(KnB, X, Y);

        }
    }
}