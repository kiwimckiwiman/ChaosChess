using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CC
{
    [TestFixture()]
     public class MovementTest{
        [Test()]
        
        /// <summary>
        /// A pawn can only move 2 steps if it has not moved yet (it is on row 6)
        /// </summary>
        public void FirstMoveWhitePawn2MoveTest(){ 
            List<Piece> _pieces = new List<Piece>();
            PawnW PTest = new PawnW(3,6);

            _pieces.Add(PTest);

            List<Square> _squares = new List<Square>();

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

            Movement _moves = new Movement();

            bool result = _moves.PieceMovements(PTest.Coordinate(), PTest, 35, _squares, _pieces);
             
            Assert.AreEqual(result, true);
        }

        [Test()]
        
        /// <summary>
        /// This pawn is on row 5, hence it has moved and therefore cannot move 2 steps
        /// </summary>
        public void SecondMoveWhitePawn2MoveTest(){
            List<Piece> _pieces = new List<Piece>();
            PawnW PTest = new PawnW(3,5);

            _pieces.Add(PTest);

            List<Square> _squares = new List<Square>();

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

            Movement _moves = new Movement();

            bool result = _moves.PieceMovements(PTest.Coordinate(), PTest, 27, _squares, _pieces);
             
            Assert.AreEqual(result, false);
        }

        [Test()]

        /// <summary>
        /// This test is to make sure a bishop, a piece that moves diagonally, is unable to jump 
        /// over a friendly piece as the pawn is in the way of the bishop and its target
        /// square
        /// </summary>
        public void CheckBlockedFriend(){
            List<Piece> _pieces = new List<Piece>();
            PawnW PTest = new PawnW(3,5);
            BishopW BTest = new BishopW(5,3);

            _pieces.Add(PTest);
            _pieces.Add(BTest);

            List<Square> _squares = new List<Square>();

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
            _squares[PTest.Coordinate()].Occ = true;

            Movement _moves = new Movement();

            bool result = _moves.PieceMovements(BTest.Coordinate(), BTest, 50, _squares, _pieces);
             
            Assert.AreEqual(result, false);
        }

        [Test()]

        /// <summary>
        /// This test is to make sure a bishop, a piece that moves diagonally, is unable to jump 
        /// over a an enemy piece as the pawn is in the way of the bishop and its target
        /// square
        /// </summary>
        public void CheckBlockedEnemy(){
            List<Piece> _pieces = new List<Piece>();
            PawnB PTest = new PawnB(3,5);
            BishopW BTest = new BishopW(5,3);

            _pieces.Add(PTest);
            _pieces.Add(BTest);

            List<Square> _squares = new List<Square>();

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
            _squares[PTest.Coordinate()].Occ = true;

            Movement _moves = new Movement();

            bool result = _moves.PieceMovements(BTest.Coordinate(), BTest, 50, _squares, _pieces);
             
            Assert.AreEqual(result, false);
        }

        [Test()]

        /// <summary>
        /// This test makes sure the bishop is unable to move to the pawn's square
        /// as it is a friendly piece and so cannot be captured
        /// </summary>
        public void CheckFriendlyFire(){
            List<Piece> _pieces = new List<Piece>();
            PawnW PTest = new PawnW(3,5);
            BishopW BTest = new BishopW(5,3);

            _pieces.Add(PTest);
            _pieces.Add(BTest);

            List<Square> _squares = new List<Square>();

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
            _squares[PTest.Coordinate()].Occ = true;

            Movement _moves = new Movement();

            bool result = _moves.PieceMovements(BTest.Coordinate(), BTest, 43, _squares, _pieces);
             
            Assert.AreEqual(result, false);
        }

        [Test()]

        /// <summary>
        /// This time, the pawn is another colour hence an enemy's piece and 
        /// therefore can move to the spot to capture it
        /// </summary>
        public void CheckCapture(){
            List<Piece> _pieces = new List<Piece>();
            PawnB PTest = new PawnB(3,5);
            BishopW BTest = new BishopW(5,3);

            _pieces.Add(PTest);
            _pieces.Add(BTest);

            List<Square> _squares = new List<Square>();

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
            _squares[PTest.Coordinate()].Occ = true;

            Movement _moves = new Movement();

            bool result = _moves.PieceMovements(BTest.Coordinate(), BTest, 43, _squares, _pieces);
             
            Assert.AreEqual(result, true);
        }

        [Test()]

        /// <summary>
        /// This test shows that the Black King is under threat of a check from the White Bishop
        /// as the King's square is in path of an enemy piece
        /// </summary>
        public void CheckCheck(){
            List<Piece> _pieces = new List<Piece>();
            KingB KTest = new KingB(3,5);
            BishopW BTest = new BishopW(5,3);

            _pieces.Add(KTest);
            _pieces.Add(BTest);

            List<Square> _squares = new List<Square>();

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

            Movement _moves = new Movement();

            bool result = _moves.CheckPiecePaths(BTest.Coordinate(), BTest, KTest.Coordinate(), _squares, _pieces);
             
            Assert.AreEqual(result, true);
        }
     }
}