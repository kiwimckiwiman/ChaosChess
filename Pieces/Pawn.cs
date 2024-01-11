using System;
using SplashKitSDK;

namespace CC
{
    public class PawnW:Piece{
        
        public PawnW(int x, int y):base(x,y){
            Colour = SColour.w;
            Type = PieceType.WPawn;
        }

        public override void Draw(){
            Bitmap PW = new Bitmap("PW", "pawn_w.png");

            SplashKit.DrawBitmap(PW, X, Y);

        }
    }

    public class PawnB:Piece{
        public PawnB(int x, int y):base(x, y){
            Colour = SColour.b;
            Type = PieceType.BPawn;
        }

        public override void Draw(){
            Bitmap PB = new Bitmap("PB", "pawn_b.png");

            SplashKit.DrawBitmap(PB, X, Y);

        }
    }
}