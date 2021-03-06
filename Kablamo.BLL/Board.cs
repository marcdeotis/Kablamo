﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kablamo.BLL
{
    public class Board
    {
        private Dictionary<Coordinate, SquareStatus> _boardState;
        private static List<Coordinate> _vectors = new List<Coordinate>() {
            new Coordinate(1, 0),
            new Coordinate(1, 1),
            new Coordinate(0, 1),
            new Coordinate(-1, 1),
            new Coordinate(-1, 0),
            new Coordinate(-1, -1),
            new Coordinate(0, -1),
            new Coordinate(1, -1), };
        private List<Coordinate> _moves;
        private bool _isBlueTurn = true;
        public Dictionary<Coordinate, SquareStatus> BoardState { get { return _boardState; } }
        //todo: win condition bools?

        public Board()
        {
            InitalizeBoard();
        }

        public Board MakeMove(Coordinate coord)
        {
            //todo: check for win state don't continue
            if (IsOnBoard(coord))
            {
                _moves = _boardState.Where(x => x.Value == (_isBlueTurn ? SquareStatus.DoubleBlue : SquareStatus.DoubleRed)).Select(y => y.Key).ToList();
                if (_moves.Any(x => (x.XCoordinate == coord.XCoordinate) && (x.YCoordinate == coord.YCoordinate)))
                {
                    _boardState[coord] = SquareStatus.Empty;
                    SinglesToDoubles();
                    ChangeSquares(coord);
                }
            }

            return this;
        }
        
        private void ChangeSquares(Coordinate coord)
        {
            bool changing = true;
            foreach (var coordinate in _vectors)
            {
                Coordinate temp = new Coordinate(coord.XCoordinate, coord.YCoordinate);
                do
                {
                    temp += coordinate;
                    if (!IsOnBoard(temp))
                    {
                        break;
                    }
                    if (_isBlueTurn)
                    {
                        switch (_boardState[temp].ToString())
                        {
                            case "Wall":
                            case "SingleBlue":
                            case "BlueBase":
                            case "DoubleBlue":
                                changing = false;
                                break;
                            case "SingleRed":
                            case "DoubleRed":
                                _boardState[temp] = SquareStatus.Empty;
                                changing = false;
                                break;
                            case "RedBase":
                                //todo: RedBase Victory condition
                                break;
                            default:
                                _boardState[temp] = SquareStatus.SingleBlue;
                                break;
                        }
                    }
                    else
                    {
                        switch (_boardState[temp].ToString())
                        {
                            case "Wall":
                            case "SingleRed":
                            case "RedBase":
                            case "DoubleRed":
                                changing = false;
                                break;
                            case "SingleBlue":
                            case "DoubleBlue":
                                _boardState[temp] = SquareStatus.Empty;
                                changing = false;
                                break;
                            case "BlueBase":
                                //todo: RedBase Victory condition
                                break;
                            default:
                                _boardState[temp] = SquareStatus.SingleRed;
                                break;
                        }
                    }
                } while (changing);
                changing = true;
            }
            _isBlueTurn = !_isBlueTurn;
        }

        private void SinglesToDoubles()
        {
            List<Coordinate> query = _boardState.Where(x => x.Value == (_isBlueTurn ? SquareStatus.SingleBlue : SquareStatus.SingleRed)).Select(y => y.Key).ToList();

            foreach(var coordinate in query)
            {
                if(_isBlueTurn)
                {
                    _boardState[coordinate] = SquareStatus.DoubleBlue;
                }
                else
                {
                    _boardState[coordinate] = SquareStatus.DoubleRed;
                }
            }
  
        }

        private bool IsOnBoard(Coordinate coord)
        {
            return coord.XCoordinate >= 0
                && coord.YCoordinate >= 0
                && coord.XCoordinate <= 9
                && coord.YCoordinate <= 9;
        }

        private void InitalizeBoard()
        {
            _boardState = new Dictionary<Coordinate, SquareStatus>();
            for (int i = 0; i < 10; i++)
            {
                for (int k = 0; k < 10; k++)
                {
                    _boardState.Add(new Coordinate(i, k), SquareStatus.Empty);
                }
            }

            Coordinate blueBase = new Coordinate(0, 0);
            Coordinate redBase = new Coordinate(9, 9);
            Coordinate wallUpperRight1 = new Coordinate(0, 8);
            Coordinate wallUpperRight2 = new Coordinate(0, 9);
            Coordinate wallUpperRight3 = new Coordinate(1, 8);
            Coordinate wallUpperRight4 = new Coordinate(1, 9);
            Coordinate middleWall1 = new Coordinate(4, 4);
            Coordinate middleWall2 = new Coordinate(4, 5);
            Coordinate middleWall3 = new Coordinate(5, 4);
            Coordinate middleWall4 = new Coordinate(5, 5);
            Coordinate wallLowerLeft1 = new Coordinate(8, 0);
            Coordinate wallLowerLeft2 = new Coordinate(8, 1);
            Coordinate wallLowerLeft3 = new Coordinate(9, 0);
            Coordinate wallLowerLeft4 = new Coordinate(9, 1);
            Coordinate bluePlayable1 = new Coordinate(0, 1);
            Coordinate bluePlayable2 = new Coordinate(1, 0);
            Coordinate redPlayable1 = new Coordinate(9, 8);
            Coordinate redPlayable2 = new Coordinate(8, 9);
            Coordinate blueSingle = new Coordinate(1, 1);
            Coordinate redSingle = new Coordinate(8, 8);

            //Bases
            _boardState[blueBase] = SquareStatus.BlueBase;
            _boardState[redBase] = SquareStatus.RedBase;

            //Upper Right Wall
            _boardState[wallUpperRight1] = SquareStatus.Wall;
            _boardState[wallUpperRight2] = SquareStatus.Wall;
            _boardState[wallUpperRight3] = SquareStatus.Wall;
            _boardState[wallUpperRight4] = SquareStatus.Wall;

            //Middle Wall
            _boardState[middleWall1] = SquareStatus.Wall;
            _boardState[middleWall2] = SquareStatus.Wall;
            _boardState[middleWall3] = SquareStatus.Wall;
            _boardState[middleWall4] = SquareStatus.Wall;

            //Lower Left Wall
            _boardState[wallLowerLeft1] = SquareStatus.Wall;
            _boardState[wallLowerLeft2] = SquareStatus.Wall;
            _boardState[wallLowerLeft3] = SquareStatus.Wall;
            _boardState[wallLowerLeft4] = SquareStatus.Wall;

            //Blue Playable Pieces
            _boardState[bluePlayable1] = SquareStatus.DoubleBlue;
            _boardState[bluePlayable2] = SquareStatus.DoubleBlue;

            //Red Playable Pieces
            _boardState[redPlayable1] = SquareStatus.DoubleRed;
            _boardState[redPlayable2] = SquareStatus.DoubleRed;

            //Blue Single Piece
            _boardState[blueSingle] = SquareStatus.SingleBlue;

            //Red Single Piece
            _boardState[redSingle] = SquareStatus.SingleRed;
        }
    }
}
