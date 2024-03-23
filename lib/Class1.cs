﻿using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace lib;

public class CellLogic
{
    // EXAMPLE STATE
    // _ _ _
    // _ _ X
    // _ X X
    readonly Random Random = new();
    private Dictionary<int[], Life> _cellBoardFrameOne = new();
    private Dictionary<int[], Life> _cellBoardFrameTwo = new();
    private readonly int _boardWidth;
    private int _numOflivingCells;
    private List<int[]> _livingCellsList = new();

    public CellLogic(int sideLength = 3, int numOflivingCells = 3) {
        // Initialize the CellLogic class in a good starting state
        _boardWidth = sideLength;
        _numOflivingCells = numOflivingCells;
        randomizedUniqueCoords(_livingCellsList);
        populateCellBoard(_cellBoardFrameOne);
        populateCellBoard(_cellBoardFrameTwo);
    }

    private void populateCellBoard(Dictionary<int[], Life> cellBoard) {
        //                                     {i} = index          [x, y] = cell coordinates
        // living cell locaations example: { --> {0} [0,1] , --> {1} [1,1] , --> {2} [2,2] }
        for (int i = 0; i < _boardWidth; i++)
        {   
            for (int j = 0; j < _boardWidth; j++) {
                    cellBoard.Add([i,j],Life.Dead);
            }
        }
        for (int i = 0; i < _livingCellsList.Count; i++)
        {
            cellBoard[_livingCellsList[i]] = Life.Alive; // cellBoard[key: _livingCellsList[ {i} --> [0,1] ] ] = Life.Alive;
        }
    }
    private void randomizedUniqueCoords(List<int[]> livingCellsList)
    {
        // Using the empty list of int arrays, create random coordinates X based on the int livingCells
        // newCell is the current randomized coord, if it is already in the list of coordinates skip
        // if newCell is not in the coordinates list, add it
        // keep doing this until the total number of coordinates in the list is less than or equal to the total of living cells
        int[] newCell = coordinateRandomizer(_boardWidth);
        for (int i = 0; i < _numOflivingCells; i++)
        {
            do
            {
                if (!livingCellsList.Contains(newCell))
                    livingCellsList.Add(newCell);
                else
                    newCell = coordinateRandomizer(_boardWidth);

            } while (livingCellsList.Contains(newCell));
        }
    }
    public void displayBoard() {
        // displays the dictionary frame board using simple logic
        // if dead, print 0
        // if alive, print 1
        // then it has an iterator to seperate them by rows in the console
        Console.Clear();
        int i = 1;
        foreach (var value in _cellBoardFrameOne)
        {
            char printChar = value.Value == Life.Dead ? '0' : '1';
            if (i % _boardWidth == 0)
                Console.WriteLine(printChar);
            else
                Console.Write(printChar);
            i++;
        }    
    }
    private int[] coordinateRandomizer(int sideLength) => [Random.Shared.Next(0,_boardWidth),Random.Shared.Next(0,_boardWidth)]; 
    // returns a random coordinate within the bounds of the grid, defined by sideLength
    enum Life {Alive, Dead};
}