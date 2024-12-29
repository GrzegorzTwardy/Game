using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simulator;
using Simulator.Maps;
using System;

namespace SimWeb.Pages
{
    public class SimulationModel : PageModel
    {   
        public int CurrentTurn { get; set; } = 0;
        private SimulationHistory? _history;
        public string CurrentMap { get; set; } = "";
        public string MovementInfo { get; set; } = "No creature has made a move.";

        public bool DisablePrev => CurrentTurn <= 0;
        public bool DisableNext => CurrentTurn >= (_history?.TurnLogs.Count - 1);


        private void BuildCurrentMap()
        {
            if (_history == null) return;

            string gridStyle = $"style='display: grid; grid-template-columns: 20px repeat({_history.SizeX}, 100px); " +
                               $"grid-template-rows: repeat({_history.SizeY}, 100px) 20px; gap: 10px;'";


            var currentSymbols = _history.TurnLogs[CurrentTurn].Symbols;

            for (int y = _history.SizeY-1; y>=0; y--)
            {
                for (int x = 0; x < _history.SizeX; x++)
                {
                    if (x == 0) CurrentMap += $"<p class='indeks'>{y}</p>";

                    var logSymbol = currentSymbols[new Point(x, y)];
                    string cellSymbol = "";
                    if (string.IsNullOrEmpty(currentSymbols[new Point(x, y)].ToString().Trim()))
                        cellSymbol = "blank";
                    else if (logSymbol.ToString() == "b")
                        cellSymbol = "pn";
                    else if (logSymbol.ToString() == "B")
                        cellSymbol = "pl";
                    else cellSymbol = logSymbol.ToString();

                    if (cellSymbol != "blank")
                    {
                        string img = $"<img src=\"/css/{cellSymbol}.png\">";
                        CurrentMap += $"<div class='cell'>{img}</div>";
                    }
                    else
                    {
                        CurrentMap += "<div class='cell'></div>";
                    }
                }
            }
            CurrentMap += "<div class='indeks'></div>";
            for (int i=0; i<_history.SizeX; i++)
                CurrentMap += $"<span class='indeks'>{i}</span>";


            CurrentMap = CurrentMap.Insert(0, $"<div class='map' {gridStyle}>");
            CurrentMap += "</div>";
        }

        public SimulationHistory GetSimulationHistory()
        {
            BigBounceMap map = new(8, 6);

            List<IMappable> mappables = [new Orc("Gorbag"), new Elf("Elandor"), new Birds("orły", true), new Birds("strusie", false, 5), new Animals("króliki")];
            List<Point> points = [new(0, 2), new(3, 5), new(0, 5), new(7, 5), new(7, 1)];
            string moves = "lulur dllld dlull dluul uu";

            Simulation sim = new(map, mappables, points, moves);
            SimulationHistory simHistory = new(sim);
            return simHistory;
        }

        public void OnGet()
        {
            if (_history == null)
                _history = GetSimulationHistory();

            CurrentTurn = HttpContext.Session.GetInt32("Turn") ?? 0;

            if (CurrentTurn >= _history.TurnLogs.Count)
                CurrentTurn = _history.TurnLogs.Count - 1;

            BuildCurrentMap();

            if (_history.TurnLogs[CurrentTurn].Mappable == null)
                MovementInfo = "Starting position:";
            else
            {
                string creature = _history.TurnLogs[CurrentTurn].Mappable.Info;
                string move = _history.TurnLogs[CurrentTurn].Move;

                MovementInfo = $"{creature} ⇒ {move}";
            }
        }

        public IActionResult OnPostNext()
        {
            if (_history == null)
                _history = GetSimulationHistory();

            CurrentTurn = HttpContext.Session.GetInt32("Turn") ?? 0;
            CurrentTurn++;

            if (CurrentTurn >= _history.TurnLogs.Count)
                CurrentTurn = _history.TurnLogs.Count - 1;

            HttpContext.Session.SetInt32("Turn", CurrentTurn);
            BuildCurrentMap();

            if (_history.TurnLogs[CurrentTurn].Mappable == null)
                MovementInfo = "Starting position:";
            else
            {
                string creature = _history.TurnLogs[CurrentTurn].Mappable.Info;
                string move = _history.TurnLogs[CurrentTurn].Move;

                MovementInfo = $"{creature} ⇒ {move}";
            }

            return Page();
        }

        public IActionResult OnPostPrevious()
        {
            if (_history == null)
                _history = GetSimulationHistory();

            CurrentTurn = HttpContext.Session.GetInt32("Turn") ?? 0;
            CurrentTurn--;

            if (CurrentTurn < 0)
                CurrentTurn = 0;

            HttpContext.Session.SetInt32("Turn", CurrentTurn);
            BuildCurrentMap();

            if (_history.TurnLogs[CurrentTurn].Mappable == null)
                MovementInfo = "Starting position:";
            else
            {
                string creature = _history.TurnLogs[CurrentTurn].Mappable.Info;
                string move = _history.TurnLogs[CurrentTurn].Move;

                MovementInfo = $"{creature} ⇒ {move}";
            }

            return Page();
        }
    }
}
