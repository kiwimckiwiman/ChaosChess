using System;
using SplashKitSDK;

namespace CC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Window Chess2 = new Window("Chess 2", 1500, 850);
            Board _board = new Board();
            Game _game = new Game();
            Points _points = new Points();
            Movement _movement = new Movement();
            
            _game.SetPieces();
            SplashKit.LoadFont("Gothic", "GOTHIC.TTF");
            do{
                SplashKit.ProcessEvents();
                Chess2.Clear(Color.Wheat);
                _board.Draw();
                _game.DrawPieces();
                _game.SelectPiece(_points);
                _game.Skills();
                _game.Rules();
                _points.CheckBar();
                _game.ResetChaos();
                _points.ResetChaos();
                _game.PiecesWinner();
                _points.PointsWinner();
                SplashKit.RefreshScreen(60);
            }while(!SplashKit.QuitRequested());
        }
    }
}
