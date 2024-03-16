using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Transactions;

namespace lib;

public class BoundBoard
{
    Random random = new();
    public Life[,] _CellBoard { get; }
    private List<int[]> _livingCellCoordinates;
    private int _numOfLivingCells;
    public BoundBoard(int boundX, int boundY, int numOfLivingCells)
    {
        _CellBoard = new Life[boundX,boundY];
        _livingCellCoordinates = randomizeUniqueCoords(numOfLivingCells);
        _numOfLivingCells = numOfLivingCells;
        foreach (int[] coord in _livingCellCoordinates)
            Console.Write(coord);
        populateBoard(numOfLivingCells, _livingCellCoordinates);
    }
    private void populateBoard(int numOfLivingCells, List<int[]> aliveCells)
    {
        randomizeUniqueCoords(numOfLivingCells);
        for (int i = 0; i < _CellBoard.GetLength(0); i++)
            for (int j = 0; j < _CellBoard.GetLength(1); j++)
                    _CellBoard[i,j] = Life.Dead;
        for (int i = 0; i < numOfLivingCells; i++)
        {
            int[] currentCoordinate = setCoord(aliveCells, i);
            _CellBoard[currentCoordinate[0],currentCoordinate[1]] = Life.Alive;
        }
    }
    private int[] setCoord(List<int[]> cellCoords, int iterator) => cellCoords[iterator]; // { 0 [3,4], 1 [2,8], 2 [1,4], 3 [5,5], 4 [{random},{random}]} <-- --> iterator
    private List<int[]> randomizeUniqueCoords(int numOfCoords)
    {
        ///<summary>
        /// Creates different coordinates for the initial living cells to start in
        /// this program checks to see if the randomized coords are contained in the list to avoid 
        /// duplicates and repeats until all X unique coordinates are created and stored in the list.
        ///</summary>
        List<int[]> livingCellCoords = new List<int[]>();
        int[] newCoord = randomizedChoord();
        for (int i = 0; i <= numOfCoords; i++)
        {
            {
                do
                {
                    if(!livingCellCoords.Contains(newCoord))
                        livingCellCoords.Add(newCoord);
                    else
                        newCoord = randomizedChoord();
                }
                while (livingCellCoords.Contains(newCoord));
            }
        }
        return livingCellCoords;
    }
    private int[] randomizedChoord() => [random.Next(0,_CellBoard.GetLength(0)), random.Next(0,_CellBoard.GetLength(1))];
    public void displayBoard()
    {
        Console.Clear();
        for (int i = 0; i < _CellBoard.GetLength(0); i++)
            for (int j = 0; j < _CellBoard.GetLength(1); j++)
            {
                char visualCellChar = _CellBoard[i,j] == Life.Dead ? '_' : 'O';
                if ((j+1) % _CellBoard.GetLength(1) == 0)
                    Console.WriteLine(visualCellChar);
                else
                    Console.Write(visualCellChar);
            }
    }
    private void cellEvolution(List<int[]> targetCoord) 
        {
        for (int i = 0; i < _numOfLivingCells; i++)
        {
            List<int[]> cellBounds = new();
            //foreach (Directions location)
            //    cellBounds.Add(returnBox(targetCoord[i], location));
            
        }
    }
    private int[] returnBox(int[] currentCoord, Directions location)
    {
        int[] newBound = new int[2];
        switch(location)
        {
            case Directions.North:
                newBound[0] = currentCoord[0]--;
                newBound[1] = currentCoord[1]; 
                break;
            case Directions.NorthEast:
                newBound[0] = currentCoord[0]--;
                newBound[1] = currentCoord[1]; 
                break;
            case Directions.East:
                newBound[0] = currentCoord[0]--;
                newBound[1] = currentCoord[1]; 
                break;
            case Directions.SouthEast:
                newBound[0] = currentCoord[0]--;
                newBound[1] = currentCoord[1]; 
                break;
            case Directions.South:
                newBound[0] = currentCoord[0]--;
                newBound[1] = currentCoord[1]; 
                break;
            case Directions.SouthWest:
                newBound[0] = currentCoord[0]--;
                newBound[1] = currentCoord[1]; 
                break;
            case Directions.West:
                newBound[0] = currentCoord[0]--;
                newBound[1] = currentCoord[1]; 
                break;
            case Directions.NorthWest:
                newBound[0] = currentCoord[0]--;
                newBound[1] = currentCoord[1]; 
                break;
        }
        return [];
    }
    public enum Directions { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest }
    public enum Life { Dead, Alive }
}
public class Conways_Game_Of_Life
{
    BoundBoard board = new(41,41, 7);
    private int _livingCellCount { get; }
    private int _livingCellLocations { get; }
    Conways_Game_Of_Life(int livingCellCount)
    {
        _livingCellCount = livingCellCount;

    }
    private int[,] livingCellLocation(int livingCellCount)
    {
        int[,] livingCellLocations = new int[livingCellCount,2];
        for (int i = 0; i < livingCellCount; i++)
        {
            
        }
        return livingCellLocations;
    }
    private int[] getCellLocation(int iteration) {
        int[] coordinates = new int[2];
        int boardWidth = board._CellBoard.GetLength(1);
        Console.Write($"Please enter the X Choord for living cell {iteration + 1} : ");
        int.Parse("");
        return [coordinates[0],coordinates[1]];
    }
}