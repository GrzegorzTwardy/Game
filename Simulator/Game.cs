using Simulator.Maps;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

public class Game
{
    public Map Map { get; }
    public List<IMappable> Mappables { get; }
    public List<Point> Positions { get; }
    public Player P { get; set; } = new Player();

    public Game(Map map, List<IMappable> mappables, List<Point> points)
    {
        Map = map;
        Mappables = mappables ?? throw new ArgumentNullException(nameof(mappables));
        Positions = points;

        // dodanie gracza i mobow do mapy
        for (int i = 0; i < mappables.Count; i++)
        {
            Mappables[i].InitMapandPosition(Map, Positions[i]);
        }
        
        // gracz jest na indeksie 0
        if (Mappables[0] is Player player)
        {
            P = player;
        }
        else
        {
            Console.WriteLine("Pierwszy obiekt nie jest typu Player.");
        }
    }

    public int Run()
    {
        string userInput = "";
        bool inputFlag = true;
        int currentTurn = 0;

        while (userInput != "q".ToLower())
        {
            if (currentTurn != 0)
                Console.WriteLine("======================= MOB MOVE =====================");

            // dodawanie moba co 10 ture
            if (currentTurn > 0 && currentTurn % 10 == 0)
            {
                Random random = new Random();
                List<IMappable> mobsToSpawn = [new Mob("Elandor", level: P.Level)];
                int mobIndex = random.Next(mobsToSpawn.Count);
                int x = random.Next(0, 7);
                int y = random.Next(0, 5);

                while (x == P.Position.X && y == P.Position.Y)
                {
                    x = random.Next(0, 7);
                    y = random.Next(0, 5);
                }

                mobsToSpawn[mobIndex].InitMapandPosition(Map, new Point(x, y));
                Mappables.Add(mobsToSpawn[mobIndex]);
            }

            // NEXT TURN

            Console.WriteLine($"TURN: {currentTurn}");
            this.Visualize();
            Console.WriteLine("q - wyjdź");
            Console.Write("Podaj ruch (r/l/u/d): ");
            userInput = Console.ReadLine().ToLower();

            switch(userInput)
            {
                case "l":
                    P.Go(Direction.Left);
                    break;
                case "r":
                    P.Go(Direction.Right);
                    break;
                case "u":
                    P.Go(Direction.Up);
                    break;
                case "d":
                    P.Go(Direction.Down);
                    break;
                case "q":
                    return 0;
                default:
                    Console.WriteLine("Enter a correct direction.");
                    inputFlag = false;
                    break;
            }
            
            FightCheck();

            currentTurn++;

            Console.WriteLine($"TURN: {currentTurn}");
            Console.WriteLine("======================= PLAYER MOVE =====================");


            if (inputFlag)
            {
                this.Visualize();
                MoveMobs();
            }
            inputFlag = true;
            currentTurn++;

        }
        return 0;
    }


    public void FightCheck()
    {
        List<Mob> mobsOnPlayer = new();
        string playersChoice = "";

        // Find mobs on the player's position
        for (int i = 0; i < Mappables.Count; i++)
        {
            if (Mappables[i].Position.X == P.Position.X && Mappables[i].Position.Y == P.Position.Y)
            {
                if (Mappables[i] is Mob M)
                    mobsOnPlayer.Add(M);
            }
        }

        if (mobsOnPlayer.Count > 0)
        {
            Console.WriteLine("--------- FIGHT INITIATED ---------");

            while (true)
            {
                Visualize();
                Console.Write("Press 'r/l/u/d' to escape or 'f' to keep fighting: ");
                playersChoice = Console.ReadLine();

                // Handle escape
                if (playersChoice == "r" || playersChoice == "l" || playersChoice == "u" || playersChoice == "d")
                {
                    Console.WriteLine("You escaped the fight!");

                    foreach (var mob in mobsOnPlayer)
                    {
                        // Mob rusza się w przeciwną stronę
                        switch (playersChoice)
                        {
                            case "r": // Gracz w prawo, mob w lewo
                                P.Go(Direction.Right);
                                mob.Go(Direction.Left);
                                break;
                            case "l": // Gracz w lewo, mob w prawo
                                P.Go(Direction.Left);
                                mob.Go(Direction.Right);
                                break;
                            case "u": // Gracz w górę, mob w dół
                                P.Go(Direction.Up);
                                mob.Go(Direction.Down);
                                break;
                            case "d": // Gracz w dół, mob w górę
                                P.Go(Direction.Down);
                                mob.Go(Direction.Up);
                                break;
                        }
                    }
                    break;
                }

                // Handle fight
                if (playersChoice == "f")
                {
                    for (int i = mobsOnPlayer.Count - 1; i >= 0; i--) // Iterate backward for safe removal
                    {
                        Mob mob = mobsOnPlayer[i];
                        Console.WriteLine($"Player HP: {P.Hp}, Mob HP: {mob.Hp}");

                        if (mob.Hp > 0)
                        {
                            mob.Hp -= P.Power;
                            Console.WriteLine($"You hit the mob! Mob HP is now {mob.Hp}");
                        }

                        if (mob.Hp <= 0)
                        {
                            Console.WriteLine("You defeated a mob!");
                            Map.Remove(mob, mob.Position);
                            Mappables.Remove(mob);
                            mobsOnPlayer.RemoveAt(i);
                        }

                        if (P.Hp > 0 && mob.Hp > 0) // Mob attacks back only if alive
                        {
                            P.Hp -= mob.Power;
                            Console.WriteLine($"The mob hit you! Your HP is now {P.Hp}");
                        }

                        if (P.Hp <= 0)
                        {
                            Console.WriteLine("Game over! You have been defeated.");
                            Map.Remove(P, P.Position);
                            return; // Exit the method entirely
                        }
                    }

                    if (mobsOnPlayer.Count == 0)
                    {
                        Console.WriteLine("You defeated all mobs!");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Unknown command. Please try again.");
                }
            }
        }
    }


    public void MoveMobs()
    {
        Point CurentPosition = P.Position;
        Random random = new Random();

        for (int i = 0; i < Mappables.Count; i++)
        {
            List<Direction> availableDirections = new();

            int deltaX = Mappables[i].Position.X - CurentPosition.X;
            int deltaY = Mappables[i].Position.Y - CurentPosition.Y;

            // Obliczamy odległość w poziomie (X) i pionie (Y)
            int absDeltaX = Math.Abs(deltaX);
            int absDeltaY = Math.Abs(deltaY);

            // Porównujemy odległości na obu osiach
            if (absDeltaX > absDeltaY)
            {
                // Jeśli odległość w poziomie (X) jest większa, poruszamy się w poziomie
                if (deltaX < 0)
                    availableDirections.Add(Direction.Right);
                else if (deltaX > 0)
                    availableDirections.Add(Direction.Left);
            }

            if (absDeltaY > absDeltaX)
            {
                // Jeśli odległość w pionie (Y) jest większa, poruszamy się w pionie
                if (deltaY < 0)
                    availableDirections.Add(Direction.Up);
                else if (deltaY > 0)
                    availableDirections.Add(Direction.Down);
            }

            // Jeśli odległości na obu osiach są równe, możemy wybrać losowy kierunek
            if (absDeltaX == absDeltaY)
            {
                // Możemy poruszać się w obu kierunkach
                if (deltaX < 0)
                    availableDirections.Add(Direction.Right);
                else if (deltaX > 0)
                    availableDirections.Add(Direction.Left);

                if (deltaY < 0)
                    availableDirections.Add(Direction.Up);
                else if (deltaY > 0)
                    availableDirections.Add(Direction.Down);
            }

            // Jeśli są dostępne kierunki, wybieramy jeden losowo
            if (availableDirections.Count > 0)
            {
                int randomIndex = random.Next(availableDirections.Count);
                Mappables[i].Go(availableDirections[randomIndex]);
            }
        }
    }


    public void Visualize()
    {
        for (int y = Map.SizeY - 1; y >= 0; y--)
        {
            for (int x = 0; x < Map.SizeX; x++)
            {
                var currentPoint = new Point(x, y);
                var mappablesAtPoint = Map.At(currentPoint);
                if (mappablesAtPoint.Count > 1)
                {
                    Console.Write('X');
                }
                else if (mappablesAtPoint.Count == 1)
                {
                    Console.Write(Map.At(currentPoint)[0].Symbol);
                }
                else
                {
                    Console.Write(".");
                }
                Console.Write('\t');
            }
            Console.WriteLine('\n');
        }
    }
}
