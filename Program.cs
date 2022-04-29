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
        // Initialize grid as a string array containing 9 elements from 1 - 9
        string[] grid = {"1","2","3","4","5","6","7","8","9"};
        // write the grid to the console
        drawInterface(grid);

        // Initialize winner as a nullable integer with a value of null (because there is no winner yet)
        int? winner = null;

        // It is only possible to make 9 moves, so instead of a while gameplay loop, we use a for loop
        for (int i=0; i<9; i++) {
            // Get the modulo (remainder) of i divided by two, because there are two players
            // This will result in player being either a one or a zero
            int player = i % 2;
            
            // Get space selection from the user, then display the result to the console
            selectSpace(player, grid);
            drawInterface(grid);

            // See if either player is detected as a winner so far, store the result in winner
            winner = checkVictory(grid);

            // If winner is no longer null, display the victory!
            if (winner != null) {
                Console.WriteLine($"Player {winner+1} is the winner!");
                Console.Write("\n[Enter] to quit.");
                Console.ReadLine();
                break;
            }
        }
        
        // One last time following our for loop, we need to inform the users that the game was a draw
        //  because no winner was detected
        drawInterface(grid);
        if (winner == null) {
            Console.WriteLine("Nobody won!");
            Console.Write("\n[Enter] to quit.");
            Console.ReadLine();
        }
    }
    static void drawInterface(string[] g) {
        // Clear anything previously written to the console, then write the grid using the string array grid
        Console.Clear();
        Console.WriteLine($"{g[0]}|{g[1]}|{g[2]}\n{g[3]}|{g[4]}|{g[5]}\n{g[6]}|{g[7]}|{g[8]}");
    }
    static string[] selectSpace(int playerNum, string[] grid) {
        // Initilize choice as an integer representing the place the user would like to play
        int choice;
        // Label Redo (explained below)
        Redo:
        try {
        // Ask the user for an integer representing where they want to play and turn it into
        //  an integer. Store this in choice
        Console.Write($"Player {playerNum+1}, choose your placement: ");
        choice = int.Parse(Console.ReadLine());
        choice --; // Convert to index
        // Ensure that the chosen space is not already taken
        choice = checkSpace(choice, grid);
        } catch {
            // If an error is thrown in the above code, whether the user entered a non-int or
            //  an int that is out of range, redraw the grid and goto the label Redo,
            //  which will prompt again an input from the user 
            drawInterface(grid);
            goto Redo;
        }
        // Based upon which player is taking their turn, replace the index of the array
        //  with that player's symbol
        if (playerNum == 0) {
            grid[choice] = "X";
        } else {
            grid[choice] = "O";
        }
        // Return the updated grid
        return grid;
    }
    static int checkSpace(int choice, string[] grid){
        // Ensures that the current player selects a space that has not been filled
        while (true) {
            // Continue looping until the user selects a place that has not been filled already
            // Error handling is the same as previously explained just after the catch in
            //  the selectSpace function
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
                // If the place that is trying to be played is not yet taken, move on from the while loop
                break;
            }
        }
        // Return the validated input from the user
        return choice;
    }
    static int? checkVictory(string[] g) {
        // For each player, check if the string array grid matches any possible win condition
        for (var i=0; i<2; i++) {
            // If any win condition is true, return the number of that player
            // Otherwise return null
            
            // Loop through with key as either "X" or "O"
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