using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace lib;

public class CellLogic
{
    readonly Random Random = new();
    private Dictionary<int, Life> _cellBoard;
    private readonly int _boardWidth;
    private readonly int _boardDimensions;
    public CellLogic(int sideLength = 3, int livingCells = 3) {
        _boardDimensions = (int) Math.Pow((double) sideLength, 2);
        _boardWidth = sideLength;
        _cellBoard = populateCellBoard(livingCells);
    }

    private Dictionary<int, Life> populateCellBoard(int livingCells) {
        var cellBoard = new Dictionary<int, Life>();
        List<int> livingCellsLocation = randomizedUniqueCoords(livingCells);
        for (int i = 0; i < _boardDimensions; i++)
        {   
            for (int j = 0; j < livingCellsLocation.Count; j++) {
                if (!livingCellsLocation.Contains(i))
                    cellBoard[i] = Life.Dead;
                else
                    cellBoard[i] = Life.Alive;
            }
        }
        return cellBoard;
    }
    private List<int> randomizedUniqueCoords(int livingCells)
    {
        var randomizedCoords = new List<int>();
        int newCell = coordinateRandomizer(_boardDimensions);
        for (int i =0; i < _boardDimensions; i++)
        {
            do
            {
                if (!randomizedCoords.Contains(newCell))
                    randomizedCoords.Add(newCell);
                else
                    newCell = coordinateRandomizer(_boardDimensions);

            } while (randomizedCoords.Contains(newCell));
        }
        return randomizedCoords;
    }
    public void displayBoard() {
        for (int i = 0; i < _boardDimensions; i++)
        {
            char printChar = _cellBoard[i] == Life.Dead ? '_' : '0';
            if ((i+1)%_boardWidth == 0)
                Console.WriteLine(_cellBoard[printChar]);
            else
                Console.Write(_cellBoard[printChar]);
        }
    }
    private int coordinateRandomizer(int sideLength) => Random.Next(0, (int) Math.Pow((double) sideLength, 2));
    enum Life {Alive, Dead};
}