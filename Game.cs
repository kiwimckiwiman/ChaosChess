using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace CC
{   
    public enum SColour{b, w};
    public class Game{
        private List<Square> _squares;
        private List<Piece> _pieces;
        private Piece _selectedP;
        private Piece _capturedP;
        private Piece _lastP;
        private SColour _player;
        private Piece _Pselected;
        private int _lastC;
        private Movement _moves;
        private int _aPieces;
        private int _wTeleport;
        private int _bTeleport;
        private int _wSwap;
        private int _bSwap;
        private int _wPieces;
        private int _bPieces;
        private List<int> _spots;

        /// <summary>
        /// Initialises the Game class
        /// </summary>
        public Game(){
            _squares = new List<Square>();
            _pieces = new List<Piece>();
            _player = SColour.w;
            _Pselected = new Selector(-2,-2);
            _moves = new Movement();
            _aPieces = 33;
            _wTeleport = 3;
            _bTeleport = 3;
            _wSwap = 3;
            _bSwap = 3;
            _spots = new List<int>();
        }

        /// <summary>
        /// Creates a list of squares with coordinates from (0,0) to (7,7) in alternating colours
        /// </summary>
        public void CreateCoordinates(){
            for(int i = 0; i < 8; i++){
                for(int j = 0; j < 8 ; j++){
                    if(((i + j)%2)==0){
                        Square s = new Square(SColour.w, j, i);
                        _squares.Add(s);
                    }else{
                        Square s = new Square(SColour.b, j, i);
                        _squares.Add(s);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the board to start the game
        /// </summary>
        public void SetPieces(){
            _squares.Clear();
            _pieces.Clear();
            CreateCoordinates();

            Dummy D1 = new Dummy(0,0);
            _selectedP = D1;
            _lastC = 0;

            RookW RW1 = new RookW(_squares[56].X, _squares[56].Y);
            RookW RW2 = new RookW(_squares[63].X, _squares[63].Y);
            RookB RB1 = new RookB(_squares[0].X, _squares[0].Y);
            RookB RB2 = new RookB(_squares[7].X, _squares[7].Y);

            KnightW KnW1 = new KnightW(_squares[57].X, _squares[57].Y);
            KnightW KnW2 = new KnightW(_squares[62].X, _squares[62].Y);
            KnightB KnB1 = new KnightB(_squares[1].X, _squares[1].Y);
            KnightB KnB2= new KnightB(_squares[6].X, _squares[6].Y);

            BishopW BW1 = new BishopW(_squares[58].X, _squares[58].Y);
            BishopW BW2 = new BishopW(_squares[61].X, _squares[61].Y);
            BishopB BB1 = new BishopB(_squares[2].X, _squares[2].Y);
            BishopB BB2 = new BishopB(_squares[5].X, _squares[5].Y);

            KingW KW = new KingW(_squares[60].X, _squares[60].Y);
            KingB KB = new KingB(_squares[4].X, _squares[4].Y);

            QueenW QW = new QueenW(_squares[59].X, _squares[59].Y);
            QueenB QB = new QueenB(_squares[3].X, _squares[3].Y);

            _pieces.Add(RB1);
            _pieces.Add(KnB1);
            _pieces.Add(BB1);
            _pieces.Add(KB);
            _pieces.Add(QB);
            _pieces.Add(BB2);
            _pieces.Add(KnB2);
            _pieces.Add(RB2);

            for (int i = 8; i < 16; i++)
            {
                PawnB PB = new PawnB(_squares[i].X, _squares[i].Y);
                _pieces.Add(PB);
            }


            for (int i = 48; i < 56; i++)
            {
                PawnW PW = new PawnW(_squares[i].X, _squares[i].Y);
                _pieces.Add(PW);
            }

            _pieces.Add(RW1);
            _pieces.Add(KnW1);
            _pieces.Add(BW1);
            _pieces.Add(KW);
            _pieces.Add(QW);
            _pieces.Add(BW2);
            _pieces.Add(KnW2);
            _pieces.Add(RW2);

            _pieces.Add(_Pselected);

            for (int i = 48; i < 64; i++)
            {
                _squares[i].SetOccupied();
            }

            for (int i = 0; i < 16; i++)
            {   
                _squares[i].SetOccupied();
            }
        }

        /// <summary>
        /// Draws the pieces where they should be
        /// </summary>
        public void DrawPieces(){
            foreach(Piece p in _pieces){
                p.Draw();
            }
        }
        
        /// <summary>
        /// Alternates player's turns
        /// </summary>
        public void PlayerTurn(){
            if(_player == SColour.w){
                _player = SColour.b;
            }else if(_player == SColour.b){
                _player = SColour.w;
            }
        }
        
        /// <summary>
        /// Displays visually which player's turn it is
        /// </summary>
        public void Player(){
            if(_player == SColour.w){
                SplashKit.DrawText("White's turn", Color.RGBColor(33, 0 , 127), "Gothic", 40, 1220, 700);
            }else if(_player == SColour.b){
                SplashKit.DrawText("Black's turn", Color.RGBColor(33, 0 , 127), "Gothic", 40, 1220, 90);
            }
        }
        
        /// <summary>
        /// Calculation to turn a point on the screen to the location of the square on the list
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int PixelToSquare(float x, float y){
            double cX  = Math.Floor(((x-350)/100.0));
            double cY = Math.Floor(((y-25)/100.0));
            int c = Convert.ToInt32((cY*8)+cX);

            return c;
        }

        /// <summary>
        /// Resets the game with the starting positions of each piece being randomised
        /// </summary>
        public void Chaos(){
            _squares.Clear();
            _pieces.Clear();
            CreateCoordinates();

            Dummy D1 = new Dummy(0,0);
            _selectedP = D1;
            _lastC = 0;

            Random _select = new Random();
            _spots.Clear();

            for (int i = 0; i < 64; i++){
                _spots.Add(i);
            }

            int num = _select.Next(0, _spots.Count);
            RookW RW1 = new RookW(_squares[_spots[num]].X, _squares[_spots[num]].Y);
            _squares[_spots[num]].SetOccupied();
            _spots.RemoveAt(num);

            num = _select.Next(0, _spots.Count);
            RookW RW2 = new RookW(_squares[_spots[num]].X, _squares[_spots[num]].Y);
            _squares[_spots[num]].SetOccupied();
            _spots.RemoveAt(num);

            num = _select.Next(0, _spots.Count);
            RookB RB1 = new RookB(_squares[_spots[num]].X, _squares[_spots[num]].Y);
            _squares[_spots[num]].SetOccupied();
            _spots.RemoveAt(num);

            num = _select.Next(0, _spots.Count);
            RookB RB2 = new RookB(_squares[_spots[num]].X, _squares[_spots[num]].Y);
            _squares[_spots[num]].SetOccupied();
            _spots.RemoveAt(num);

            num = _select.Next(0, _spots.Count);
            KnightW KnW1 = new KnightW(_squares[_spots[num]].X, _squares[_spots[num]].Y);
            _squares[_spots[num]].SetOccupied();
            _spots.RemoveAt(num);

            num = _select.Next(0, _spots.Count);
            KnightW KnW2 = new KnightW(_squares[_spots[num]].X, _squares[_spots[num]].Y);
            _squares[_spots[num]].SetOccupied();
            _spots.RemoveAt(num);

            num = _select.Next(0, _spots.Count);
            KnightB KnB1 = new KnightB(_squares[_spots[num]].X, _squares[_spots[num]].Y);
            _squares[_spots[num]].SetOccupied();
            _spots.RemoveAt(num);

            num = _select.Next(0, _spots.Count);
            KnightB KnB2= new KnightB(_squares[_spots[num]].X, _squares[_spots[num]].Y);
            _squares[_spots[num]].SetOccupied();
            _spots.RemoveAt(num);

            num = _select.Next(0, _spots.Count);
            BishopW BW1 = new BishopW(_squares[_spots[num]].X, _squares[_spots[num]].Y);
            _squares[_spots[num]].SetOccupied();
            _spots.RemoveAt(num);

            num = _select.Next(0, _spots.Count);
            BishopW BW2 = new BishopW(_squares[_spots[num]].X, _squares[_spots[num]].Y);
            _squares[_spots[num]].SetOccupied();
            _spots.RemoveAt(num);

            num = _select.Next(0, _spots.Count);
            BishopB BB1 = new BishopB(_squares[_spots[num]].X, _squares[_spots[num]].Y);
            _squares[_spots[num]].SetOccupied();
            _spots.RemoveAt(num);

            num = _select.Next(0, _spots.Count);
            BishopB BB2 = new BishopB(_squares[_spots[num]].X, _squares[_spots[num]].Y);
            _squares[_spots[num]].SetOccupied();
            _spots.RemoveAt(num);

            num = _select.Next(0, _spots.Count);
            QueenW QW = new QueenW(_squares[_spots[num]].X, _squares[_spots[num]].Y);
            _squares[_spots[num]].SetOccupied();
            _spots.RemoveAt(num);

            num = _select.Next(0, _spots.Count);
            QueenB QB = new QueenB(_squares[_spots[num]].X, _squares[_spots[num]].Y);
            _squares[_spots[num]].SetOccupied();
            _spots.RemoveAt(num);

            for (int i = 0; i < 8; i++){   
                num = _select.Next(0, _spots.Count);
                PawnB PB = new PawnB(_squares[_spots[num]].X, _squares[_spots[num]].Y);
                _squares[_spots[num]].SetOccupied();
                _spots.RemoveAt(num);
                _pieces.Add(PB);
            }

            for (int i = 0; i < 8; i++){
                num = _select.Next(0, _spots.Count);
                PawnW PW = new PawnW(_squares[_spots[num]].X, _squares[_spots[num]].Y);
                _squares[_spots[num]].SetOccupied();
                _spots.RemoveAt(num);
                _pieces.Add(PW);
            }

            num = _select.Next(0, _spots.Count);
            KingW KW = new KingW(_squares[_spots[num]].X, _squares[_spots[num]].Y);
            _squares[_spots[num]].SetOccupied();
            _spots.RemoveAt(num);

            num = _select.Next(0, _spots.Count);
            KingB KB = new KingB(_squares[_spots[num]].X, _squares[_spots[num]].Y);
            _squares[_spots[num]].SetOccupied();
            _spots.RemoveAt(num);

            _pieces.Add(RB1);
            _pieces.Add(KnB1);
            _pieces.Add(BB1);
            _pieces.Add(KB);
            _pieces.Add(QB);
            _pieces.Add(BB2);
            _pieces.Add(KnB2);
            _pieces.Add(RB2);

            

            _pieces.Add(RW1);
            _pieces.Add(KnW1);
            _pieces.Add(BW1);
            _pieces.Add(KW);
            _pieces.Add(QW);
            _pieces.Add(BW2);
            _pieces.Add(KnW2);
            _pieces.Add(RW2);

            _pieces.Add(_Pselected);      
        }

        /// <summary>
        /// Buttons to reset the game or reset with chaos
        /// </summary>
        public void ResetChaos(){
            SplashKit.FillRectangle(Color.Orange, 1180, 280, 300, 100);
            SplashKit.DrawText("Reset", Color.RGBColor(33, 0 , 127), "Gothic", 30, 1285, 310);
            SplashKit.FillRectangle(Color.Orange, 1180, 450, 300, 100);
            SplashKit.DrawText("Chaos", Color.RGBColor(33, 0 , 127), "Gothic", 30, 1285, 480);

            if(SplashKit.MouseClicked(MouseButton.LeftButton)){
                if(SplashKit.MouseX() > 1180 && SplashKit.MouseX() < 1480 && SplashKit.MouseY() > 280 && SplashKit.MouseY() < 380){
                    Points _points = new Points();
                    SetPieces();
                    _aPieces = 33;
                    _player = SColour.w;
                    _wTeleport = 3;
                    _bTeleport = 3;
                    _wSwap = 3;
                    _bSwap = 3;

                }else if(SplashKit.MouseX() > 1180 && SplashKit.MouseX() < 1480 && SplashKit.MouseY() > 450 && SplashKit.MouseY() < 550){
                    Chaos();
                    _aPieces = 33;
                    _player = SColour.w;
                    _wTeleport = 3;
                    _bTeleport = 3;
                    _wSwap = 3;
                    _bSwap = 3;
                }
            }
        }
        
        /// <summary>
        /// Allows user to pick the correct piece to move
        /// </summary>
        public void SelectPiece(Points _points){
            
            _moves.Check(_pieces, _squares); 
            Player();
            if(SplashKit.MouseUp(MouseButton.LeftButton)){
                if(SplashKit.MouseX() > 351 && SplashKit.MouseX() < 1149 && SplashKit.MouseY() > 26 && SplashKit.MouseY() < 824){
                    if(SplashKit.MouseClicked(MouseButton.LeftButton)){
                        int _selectedC = PixelToSquare(SplashKit.MouseX(), SplashKit.MouseY());
                        _lastC = _selectedC;
                        _lastP = _selectedP;
                        if(_lastP.Moving){
                            _lastP.isMove();
                            _squares[_lastP.Coordinate()].SetOccupied();
                        }

                        if(_squares[_selectedC].Occ){
                            int index = _pieces.FindIndex(s => s.Coordinate() == _selectedC);
                            _selectedP = _pieces[index];
                            if(_selectedP.Colour == _player){    
                                _pieces.RemoveRange(_aPieces, _pieces.Count - _aPieces);
                                                  
                                _moves.PossibleMoves(_selectedC, _selectedP, _squares, _pieces);
                                _selectedP.isMove();
                                _squares[_selectedC].SetOccupied();
                                _Pselected.X = _squares[_selectedC].X;
                                _Pselected.Y = _squares[_selectedC].Y; 
                                
                            }else{
                                _pieces.RemoveRange(_aPieces, _pieces.Count - _aPieces);
                                _Pselected.X = -2;
                                _Pselected.Y = -2;
                            }
                        }else{
                                _pieces.RemoveRange(_aPieces, _pieces.Count - _aPieces);
                                _Pselected.X = -2;
                                _Pselected.Y = -2;
                        }
                    }
                }
                MovePiece(_points);
            }
        }

        /// <summary>
        /// Allows player to set the piece down on an empty square, capture a piece, teleport their piece to an
        /// empty square or swap positions with another piece
        /// </summary>
        public void MovePiece(Points _points){
            Random _rng = new Random();
            if(SplashKit.MouseClicked(MouseButton.RightButton) && _selectedP.Moving){
                if(SplashKit.MouseX() > 351 && SplashKit.MouseX() < 1149 && SplashKit.MouseY() > 26 && SplashKit.MouseY() < 824){
                    int _targetC = PixelToSquare(SplashKit.MouseX(), SplashKit.MouseY());
                    
                    if(_moves.PieceMovements(_selectedP.Coordinate(), _selectedP, _targetC, _squares, _pieces)){
                        if((_squares[_targetC].Occ) && !(_lastC == _targetC)){

                            int index = _pieces.FindIndex(s => s.Coordinate() == _targetC);
                            if(!(_pieces[index].Colour == _selectedP.Colour) && !(_pieces[index].Type == PieceType.KingW) && !(_pieces[index].Type == PieceType.KingB)){
                                SplashKit.LoadMusic("Place", "PiecePlace.mp3");  
                                SplashKit.PlayMusic("Place");
                                _capturedP = _pieces[index];
                                _pieces.Remove(_capturedP);

                                _selectedP.X = _squares[_targetC].X;
                                _selectedP.Y = _squares[_targetC].Y;

                                _selectedP.isMove();

                                _Pselected.X = -2;
                                _Pselected.Y = -2;

                                _aPieces--;
                                _pieces.RemoveRange(_aPieces, _pieces.Count - _aPieces);                    
                                
                                PlayerTurn();
                                Promotion(_selectedP);
                                _moves.CheckPoints(_pieces, _squares, _points);

                                //Sounds(_rng.Next(1,5)); 
                            }

                        }else if(!(_squares[_targetC].Occ) && !(_lastC == _targetC)){
                            SplashKit.LoadMusic("Place", "PiecePlace.mp3");  
                            SplashKit.PlayMusic("Place");
                            _squares[_targetC].SetOccupied();
                            SplashKit.Delay(100);

                            _selectedP.X = _squares[_targetC].X;
                            _selectedP.Y = _squares[_targetC].Y;
                            _selectedP.isMove();

                            _Pselected.X = -2;
                            _Pselected.Y = -2;

                            _pieces.RemoveRange(_aPieces, _pieces.Count - _aPieces);

                            PlayerTurn();
                            Promotion(_selectedP);
                            _moves.CheckPoints(_pieces, _squares, _points); 
                        }
                    }
                }
            }else if(SplashKit.KeyTyped(KeyCode.TKey)){
                if((_player == SColour.w) && !(_wTeleport == 0)){
                    if(SplashKit.MouseX() > 351 && SplashKit.MouseX() < 1149 && SplashKit.MouseY() > 26 && SplashKit.MouseY() < 824){
                        int _targetC = PixelToSquare(SplashKit.MouseX(), SplashKit.MouseY());
                        if(!_squares[_targetC].Occ && (_selectedP.Colour == _player)){
                            SplashKit.LoadMusic("Place", "PiecePlace.mp3");  
                            SplashKit.PlayMusic("Place");
                            _squares[_targetC].SetOccupied();
                            int index = _pieces.FindIndex(s => s.Coordinate() == _targetC);
                            _selectedP.X = _squares[_targetC].X;
                            _selectedP.Y = _squares[_targetC].Y;

                            _selectedP.isMove();

                            _Pselected.X = -2;
                            _Pselected.Y = -2;
                            _pieces.RemoveRange(_aPieces, _pieces.Count - _aPieces);

                            PlayerTurn();
                            Promotion(_selectedP);
                            _wTeleport--;
                            _moves.CheckPoints(_pieces, _squares, _points);
                        }
                    }
                }else if((_player == SColour.b) && !(_bTeleport == 0)){
                    if(SplashKit.MouseX() > 351 && SplashKit.MouseX() < 1149 && SplashKit.MouseY() > 26 && SplashKit.MouseY() < 824){
                        int _targetC = PixelToSquare(SplashKit.MouseX(), SplashKit.MouseY());
                        if(!_squares[_targetC].Occ && (_selectedP.Colour == _player)){
                            SplashKit.LoadMusic("Place", "PiecePlace.mp3");  
                            SplashKit.PlayMusic("Place");
                            _squares[_targetC].SetOccupied();
                            int index = _pieces.FindIndex(s => s.Coordinate() == _targetC);
                            _selectedP.X = _squares[_targetC].X;
                            _selectedP.Y = _squares[_targetC].Y;

                            _selectedP.isMove();

                            _Pselected.X = -2;
                            _Pselected.Y = -2;
                            _pieces.RemoveRange(_aPieces, _pieces.Count - _aPieces);

                            PlayerTurn();
                            Promotion(_selectedP);
                            _bTeleport--;
                            _moves.CheckPoints(_pieces, _squares, _points);  
                        }
                    }
                }  
            }else if(SplashKit.KeyTyped(KeyCode.SKey)){
                if((_player == SColour.w) && !(_wSwap == 0)){
                    if(SplashKit.MouseX() > 351 && SplashKit.MouseX() < 1149 && SplashKit.MouseY() > 26 && SplashKit.MouseY() < 824){
                        int _targetC = PixelToSquare(SplashKit.MouseX(), SplashKit.MouseY()); 
                        if(_squares[_targetC].Occ && (_selectedP.Colour == _player) && _targetC > 7 && _targetC < 56 && _selectedP.Coordinate() > 7 &&_selectedP.Coordinate() < 56){
                            SplashKit.LoadMusic("Place", "PiecePlace.mp3");  
                            SplashKit.PlayMusic("Place");
                            int index = _pieces.FindIndex(s => s.Coordinate() == _targetC);
                            int square = _squares.FindIndex(s => s.Coordinate() == _selectedP.Coordinate());
                            _selectedP.X = _squares[_targetC].X;
                            _selectedP.Y = _squares[_targetC].Y;

                            _pieces[index].X = _squares[square].X;
                            _pieces[index].Y = _squares[square].Y;

                            _squares[square].SetOccupied();
                            _selectedP.isMove();
                            _Pselected.X = -2;
                            _Pselected.Y = -2;
                            _pieces.RemoveRange(_aPieces, _pieces.Count - _aPieces);
                            PlayerTurn();
                            Promotion(_selectedP);
                            _wSwap--;
                            _moves.CheckPoints(_pieces, _squares, _points); 
                        }                                               
                    }
                }else if((_player == SColour.b) && !(_bSwap == 0)){
                    if(SplashKit.MouseX() > 351 && SplashKit.MouseX() < 1149 && SplashKit.MouseY() > 26 && SplashKit.MouseY() < 824){
                        int _targetC = PixelToSquare(SplashKit.MouseX(), SplashKit.MouseY()); 
                        if(_squares[_targetC].Occ && (_selectedP.Colour == _player) && _targetC > 7 && _targetC < 56 && _selectedP.Coordinate() > 7 &&_selectedP.Coordinate() < 56){
                            SplashKit.LoadMusic("Place", "PiecePlace.mp3");  
                            SplashKit.PlayMusic("Place");
                            int index = _pieces.FindIndex(s => s.Coordinate() == _targetC);
                            int square = _squares.FindIndex(s => s.Coordinate() == _selectedP.Coordinate());
                            _selectedP.X = _squares[_targetC].X;
                            _selectedP.Y = _squares[_targetC].Y;

                            _pieces[index].X = _squares[square].X;
                            _pieces[index].Y = _squares[square].Y;

                            _squares[square].SetOccupied();
                            _selectedP.isMove();
                            _Pselected.X = -2;
                            _Pselected.Y = -2;
                            _pieces.RemoveRange(_aPieces, _pieces.Count - _aPieces);
                            PlayerTurn();
                            Promotion(_selectedP);
                            _bSwap--;
                            _moves.CheckPoints(_pieces, _squares, _points);
                        }                                               
                    }
                }  
            }   
        }
        
        /// <summary>
        /// Allows a Pawn that has reached the otherside of the board to be promoted toi another piece at random
        /// </summary>
        public void Promotion(Piece _piece){
            Random _rng = new Random();
            if((_piece.Type == PieceType.WPawn) && (_piece.Coordinate() < 8)){
                int _num = _rng.Next(1, 6);
                if((_num == 1)){
                    int square = _squares.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    QueenW QW = new QueenW(_squares[square].X, _squares[square].Y);
                    int index = _pieces.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    _pieces.Remove(_piece);
                    _pieces.Insert(index, QW);
                    //Sounds(_rng.Next(13,15));

                }else if((_num == 2)){
                    int square = _squares.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    BishopW BW = new BishopW(_squares[square].X, _squares[square].Y);
                    int index = _pieces.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    _pieces.Remove(_piece);
                    _pieces.Insert(index, BW);

                    //Sounds(_rng.Next(13,15));

                }else if((_num == 3)){
                    int square = _squares.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    RookW RW = new RookW(_squares[square].X, _squares[square].Y);
                    int index = _pieces.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    _pieces.Remove(_piece);
                    _pieces.Insert(index, RW);
                    //Sounds(_rng.Next(13,15));

                }else if((_num == 4)){
                    int square = _squares.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    KnightW KnW = new KnightW(_squares[square].X, _squares[square].Y);
                    int index = _pieces.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    _pieces.Remove(_piece);
                    _pieces.Insert(index, KnW);
                    //Sounds(_rng.Next(13,15));

                }else if((_num == 5)){
                    int square = _squares.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    PawnB PB = new PawnB(_squares[square].X, _squares[square].Y);
                    int index = _pieces.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    _pieces.Remove(_piece);
                    _pieces.Insert(index, PB);
                    //Sounds(_rng.Next(17,19));
                }

            }else if((_piece.Type == PieceType.BPawn) && (_piece.Coordinate() > 55)){
                int _num = _rng.Next(1, 6);
                if((_num == 1)){
                    int square = _squares.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    QueenB QB = new QueenB(_squares[square].X, _squares[square].Y);
                    int index = _pieces.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    _pieces.Remove(_piece);
                    _pieces.Insert(index, QB);
                    //Sounds(_rng.Next(13,15));

                }else if((_num == 2)){
                    int square = _squares.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    BishopB BB = new BishopB(_squares[square].X, _squares[square].Y);
                    int index = _pieces.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    _pieces.Remove(_piece);
                    _pieces.Insert(index, BB);
                    //Sounds(_rng.Next(13,15));

                }else if((_num == 3)){
                    int square = _squares.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    RookB RB = new RookB(_squares[square].X, _squares[square].Y);
                    int index = _pieces.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    _pieces.Remove(_piece);
                    _pieces.Insert(index, RB);
                    //Sounds(_rng.Next(13,15));

                }else if((_num == 4)){
                    int square = _squares.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    KnightB KnB = new KnightB(_squares[square].X, _squares[square].Y);
                    int index = _pieces.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    _pieces.Remove(_piece);
                    _pieces.Insert(index, KnB);
                    //Sounds(_rng.Next(13,15));

                }else if((_num == 5)){
                    int square = _squares.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    PawnW PW = new PawnW(_squares[square].X, _squares[square].Y);
                    int index = _pieces.FindIndex(s => s.Coordinate() == _piece.Coordinate());
                    _pieces.Remove(_piece);
                    _pieces.Insert(index, PW);
                    //Sounds(_rng.Next(17,19));
                }
            }
        }

        /// <summary>
        /// Sounds added when an event happens purely for fun
        /// </summary>
        /*public void Sounds(int i){
            SplashKit.LoadMusic("Bong", "Bong.mp3");
            SplashKit.LoadMusic("Bonk", "Bonk.mp3");
            SplashKit.LoadMusic("Minecraft_Death", "Minecraft_Death.mp3");
            
            SplashKit.LoadMusic("Fnaf2_ambience", "Fnaf2_ambience.mp3");
            SplashKit.LoadMusic("Minecraft_Ambience", "Minecraft_Ambience.mp3");
            SplashKit.LoadMusic("XP", "XP.mp3");

            SplashKit.LoadMusic("Gangsta's_Paradise", "Gangsta's_Paradise.mp3");
            
            SplashKit.LoadMusic("Imposter", "imposter.mp3");
            SplashKit.LoadMusic("What", "What.mp3");
            

            if(i == 1){
                SplashKit.PlayMusic("Bong");
            }else if(i == 2){
                SplashKit.PlayMusic("Bonk");
            }else if(i == 3){
                SplashKit.PlayMusic("Minecraft_Death");
            }else if(i == 7){
                SplashKit.PlayMusic("Fnaf2_ambience");
            }else if(i == 8){
                SplashKit.PlayMusic("Minecraft_Ambience");
            }else if(i == 9){
                SplashKit.PlayMusic("XP");
            }else if(i == 13){
                SplashKit.PlayMusic("Gangsta's_Paradise");
            }else if(i == 17){
                SplashKit.PlayMusic("Imposter");
            }else if(i == 18){
                SplashKit.PlayMusic("What");
            }
        }*/

        /// <summary>
        /// Determines winner if the player captures all of the other player's pieces
        /// </summary>
        public void PiecesWinner(){
            _wPieces = 0;
            _bPieces = 0;
            foreach(Piece p in _pieces){
                if(p.Colour == SColour.w){
                    _wPieces++;
                }else if(p.Colour == SColour.b){
                    _bPieces++;
                }
            }

            if(_wPieces == 1){
                SplashKit.FillRectangle(Color.Orange, 490, 315, 520, 220);
                SplashKit.FillRectangle(Color.Wheat, 500, 325, 500, 200);
                SplashKit.DrawText("Black Wins!", Color.RGBColor(33, 0 , 127), "Gothic", 50, 630, 390);
            }else if(_bPieces == 1){
                SplashKit.FillRectangle(Color.Orange, 490, 315, 520, 220);
                SplashKit.FillRectangle(Color.Wheat, 500, 325, 500, 200);
                SplashKit.DrawText("White Wins!", Color.RGBColor(33, 0 , 127), "Gothic", 50, 620, 390);
            }
        }

        /// <summary>
        /// Visually displays how many times the player can use teleport or swap
        /// </summary>
        public void Skills(){
            SplashKit.DrawText("Teleport: " + _bTeleport, Color.RGBColor(33, 0 , 127), "Gothic", 20, 1200, 200);
            SplashKit.DrawText("Swaps: " + _bSwap, Color.RGBColor(33, 0 , 127), "Gothic", 20, 1370, 200);
            SplashKit.DrawText("Teleport: " + _wTeleport, Color.RGBColor(33, 0 , 127), "Gothic", 20, 1200, 600);
            SplashKit.DrawText("Swaps: " + _wSwap, Color.RGBColor(33, 0 , 127), "Gothic", 20, 1370, 600);
        }

        /// <summary>
        /// Visually displays all the rules
        /// </summary>
        public void Rules(){
            SplashKit.DrawText("Rules:", Color.RGBColor(33, 0 , 127), "Gothic", 25, 10, 60);
            SplashKit.DrawText("Same chess rules apply,", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 95);
            SplashKit.DrawText("except checkmate is not the", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 115);
            SplashKit.DrawText("aim to win. You get 10 lives", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 135);
            SplashKit.DrawText("each, and if you get checked,", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 155);
            SplashKit.DrawText("you lose a life. However if", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 175);
            SplashKit.DrawText("you put the other player in", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 195);
            SplashKit.DrawText("check, you get a life. You win", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 215);
            SplashKit.DrawText("by either getting the other", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 235);
            SplashKit.DrawText("player's lives to 0, or capture", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 255);
            SplashKit.DrawText("all of the opponents pieces first", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 275);

            SplashKit.DrawText("You each also get 2 power ups,", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 305);
            SplashKit.DrawText("Teleport allows you to move any", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 325);
            SplashKit.DrawText("piece to any free space on the", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 345);
            SplashKit.DrawText("board.", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 365);
            SplashKit.DrawText("Swap allows you to swap any of", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 385);
            SplashKit.DrawText("your piece's location with", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 405);
            SplashKit.DrawText("another's. However, you cannot", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 425);
            SplashKit.DrawText("swap a piece that is on the", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 445);
            SplashKit.DrawText("first or last row of the board.", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 465);
            SplashKit.DrawText("Use these wisely, as you only", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 485);
            SplashKit.DrawText("have 3 chances of each.", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 505);

            SplashKit.DrawText("To play, left click your piece", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 535);
            SplashKit.DrawText("and then right click to any", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 555);
            SplashKit.DrawText("green square to move it.", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 575);
            SplashKit.DrawText("To use your power ups, left", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 595);
            SplashKit.DrawText("click your piece and hit 'T'", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 615);
            SplashKit.DrawText("on an empty space to teleport", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 635);
            SplashKit.DrawText("or hit 'S' on a piece you wish", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 655);
            SplashKit.DrawText("to swap with.", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 675);

            SplashKit.DrawText("Press 'Reset' to restart the", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 705);
            SplashKit.DrawText("game or 'Chaos' to", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 725);
            SplashKit.DrawText("randomise start locations", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 745);
            SplashKit.DrawText("You may start with a", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 765);
            SplashKit.DrawText("disadvantage though", Color.RGBColor(33, 0 , 127), "Gothic", 15, 10, 785);
        }
    }
}
    
  

