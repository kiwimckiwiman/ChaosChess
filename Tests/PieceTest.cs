using NUnit.Framework;
using System;

namespace CC
{
    [TestFixture()]
    public class PieceTest{
        [Test()]

        /// <summary>
        /// Tests if x coordinate to pixel X is correct
        /// </summary>
        public void PixelXTest(){
            PawnW PTest = new PawnW(5,4);

            Assert.AreEqual(PTest.PixelX(), 850);
        }

        [Test()]

        /// <summary>
        /// Tests if x coordinate to pixel X is correct
        /// </summary>
        public void PixelYTest(){
            PawnW PTest = new PawnW(5,4);

            Assert.AreEqual(PTest.PixelY(), 425);
        }

        [Test()]
        /// <summary>
        /// Tests if conversion of coordinate to list number is correct
        /// </summary>
        public void CoordinateTest(){
            PawnW PTest = new PawnW(5,4);

            Assert.AreEqual(PTest.Coordinate(), 37);
        }
    }
}