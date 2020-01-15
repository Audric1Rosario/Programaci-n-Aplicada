using System;
using System.Threading;

namespace AdivinaUnNumero
{
    class Program
    {
        // Clase 2
        public static Game CurrentGame;
        static Thread inputThread;
        static void Main(string[] args)
        {
            CurrentGame = new Game();
            CurrentGame.GameInit();
            inputThread = new Thread(Input);
            inputThread.Start();
            while(CurrentGame.CurrentState != Game.eGameState.Over)
            {
                switch (CurrentGame.CurrentState)
                {
                    case Game.eGameState.Starting:
                        Console.WriteLine("Digite un valor entre 1 y 1000: ");
                        CurrentGame.CurrentState = Game.eGameState.Playing;
                        break;
                    case Game.eGameState.Playing:
                        if (CurrentGame.LastTry == 0)
                            continue;

                        // Check if secret number is guessed.
                        switch (CurrentGame.CheckIfGuessed())
                        {
                            case Game.AttemptResult.Greater:
                                Console.WriteLine("El número secreto es menor.");
                                break;
                            case Game.AttemptResult.Lower:
                                Console.WriteLine("El número secreto es mayor.");
                                break;
                            case Game.AttemptResult.Guessed:
                                Console.WriteLine("Ha Adivinado :D");
                                CurrentGame.CurrentState = Game.eGameState.Over;
                                break;                                
                        }

                        if (CurrentGame.CurrentState != Game.eGameState.Over)
                            Console.WriteLine("Digite otro valor: ");

                        // And reset the last try.
                        CurrentGame.LastTry = 0;
                        break;
                }
            }            
            Console.WriteLine($"Ha adivinado en {CurrentGame.Attempts} intentos.");
            Console.WriteLine($"Lo ha hecho en {CurrentGame.TimeSpent.TotalSeconds} segundos.");        
            Console.WriteLine("Muchas gracias por jugar :D");
            CurrentGame.SaveState();
            Console.ReadKey();
        }

        static void Input()
        {
            int _currentValue = 0;

            while (CurrentGame.CurrentState != Game.eGameState.Over)
            {
                _currentValue = int.Parse(Console.ReadLine());
                CurrentGame.LastTry = _currentValue;
            }
        }
    }
}
