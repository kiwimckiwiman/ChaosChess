using System;
using SplashKitSDK;

namespace CC
{   
    public enum PieceType{WPawn, BPawn, Rook, Bishop, Knight, Queen, KingW, KingB};
    public abstract class Piece{
        private int _x;
        private int _y;
        private bool _moving;
        private SColour _colour;
        private PieceType _pieceType;

        /// <summary>
        /// Parent class for all the pieces including the selector, initiated with an x and y coordinate
        /// </summary>
        public Piece(int x, int y){
            _x = x;
            _y = y;
            _moving = false;
        }

        /// <summary>
        /// Determines the piece type
        /// </summary>
        public PieceType Type{
            get{ return _pieceType; }
            set{ _pieceType = value; }
        }

        /// <summary>
        /// Takes in the x value and converts it into a pixel on the screen
        /// </summary>
        public int PixelX(){
            return ((_x * 100) + 350);
        }

        /// <summary>
        /// Takes in the y value and converts it into a pixel on the screen
        /// </summary>
        public int PixelY(){
            return ((_y * 100) + 25);
        }

        /// <summary>
        /// Sets an x coordinate, returns a pixel coordinate
        /// </summary>
        public int X{
            get{ return PixelX(); }
            set{ _x = value; }
        }

        /// <summary>
        /// Sets an y coordinate, returns a pixel coordinate
        /// </summary>
        public int Y{
            get{ return PixelY(); }
            set{ _y = value; }
        }

        /// <summary>
        /// Takes in the x and y coordinates to find the square its on in the list
        /// </summary>
        public int Coordinate(){
            return ((_y*8)+_x);
        }

        /// <summary>
        /// Determines if the piece is able to move or not
        /// </summary>
        public bool Moving{
            get{ return _moving; }
            set{ _moving = value; }
        }

        /// <summary>
        /// Sets the colour of the piece
        /// </summary>
        public SColour Colour{
            get{ return _colour; }
            set{ _colour = value; }
        }

        /// <summary>
        /// Toggles between can move and can't move
        /// </summary>
        public void isMove(){
            if(_moving){
                _moving = false;
            }else{
                _moving = true;
            }
        }   

        /// <summary>
        /// Visually draws the piece as a bitmap
        /// </summary>
        public virtual void Draw(){

        }

        
    }
}