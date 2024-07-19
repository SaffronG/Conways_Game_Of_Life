namespace Conways_Game_Of_Life;
using lib;

class Program
{
    static void Main(string[] args)
    {
        CellLogic logic = new(50,100);
        logic.gameLoop();
    }
}
