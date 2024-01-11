using System;
using SplashKitSDK;

namespace CC
{
    public class BishopW:Piece{
        public BishopW(int x, int y):base(x,y){
            Colour = SColour.w;
            Type = PieceType.Bishop;
        }

        public override void Draw(){
            Bitmap BW = new Bitmap("BW", "bishop_w.png");

            SplashKit.DrawBitmap(BW, X, Y);

        }
    }

    public class BishopB:Piece{
        public BishopB(int x, int y):base(x, y){
            Colour = SColour.b;
            Type = PieceType.Bishop;
        }

        public override void Draw(){
            Bitmap BB = new Bitmap("BB", "bishop_b.png");

            SplashKit.DrawBitmap(BB, X, Y);

        }
    }
}