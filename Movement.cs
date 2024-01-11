using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace CC
{
    public class Movement{
        private bool _blocked;
        private List<int> _wBadSqaures;
        private List<int> _bBadSqaures;
        
        public Movement(){
            _blocked = false;
            _wBadSqaures = new List<int>();
            _bBadSqaures = new List<int>();
            
        }

        /// <summary>
        /// Finds the piece in a list of pieces via the coordinate
        /// </summary>
        public int FindPiece(List<Piece> _pieces, int _targetC){
            int index = _pieces.FindIndex(s => s.Coordinate() == _targetC);
            return index;
        }

        /// <summary>
        /// Checks if the piece in a path of the player is one of theirs or the other's
        /// </summary>
        public bool FriendlyFire(List<Square> _squares, List<Piece> _pieces, int _targetC, int _currentC){
            if(!_squares[_targetC].Occ){
                return true;
            }else if((_squares[_targetC].Occ) && !(_pieces[FindPiece(_pieces, _targetC)].Colour == _pieces[FindPiece(_pieces, _currentC)].Colour)){
                return true;
            }else{
                return false;
            }
        }

        /// <summary>
        /// Visually displays the possible moves a piece can make on the board
        /// </summary>
        public void PossibleMoves(int currentC, Piece p, List<Square> _squares, List<Piece> _pieces){
            for (int i = 0; i < 64; i++){
                if(PieceMovements(currentC, p, i, _squares, _pieces)){
                    Possible possible = new Possible(_squares[i].X, _squares[i].Y);
                    _pieces.Add(possible);
                }
            }
        }

        /// <summary>
        /// Visually displays which player is in check
        /// </summary>
        public void Check(List<Piece> _pieces, List<Square> _squares){
            
            int wK = _pieces.FindIndex(s => s.Type == PieceType.KingW);
            int bK = _pieces.FindIndex(s => s.Type == PieceType.KingB);

            foreach(Piece p in _pieces){
                if((p.Colour == SColour.w) && !(p.Type == PieceType.KingW)){
                    if(CheckPiecePaths(p.Coordinate(), p, _pieces[bK].Coordinate(), _squares, _pieces)){
                        SplashKit.DrawText("Black is Checked", Color.RGBColor(33, 0 , 127), "Gothic", 25, 1223, 150);
                    }
                }else if((p.Colour == SColour.b) && !(p.Type == PieceType.KingB)){
                    if(CheckPiecePaths(p.Coordinate(), p, _pieces[wK].Coordinate(), _squares, _pieces)){
                        SplashKit.DrawText("White is Checked", Color.RGBColor(33, 0 , 127), "Gothic", 25, 1226, 660);
                    }
                }
            }
        }

        /// <summary>
        /// Gives and takes a point from each player if a check is made
        /// </summary>
        public void CheckPoints(List<Piece> _pieces, List<Square> _squares, Points _points){
            Game _game = new Game();
            Random _rng = new Random();
            int wK = _pieces.FindIndex(s => s.Type == PieceType.KingW);
            int bK = _pieces.FindIndex(s => s.Type == PieceType.KingB);

            foreach(Piece p in _pieces){
                if((p.Colour == SColour.w) && !(p.Type == PieceType.KingW)){
                    if(CheckPiecePaths(p.Coordinate(), p, _pieces[bK].Coordinate(), _squares, _pieces)){
                        _points.BlackCheck();
                        //_game.Sounds(_rng.Next(7, 13));
                    }
                }else if((p.Colour == SColour.b) && !(p.Type == PieceType.KingB)){
                    if(CheckPiecePaths(p.Coordinate(), p, _pieces[wK].Coordinate(), _squares, _pieces)){
                       _points.WhiteCheck();
                       //_game.Sounds(_rng.Next(7, 10));
                    }
                }
            }
        }

        /// <summary>
        /// Checks to see if a piece is between the current and target square a piece wants to move to
        /// </summary>
        public void CheckBlocked(int _currentC, List<Square> _squares, int _targetC){
            int spaces = _currentC - _targetC;
            if(((spaces) > 0) && (((spaces)%8) == 0)){
                //Checks if there are no occupied _pieces between target and current square if the jump up is more than 1 space
                for (int i = 1; i < spaces/8; i++){                    
                    if(_squares[_currentC - (i*8)].Occ){
                        _blocked = true;
                    }
                }
            }else if(((spaces) < 0) && (((spaces)%8) == 0)){
                //Checks if there are no occupied _pieces between target and current square if the jump down is more than 1 space
                for (int i = 1; i < Math.Abs(spaces/8); i++){                   
                    if(_squares[_currentC + (i*8)].Occ){
                        _blocked = true;
                    }
                }
            }else if(((spaces) < 0) && (_targetC < (Convert.ToInt32(Math.Ceiling((_currentC + 1)/8.0))*8))){
                //Checks if there are no occupied _pieces between target and current square if the jump right is more than 1 space
                for (int i = 1; i < (_targetC - _currentC); i++){
                    if(_squares[_currentC + i].Occ){
                        _blocked = true;
                    }
                }
            }else if(((spaces) > 0) && (_targetC > (Convert.ToInt32(Math.Floor(_currentC/8.0))*8)-1)){
                //Checks if there are no occupied _pieces between target and current square if the jump left is more than 1 space
                for (int i = 1; i < (_currentC - _targetC); i++){                   
                    if(_squares[_currentC - i].Occ){
                        _blocked = true;
                    }
                }
            }else if((spaces < 0) && ((spaces)%7 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour)){
                //Checks if there are no occupied _pieces between target and current square if the jump down and left is more than 1 
                for (int i = 1; i < Math.Abs(spaces/7); i++){                   
                    if(_squares[_currentC + (i*7)].Occ){
                        _blocked = true;
                    }
                }   
            }else if((spaces > 0) && ((spaces)%7 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour)){
                //Checks if there are no occupied _pieces between target and current square if the jump up and right is more than 1 
                for (int i = 1; i < spaces/7; i++){                   
                    if(_squares[_currentC - (i*7)].Occ){
                        _blocked = true;
                    }
                }  
            }else if((spaces < 0) && ((spaces)%9 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour)){
                //Checks if there are no occupied _pieces between target and current square if the jump down and right is more than 1 
                for (int i = 1; i < Math.Abs(spaces/9); i++){                   
                    if(_squares[_currentC + (i*9)].Occ){
                        _blocked = true;
                    }
                }    
            }else if((spaces > 0) && ((spaces)%9 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour)){
                //Checks if there are no occupied _pieces between target and current square if the jump up and left is more than 1 
                for (int i = 1; i < spaces/9; i++){                   
                    if(_squares[_currentC - (i*9)].Occ){
                        _blocked = true;
                    }
                }   
            }
        }

        /// <summary>
        /// Shows all the possible moves a piece can make within the rules of the game
        /// </summary>
        public bool PieceMovements(int _currentC, Piece _piece, int _targetC, List<Square> _squares, List<Piece> _pieces){
            if(_blocked){
                _blocked = false;
            }
            //2 sets of movements are made for either pawns since the boards direction doesnt change, so for a pawn on the top side to "move forward" it goes down the board
            //and vice versa
            if(_piece.Type == PieceType.WPawn){ //Pawns move only 1 step towards the enemy. They can move 1 or 2 steps as their first move, and capture _pieces diagonally
                if((Math.Floor(_currentC/8.0) == 6) && ((_currentC - _targetC == 16) && !_squares[_currentC - 8].Occ) && !_squares[_currentC - 16].Occ){
                    //If its the first move, Pawn wants to move 2 steps up given the square between target and starting is unoccupied
                    return true;
                }else if((_currentC - _targetC == 8) && !_squares[_targetC].Occ){
                    //Pawn is trying to move up 1 given the target square is unoccupied
                    return true;
                }else if((_currentC - _targetC == 9) && _squares[_targetC].Occ && !(_pieces[FindPiece(_pieces, _targetC)].Colour == _pieces[FindPiece(_pieces, _currentC)].Colour) && ((Math.Floor(_currentC/8.0) - Math.Floor(_targetC/8.0) == 1))){
                    //If there is a piece in front of the pawn diagonally to the left, it may move to capture that piece
                    return true;
                }else if((_currentC - _targetC == 7) && _squares[_targetC].Occ && !(_pieces[FindPiece(_pieces, _targetC)].Colour == _pieces[FindPiece(_pieces, _currentC)].Colour) && ((Math.Floor(_currentC/8.0) - Math.Floor(_targetC/8.0)) == 1)){
                    //If there is a piece in front of the pawn diagonally to the right, it may move to capture that piece
                    return true;
                }else{
                    return false;
                }

            }else if(_piece.Type == PieceType.BPawn){ //Pawns move only 1 step towards the enemy. They can move 1 or 2 steps as their first move, and capture _pieces diagonally
                if((Math.Floor(_currentC/8.0) == 1) && ((_targetC - _currentC == 16) && !_squares[_currentC + 8].Occ) && !_squares[_currentC + 16].Occ){
                    //If its the first move, Pawn wants to move 2 steps up given the square between target and starting is unoccupied
                    return true;
                }else if((_targetC - _currentC == 8) && !_squares[_targetC].Occ){
                    //Pawn is trying to move up 1 given the target square is unoccupied
                    return true;
                }else if((_targetC - _currentC == 9) && _squares[_targetC].Occ && !(_pieces[FindPiece(_pieces, _targetC)].Colour == _pieces[FindPiece(_pieces, _currentC)].Colour) && ((Math.Floor(_currentC/8.0) - Math.Floor(_targetC/8.0)) == -1)){
                    //If there is a piece in front of the pawn diagonally to the left, it may move to capture that piece
                    return true;
                }else if((_targetC - _currentC == 7) && _squares[_targetC].Occ && !(_pieces[FindPiece(_pieces, _targetC)].Colour == _pieces[FindPiece(_pieces, _currentC)].Colour) && ((Math.Floor(_currentC/8.0) - Math.Floor(_targetC/8.0)) == -1)){
                    //If there is a piece in front of the pawn diagonally to the right, it may move to capture that piece
                    return true;
                }else{
                    return false;
                }

            }else if(_piece.Type == PieceType.Knight){ //Knights move in an L shape, to 8 possible locations around it. They can go over _pieces
                if((_targetC - _currentC == 17) && !(_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Knight is trying to move up 2 and to the left 1
                    return true;
                }else if((_targetC - _currentC == 15) && !(_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Knight is trying to move up 2 and to the right 1
                    return true;
                }else if((_currentC - _targetC == 17) && !(_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Knight is trying to move down 2 and to the right 1
                    return true;
                }else if((_currentC - _targetC == 15) && !(_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Knight is trying to move down 2 and to the left 1
                    return true;
                }else if((_currentC - _targetC == 10) && !(_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Knight is trying to the right 2 and down 1
                    return true;
                }else if((_currentC - _targetC == 6) && !(_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Knight is trying to the right 2 and up 1
                    return true;
                }else if((_targetC - _currentC == 6) && !(_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Knight is trying to the left 2 and down 1
                    return true;
                }else if((_targetC - _currentC == 10) && !(_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Knight is trying to the left 2 and up 1
                    return true;
                }else{
                    return false;
                }

            }else if(_piece.Type == PieceType.KingW){ //Kings can only move 1 step in any direction
                
                if((_currentC - _targetC == 9) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //King is trying to move 1 step diagonally up and to the left
                    return true;
                }else if((_currentC - _targetC == 8) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //King is trying to move 1 step up
                    return true;
                }else if((_currentC - _targetC == 7) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //King is trying to move 1 step diagonally up and to the right
                    return true;           
                }else if((_currentC - _targetC == 1) && (Math.Floor(_currentC/8.0) == Math.Floor(_targetC/8.0)) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //King is trying to move 1 step to the left
                    return true;
                }else if((_targetC - _currentC == 1) && (Math.Floor(_currentC/8.0) == Math.Floor(_targetC/8.0)) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //King is trying to move 1 step to the right
                    return true;
                }else if((_targetC - _currentC == 7) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //King is trying to move 1 step diagonally down and to the left
                    return true;
                }else if((_targetC - _currentC == 8) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //King is trying to move 1 step down
                    return true;
                }else if((_targetC - _currentC == 9) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //King is trying to move 1 step diagonally down and to the right
                    return true;
                }else{
                    return false;
                }

            }else if(_piece.Type == PieceType.KingB){ //Kings can only move 1 step in any direction
                
                if((_currentC - _targetC == 9) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //King is trying to move 1 step diagonally up and to the left
                    return true;
                }else if((_currentC - _targetC == 8) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //King is trying to move 1 step up
                    return true;
                }else if((_currentC - _targetC == 7) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //King is trying to move 1 step diagonally up and to the right
                    return true;           
                }else if((_currentC - _targetC == 1) && (Math.Floor(_currentC/8.0) == Math.Floor(_targetC/8.0)) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //King is trying to move 1 step to the left
                    return true;
                }else if((_targetC - _currentC == 1) && (Math.Floor(_currentC/8.0) == Math.Floor(_targetC/8.0)) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //King is trying to move 1 step to the right
                    return true;
                }else if((_targetC - _currentC == 7) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //King is trying to move 1 step diagonally down and to the left
                    return true;
                }else if((_targetC - _currentC == 8) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //King is trying to move 1 step down
                    return true;
                }else if((_targetC - _currentC == 9) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //King is trying to move 1 step diagonally down and to the right
                    return true;
                }else{
                    return false;
                }

            }else if(_piece.Type == PieceType.Rook){
                int spaces = _currentC - _targetC;
                CheckBlocked(_currentC, _squares, _targetC);
                if(((spaces) > 0) && (((spaces)%8) == 0) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Rook is trying to move up
                    return true;
                }else if(((spaces) < 0) && (((spaces)%8) == 0) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Rook is trying to move down
                    return true;
                }else if(((spaces) < 0) && (_targetC < (Convert.ToInt32(Math.Ceiling((_currentC + 1)/8.0))*8)) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Rook is trying to the to the right
                    return true;
                }else if(((spaces) > 0) && (_targetC > (Convert.ToInt32(Math.Floor(_currentC/8.0))*8)-1) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Rook is trying to the to the left
                    return true;
                }else{
                    return false;
                }

            }else if(_piece.Type == PieceType.Bishop){
                CheckBlocked(_currentC, _squares, _targetC);
                int spaces = _currentC - _targetC;
                if((spaces < 0) && ((spaces)%7 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Bishop is trying to diagonally down then left
                    return true;
                }else if((spaces > 0) && ((spaces)%7 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Bishop is trying to diagonally up then right
                    return true;
                }else if((spaces < 0) && ((spaces)%9 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Bishop is trying to diagonally down then right
                    return true;
                }else if((spaces > 0) && ((spaces)%9 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Bishop is trying to diagonally up then left
                    return true;
                }else{
                    return false;
                }

            }else if(_piece.Type == PieceType.Queen){
                CheckBlocked(_currentC, _squares, _targetC);
                int spaces = _currentC - _targetC;
                if(((spaces) < 0) && (((spaces)%8) == 0) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Queen is trying to move up
                    return true;
                }else if(((spaces) > 0) && (((spaces)%8) == 0) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Queen is trying to move down
                    return true;
                }else if(((spaces) < 0) && (_targetC < (Convert.ToInt32(Math.Ceiling((_currentC + 1)/8.0))*8)) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Queen is trying to move to the right
                    return true;
                }else if(((spaces) > 0) && (_targetC > (Convert.ToInt32(Math.Floor(_currentC/8.0))*8)-1) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Queen is trying to the to the left
                    return true;
                }else if((spaces < 0) && ((spaces)%7 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Queen is trying to diagonally down then left
                    return true;
                }else if((spaces > 0) && ((spaces)%7 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Queen is trying to diagonally up then right
                    return true;
                }else if((spaces < 0) && ((spaces)%9 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Queen is trying to diagonally down then right
                    return true;
                }else if((spaces > 0) && ((spaces)%9 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                    //Queen is trying to diagonally up then left
                    return true;
                }else{
                    return false;
                }

            }else{
                return false;
            } 
        }

        /// <summary>
        /// Shows all possible kill moves a piece can make. Used for checking for checks if the target square is the King
        /// </summary>
        public bool CheckPiecePaths(int _currentC, Piece _piece, int _targetC, List<Square> _squares, List<Piece> _pieces){
            if(_blocked){
                _blocked = false;
            }
            if((_targetC < 64) && _targetC > 0){
                //2 sets of movements are made for either pawns since the boards direction doesnt change, so for a pawn on the top side to "move forward" it goes down the board
                //and vice versa
                if(_piece.Type == PieceType.WPawn){ //Pawns move only 1 step towards the enemy. They can move 1 or 2 steps as their first move, and capture _pieces diagonally
                    if((_currentC - _targetC == 9) && _squares[_targetC].Occ && !(_pieces[FindPiece(_pieces, _targetC)].Colour == _pieces[FindPiece(_pieces, _currentC)].Colour) && ((Math.Floor(_currentC/8.0) - Math.Floor(_targetC/8.0) == 1))){
                        //If there is a piece in front of the pawn diagonally to the left, it may move to capture that piece
                        return true;
                    }else if((_currentC - _targetC == 7) && _squares[_targetC].Occ && !(_pieces[FindPiece(_pieces, _targetC)].Colour == _pieces[FindPiece(_pieces, _currentC)].Colour) && ((Math.Floor(_currentC/8.0) - Math.Floor(_targetC/8.0)) == 1)){
                        //If there is a piece in front of the pawn diagonally to the right, it may move to capture that piece
                        return true;
                    }else{
                        return false;
                    }

                }else if(_piece.Type == PieceType.BPawn){ //Pawns move only 1 step towards the enemy. They can move 1 or 2 steps as their first move, and capture _pieces diagonally
                    if((_targetC - _currentC == 9) && _squares[_targetC].Occ && !(_pieces[FindPiece(_pieces, _targetC)].Colour == _pieces[FindPiece(_pieces, _currentC)].Colour) && ((Math.Floor(_currentC/8.0) - Math.Floor(_targetC/8.0)) == -1)){
                        //If there is a piece in front of the pawn diagonally to the left, it may move to capture that piece
                        return true;
                    }else if((_targetC - _currentC == 7) && _squares[_targetC].Occ && !(_pieces[FindPiece(_pieces, _targetC)].Colour == _pieces[FindPiece(_pieces, _currentC)].Colour) && ((Math.Floor(_currentC/8.0) - Math.Floor(_targetC/8.0)) == -1)){
                        //If there is a piece in front of the pawn diagonally to the right, it may move to capture that piece
                        return true;
                    }else{
                        return false;
                    }

                }else if(_piece.Type == PieceType.Knight){ //Knights move in an L shape, to 8 possible locations around it. They can go over _pieces
                    if((_targetC - _currentC == 17) && !(_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Knight is trying to move up 2 and to the left 1
                        return true;
                    }else if((_targetC - _currentC == 15) && !(_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Knight is trying to move up 2 and to the right 1
                        return true;
                    }else if((_currentC - _targetC == 17) && !(_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Knight is trying to move down 2 and to the right 1
                        return true;
                    }else if((_currentC - _targetC == 15) && !(_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Knight is trying to move down 2 and to the left 1
                        return true;
                    }else if((_currentC - _targetC == 10) && !(_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Knight is trying to the right 2 and down 1
                        return true;
                    }else if((_currentC - _targetC == 6) && !(_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Knight is trying to the right 2 and up 1
                        return true;
                    }else if((_targetC - _currentC == 6) && !(_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Knight is trying to the left 2 and down 1
                        return true;
                    }else if((_targetC - _currentC == 10) && !(_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Knight is trying to the left 2 and up 1
                        return true;
                    }else{
                        return false;
                    }

                }else if(_piece.Type == PieceType.KingW || _piece.Type == PieceType.KingB){ //Kings can only move 1 step in any direction
                    
                    if((_currentC - _targetC == 9) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //King is trying to move 1 step diagonally up and to the left
                        return true;
                    }else if((_currentC - _targetC == 8) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //King is trying to move 1 step up
                        return true;
                    }else if((_currentC - _targetC == 7) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //King is trying to move 1 step diagonally up and to the right
                        return true;            
                    }else if((_currentC - _targetC == 1) && (Math.Floor(_currentC/8.0) == Math.Floor(_targetC/8.0)) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //King is trying to move 1 step to the left
                        return true;
                    }else if((_targetC - _currentC == 1) && (Math.Floor(_currentC/8.0) == Math.Floor(_targetC/8.0)) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //King is trying to move 1 step to the right
                        return true;
                    }else if((_targetC - _currentC == 7) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //King is trying to move 1 step diagonally down and to the left
                        return true;
                    }else if((_targetC - _currentC == 8) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //King is trying to move 1 step down
                        return true;
                    }else if((_targetC - _currentC == 9) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //King is trying to move 1 step diagonally down and to the right
                        return true;
                    }else{
                        return false;
                    }

                }else if(_piece.Type == PieceType.Rook){
                    int spaces = _currentC - _targetC;
                    CheckBlocked(_currentC, _squares, _targetC);
                    if(((spaces) > 0) && (((spaces)%8) == 0) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Rook is trying to move up
                        return true;
                    }else if(((spaces) < 0) && (((spaces)%8) == 0) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Rook is trying to move down
                        return true;
                    }else if(((spaces) < 0) && (_targetC < (Convert.ToInt32(Math.Ceiling((_currentC + 1)/8.0))*8)) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Rook is trying to the to the right
                        return true;
                    }else if(((spaces) > 0) && (_targetC > (Convert.ToInt32(Math.Floor(_currentC/8.0))*8)-1) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Rook is trying to the to the left
                        return true;
                    }else{
                        return false;
                    }

                }else if(_piece.Type == PieceType.Bishop){
                    CheckBlocked(_currentC, _squares, _targetC);
                    int spaces = _currentC - _targetC;
                    if((spaces < 0) && ((spaces)%7 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Bishop is trying to diagonally down then left
                        return true;
                    }else if((spaces > 0) && ((spaces)%7 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Bishop is trying to diagonally up then right
                        return true;
                    }else if((spaces < 0) && ((spaces)%9 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Bishop is trying to diagonally down then right
                        return true;
                    }else if((spaces > 0) && ((spaces)%9 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Bishop is trying to diagonally up then left
                        return true;
                    }else{
                        return false;
                    }

                }else if(_piece.Type == PieceType.Queen){
                    CheckBlocked(_currentC, _squares, _targetC);
                    int spaces = _currentC - _targetC;
                    if(((spaces) < 0) && (((spaces)%8) == 0) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Queen is trying to move up
                        return true;
                    }else if(((spaces) > 0) && (((spaces)%8) == 0) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Queen is trying to move down
                        return true;
                    }else if(((spaces) < 0) && (_targetC < (Convert.ToInt32(Math.Ceiling((_currentC + 1)/8.0))*8)) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Queen is trying to move to the right
                        return true;
                    }else if(((spaces) > 0) && (_targetC > (Convert.ToInt32(Math.Floor(_currentC/8.0))*8)-1) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Queen is trying to the to the left
                        return true;
                    }else if((spaces < 0) && ((spaces)%7 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Queen is trying to diagonally down then left
                        return true;
                    }else if((spaces > 0) && ((spaces)%7 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Queen is trying to diagonally up then right
                        return true;
                    }else if((spaces < 0) && ((spaces)%9 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Queen is trying to diagonally down then right
                        return true;
                    }else if((spaces > 0) && ((spaces)%9 == 0) && (_squares[_currentC].Colour == _squares[_targetC].Colour) && !_blocked && FriendlyFire(_squares, _pieces, _targetC, _currentC)){
                        //Queen is trying to diagonally up then left
                        return true;
                    }else{
                        return false;
                    }

                }else{
                    return false;
                } 
            }else{
                return false;
            }
        }     
    }
}