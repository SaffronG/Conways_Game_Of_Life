using System.Runtime.InteropServices;

namespace lib;

public class BoundBoard
{
    public char[,] emptyBoard { get; }
    public BoundBoard(int boundX, int boundY)
    {
        emptyBoard = new char[boundX,boundY];
        populateBoard();
    }
    private void populateBoard()
    {
        for (int i = 0; i < emptyBoard.GetLength(0); i++)
            for (int j = 0; j < emptyBoard.GetLength(1); j++)
                emptyBoard[i,j] = '.';
    }
    public void displayBoard()
    {
        for (int i = 0; i < emptyBoard.GetLength(0); i++)
            for (int j = 0; j < emptyBoard.GetLength(1); j++)
            {
                if (j % emptyBoard.GetLength(1) == 0)
                    Console.WriteLine(emptyBoard[i,j]);
                else
                    Console.Write(emptyBoard[i,j]);
            }
    }
}
public class Conways_Game_Of_Life
{
    BoundBoard board = new(41,41);
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
        int boardWidth = board.emptyBoard.GetLength(1);
        Console.Write($"Please enter the X Choord for living cell {iteration + 1} : ");
        int.Parse("");
        return [coordinates[0],coordinates[1]];
    }
    enum Direction {Up, Down, Left, Right}
}