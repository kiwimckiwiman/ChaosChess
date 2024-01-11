using System;
using SplashKitSDK;

namespace CC
{
    public class KingW:Piece{
        public KingW(int x, int y):base(x,y){
            Colour = SColour.w;
            Type = PieceType.KingW;
        }

        public override void Draw(){
            Bitmap KW = new Bitmap("KW", "king_w.png");

            SplashKit.DrawBitmap(KW, X, Y);

        }
    }

    public class KingB:Piece{
        public KingB(int x, int y):base(x, y){
            Colour = SColour.b;
            Type = PieceType.KingB;
        }

        public override void Draw(){
            Bitmap KB = new Bitmap("KB", "king_b.png");

            SplashKit.DrawBitmap(KB, X, Y);

        }
    }
}