﻿using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
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
    private Dictionary<(int x, int y), Life> _cellBoardFrameOne = new();
    private Dictionary<(int x, int y), Life> _cellBoardFrameTwo = new();
    private readonly int _boardWidth;
    private int _numOflivingCells;
    private List<(int x, int y)> _livingCellsList = new();

    public CellLogic(int sideLength = 3, int numOflivingCells = 3) {
        // Initialize the CellLogic class in a good starting state
        _boardWidth = sideLength;
        _numOflivingCells = numOflivingCells;
        randomizedUniqueCoords(_livingCellsList);
        populateCellBoard(_cellBoardFrameOne);
        populateCellBoard(_cellBoardFrameTwo);
    }
    public void gameLoop() {
        displayBoard(_cellBoardFrameOne);
        Console.ReadKey(true);
        while (true)
        {
            displayBoard(_cellBoardFrameOne);
            evolveFrame();
            setVisibleFrame();
            Thread.Sleep(100);
            displayBoard(_cellBoardFrameOne);
        }
    }
    private void populateCellBoard(Dictionary<(int x, int y), Life> cellBoard) {
        //                                     {i} = index          [x, y] = cell coordinates
        // living cell locaations example: { --> {0} [0,1] , --> {1} [1,1] , --> {2} [2,2] }
        for (int i = 0; i < _boardWidth; i++)
        {   
            for (int j = 0; j < _boardWidth; j++) {
                    cellBoard.Add((i,j),Life.Dead);

            }
        }
        for (int i = 0; i < _livingCellsList.Count; i++)
        {
            for (int j = 0; j < _boardWidth; j ++)
                for (int k = 0; k < _boardWidth; k++)
                {
                    if (_livingCellsList.ElementAt(i) == (j, k))
                        cellBoard[(j, k)] = Life.Alive;
                }
            // cellBoard[_livingCellsList[i]] = Life.Alive; // cellBoard[key: _livingCellsList[ {i} --> [0,1] ] ] = Life.Alive;
        }
    }
    private void setVisibleFrame() {
        for (int i = 0; i < _boardWidth; i++)
            for (int j = 0; j < _boardWidth; j++)
                _cellBoardFrameOne[(i,j)] = _cellBoardFrameTwo[(i,j)];
    }
    private void randomizedUniqueCoords(List<(int x, int y)> livingCellsList)
    {
        // Using the empty list of int arrays, create random coordinates X based on the int livingCells
        // newCell is the current randomized coord, if it is already in the list of coordinates skip
        // if newCell is not in the coordinates list, add it
        // keep doing this until the total number of coordinates in the list is less than or equal to the total of living cells
        (int, int) newCell = coordinateRandomizer(_boardWidth);
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
    private void displayBoard(Dictionary<(int x, int y), Life> frame) {
        // displays the dictionary frame board using simple logic
        // if dead, print 0
        // if alive, print 1
        // then it has an iterator to seperate them by rows in the console
        Console.Clear();
        int i = 1;
        foreach (var value in frame)
        {
            char printChar = value.Value == Life.Dead ? '_' : 'O';
            if (i % _boardWidth == 0)
                Console.WriteLine(printChar);
            else
                Console.Write(printChar);
            i++;
        }    
    }
    public void evolveFrame() {
        int livingNeighbors;
        foreach (var cell in _cellBoardFrameOne)
        {
            livingNeighbors = cellNeighborsCount(cell.Key);
            renderNextFrame(cell.Key, livingNeighbors);
        }
    }
    private int cellNeighborsCount((int x, int y) location) {
        int livingNeighbors = 0;
        // {0,0} is the target cell where (x,y) are the neighboring cells
        // (-1,-1), (0,-1), (1,-1),
        // (-1, 0), {0, 0}, (1, 0),
        // (-1, 1), (0, 1), (1, 1),
        livingNeighbors += _cellBoardFrameOne.ContainsKey((location.x-1,location.y-1)) ? determineState((location.x-1,location.y-1)) : 0; // (0,0)
        livingNeighbors += _cellBoardFrameOne.ContainsKey((location.x,location.y-1)) ? determineState((location.x,location.y-1)) : 0; //(1,0)
        livingNeighbors += _cellBoardFrameOne.ContainsKey((location.x+1,location.y-1)) ? determineState((location.x+1,location.y-1)) : 0; // (2,0)
        livingNeighbors += _cellBoardFrameOne.ContainsKey((location.x-1,location.y)) ? determineState((location.x -1,location.y)) : 0; // (0,1)
        livingNeighbors += _cellBoardFrameOne.ContainsKey((location.x+1,location.y)) ? determineState((location.x+1,location.y)) : 0; // (2,1)
        livingNeighbors += _cellBoardFrameOne.ContainsKey((location.x-1,location.y+1)) ? determineState((location.x-1,location.y+1)) : 0; // (0,2)
        livingNeighbors += _cellBoardFrameOne.ContainsKey((location.x,location.y+1)) ? determineState((location.x,location.y+1)) : 0; // (1,2)
        livingNeighbors += _cellBoardFrameOne.ContainsKey((location.x+1,location.y+1)) ? determineState((location.x+1,location.y+1)) : 0; // (2,2)

        return livingNeighbors;
    }
    private void renderNextFrame((int x, int y) cell, int neighbors) {
        if (neighbors == 1 | neighbors == 2)
            _cellBoardFrameTwo[cell] = _cellBoardFrameOne[cell];
        else if (neighbors == 3)
            _cellBoardFrameTwo[cell] = Life.Alive;
        else
            _cellBoardFrameTwo[cell] = Life.Dead;
    }
    private int determineState((int x, int y) location) => _cellBoardFrameOne[location] == Life.Alive ? 1 : 0;
    private (int, int) coordinateRandomizer(int sideLength) => (Random.Shared.Next(0,_boardWidth),Random.Shared.Next(0,_boardWidth)); 
    // returns a random coordinate within the bounds of the grid, defined by sideLength
    enum Life {Alive, Dead};
}