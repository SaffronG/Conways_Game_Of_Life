namespace Conways_Game_Of_Life;
using lib;

class Program
{
    static void Main(string[] args)
    {
        BoundBoard board = new(20,20,6);
        board.displayBoard();
    }
}
