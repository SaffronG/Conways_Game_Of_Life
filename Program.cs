namespace Conways_Game_Of_Life;
using lib;

class Program
{
    static void Main(string[] args)
    {
        CellLogic logic = new(9,9);
        logic.gameLoop();
    }
}
