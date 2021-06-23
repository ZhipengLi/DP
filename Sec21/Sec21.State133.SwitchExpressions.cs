using Autofac.Core.Activators;
using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;

namespace Sec21.State133
{
    //enum Chest
    //{ 
    //    Open,
    //    Close,
    //    Locked
    //}

    //enum Action
    //{ 
    //    Open,
    //    Close
    //}

    //public class Demo
    //{
    //    static Chest Manipulate
    //        (Chest chest, Action action, bool haveKey) =>
    //    (chest, action, haveKey) switch
    //    {
    //        (Chest.Locked, Action.Open, true) => Chest.Open,
    //        (Chest.Closed, Action.Open, _) => Chest.Open,
    //        (Chest.Open, Action.Close, true) => Chest.Locked,
    //        (Chest.Open, Action.Close, false) => Chest.Closed,
    //        _ => chest
    //    };

    //    static void Main(string[] args)
    //    {
    //        main();
    //        ReadLine();
    //    }
    //    static void main()
    //    {
    //        var chest = Chest.Locked;
    //        WriteLine($"Chest is {chest}");

    //        chest = Manipulate(chest, Action.Open, true);
    //        WriteLine($"Chest is {chest}");

    //        chest = Manipulate(chest, Action.Close, false);
    //        WriteLine($"Chest is {chest}");

    //        chest = Manipulate(chest, Action.Close, false);
    //        WriteLine($"Chest is {chest}");

    //    }
    //}
}
