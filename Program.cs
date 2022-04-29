/*
    01 Ponder & Prove: Developer
    Title: tictactoe
    Author: Joshua Johnson
    Description:
                Play a text-based game of tic-tac-toe!
                This program also includes detecting and dealing with improper entries by the user:
                    If the user enters a string, they are simply prompted again to enter a number.
                    If they enter a number outside of the expected range or in a place that is
                        already taken, they will be prompted again for a response.
*/

// what needs to get done here?
// functions:
//          drawInterface()
//          selectSpace()
//          checkSpace() ^
//          checkVictory()
//
// interface sample:
//          1|2|3    1|2|3     O|2|3    O|2|3    O|O|3      
//          4|5|6    4|5|6     4|5|6    4|5|6    4|5|6
//          7|8|9    7|X|8     7|X|8    X|X|8    X|X|X       "Player 1 wins!"
class Game {
    static void Main()
    {
        string[] grid = {"1","2","3","4","5","6","7","8","9"};
        drawInterface(grid);

        int? winner = null;

        for (int i=0; i<9; i++) {
            int player = i % 2;
            
            selectSpace(player, grid);
            drawInterface(grid);

            winner = checkVictory(grid);

            if (winner != null) {
                Console.WriteLine($"Player {winner+1} is the winner!");
                Console.Write("\n[Enter] to quit.");
                Console.ReadLine();
                break;
            }
        }
        drawInterface(grid);
        if (winner == null) {
            Console.WriteLine("Nobody won!");
            Console.Write("\n[Enter] to quit.");
            Console.ReadLine();
        }
    }
    static void drawInterface(string[] g) {
        Console.Clear();
        Console.WriteLine($"{g[0]}|{g[1]}|{g[2]}\n{g[3]}|{g[4]}|{g[5]}\n{g[6]}|{g[7]}|{g[8]}");
    }
    static string[] selectSpace(int playerNum, string[] grid) {
        int choice;
        Redo:
        try {
        Console.Write($"Player {playerNum+1}, choose your placement: ");
        choice = int.Parse(Console.ReadLine());
        choice --; // Convert to index
        choice = checkSpace(choice, grid);
        } catch {
            drawInterface(grid);
            goto Redo;
        }

        if (playerNum == 0) {
            grid[choice] = "X";
        } else {
            grid[choice] = "O";
        }
        return grid;
    }
    static int checkSpace(int choice, string[] grid){
        while (true) {
            if (grid[choice] == "X" || grid[choice] == "O") {
                Console.WriteLine("That spot has been taken. Please choose another.");
                Redo:
                try {
                Console.Write("Enter another space: ");
                choice = int.Parse(Console.ReadLine());
                } catch {
                    goto Redo;
                }
                choice--; // Convert to index
            } else {
                break;
            }
        }
        return choice;
    }
    static int? checkVictory(string[] g) {
        for (var i=0; i<2; i++) {
            string[] XorO = {"X","O"};
            string key = XorO[i];

            if (g[0] == key             // X|X|X
             && g[1] == key             // *|*|*
             && g[2] == key) {          // *|*|*
                return i;
            } else if (g[3] == key      // *|*|*
                    && g[4] == key      // X|X|X
                    && g[5] == key) {   // *|*|*
                return i;
            } else if (g[6] == key      // *|*|*
                    && g[7] == key      // *|*|*
                    && g[8] == key) {   // X|X|X
                return i;
            } else if (g[0] == key      // X|*|*
                    && g[3] == key      // X|*|*
                    && g[6] == key) {   // X|*|*
                return i;
            } else if (g[1] == key      // *|X|*
                    && g[4] == key      // *|X|*
                    && g[7] == key) {   // *|X|*
                return i;
            } else if (g[2] == key      // *|*|X
                    && g[5] == key      // *|*|X
                    && g[8] == key) {   // *|*|X
                return i;
            } else if (g[0] == key      // X|*|*
                    && g[4] == key      // *|X|*
                    && g[8] == key) {   // *|*|X
                return i;
            } else if (g[2] == key      //*|*|X
                    && g[4] == key      //*|X|*
                    && g[6] == key) {   //X|*|*
                return i;
            }
        }
        return null;
    }
}