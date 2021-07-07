using Autofac.Core.Activators;
using NUnit.Framework;
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
        public string Run()
        {
            Start();
            while (!HaveWinner)
                TakenTurn();
            WriteLine($"Player {WinningPlayer} wins.");
            return $"Player {WinningPlayer} wins.";
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
        protected abstract int WinningPlayer { get; }
    }
    public class Chess : Game
    { 
        public Chess(int numOfPlayer, int maxTurns):base(numOfPlayer)
        {
            this.maxTurns = maxTurns;
        }
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
        protected override int WinningPlayer => currentPlayer;
        private int turn = 1;
        private int maxTurns;
    }

    //=============================================================================
    public class Demo
    {
        static void Main(string[] args)
        {
            main();
            ReadLine();
        }
        static void main()
        {
            var chess = new Chess(2, 10);
            chess.Run();
        }
    }

    //=============================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ClassTest()
        {
            Assert.AreEqual("Int32", typeof(Game).GetField("currentPlayer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .FieldType.Name);
            Assert.AreEqual("Int32", typeof(Game).GetField("numberOfPlayers", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .FieldType.Name);
            Assert.AreEqual("Boolean", typeof(Game).GetProperty("HaveWinner", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .PropertyType.Name);
            Assert.AreEqual("Int32", typeof(Game).GetProperty("WinningPlayer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .PropertyType.Name);
            Assert.IsNotNull(typeof(Game).GetMethod("Start", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance));
            Assert.IsNotNull(typeof(Game).GetMethod("TakenTurn", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance));

            var chess = new Chess(2, 10);
            Assert.IsTrue(chess is Game);
        }
        [Test]
        public void BasicTest()
        {
            var chess = new Chess(2, 10);
            Assert.AreEqual("Player 1 wins.", chess.Run());
        }
    }
}
