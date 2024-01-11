using System;
using SplashKitSDK;

namespace CC
{
    public class Square{
        
        private int _x;
        private int _y;
        private bool _occ;
        public SColour _colour;

        public int X{
            get { return _x; }
            set { _x = value; }
        }
        public int Y{
            get { return _y; }
            set { _y = value; }
        }

        public bool Occ{
            get { return _occ; }
            set { _occ = value; }
        }

        public SColour Colour{
            get{ return _colour; }
            set{ _colour = value; }
        }

        public int Coordinate(){
            return (_y*8)+_x;
        }
        public Square(SColour colour, int x, int y){
            _x = x;
            _y = y;
            _colour = colour;
            _occ = false;
        }

        public void SetOccupied(){
            if(_occ){
                _occ = false;
            }else{
                _occ = true;
            }
        }
    }
}