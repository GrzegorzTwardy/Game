using Simulator.Maps;
using System;
using System.Collections.Generic;
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

        for (int i = 0; i < mappables.Count; i++)
        {
            Mappables[i].InitMapandPosition(Map, Positions[i]);
        }

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

        while (userInput != "q".ToLower())
        {
            Console.Clear();
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
            Console.WriteLine("============================ PLAYER =============================");


            if (inputFlag)
            {
                this.Visualize();
                MoveMobs();
            }
            inputFlag = true;
        }
        return 0;
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
