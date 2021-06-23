using Autofac.Core.Activators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;

namespace Sec23.TemplateMethod142
{
    public abstract class Game
    {
        public void Run()
        {
            Start();
            while (!HaveWinner)
                TakenTurn();
            WriteLine($"Player {winningPlayer} wins.");
        }
        protected int currentPlayer;
        protected readonly int numberOfPlayers;
        protected Game(int numberOfPlayers)
        {
            this.numberOfPlayers = numberOfPlayers;
        }
        protected abstract void Start();
        protected abstract void TakenTurn();
        protected abstract bool HaveWinner { get; }
        protected abstract int winningPlayer { get; }
    }
    public class Chess : Game
    { 
        public Chess():base(2)
        { }
        protected override void Start()
        {
            WriteLine($"Startting a game of chess with {numberOfPlayers}");
        }
        protected override void TakenTurn()
        {
            WriteLine($"Turn {turn++} taken by player {currentPlayer}");
            currentPlayer = (currentPlayer + 1) % numberOfPlayers;
        }
        protected override bool HaveWinner => turn == maxTurns;
        protected override int winningPlayer => currentPlayer;
        private int turn = 1;
        private int maxTurns = 10;
    }
    public class Demo
    {
        //static void Main(string[] args)
        //{
        //    main();
        //    ReadLine();
        //}
        static void main()
        {
            var chess = new Chess();
            chess.Run();
        }
    }
}
