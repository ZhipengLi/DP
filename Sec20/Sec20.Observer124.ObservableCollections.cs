using Autofac;
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
using System.Reactive.Linq;
using System.ComponentModel;

namespace Sec20.Observer124
{
    //public class Market : INotifyPropertyChanged
    //{
    //    private float volatility;
    //    public float Volatility
    //    {
    //        get => volatility;
    //        set
    //        {
    //            if (value.Equals(volatility))
    //                return;
    //            volatility = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    //[NotifyPropertyChangedInvocator]
    //    protected virtual void OnPropertyChanged(string propertyName = "volatility")
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    //    }
    //}

    //public class Market
    //{
    //    private List<float> prices = new List<float>();
    //    public void AddPrice(float price)
    //    {
    //        prices.Add(price);
    //        PriceAdded?.Invoke(this, price);
    //    }
    //    public event EventHandler<float> PriceAdded;
    //}
    public class Market
    {
        public BindingList<float> Prices = new BindingList<float>();
        public void AddPrice(float price)
        {
            Prices.Add(price);
        }
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
            var market = new Market();
            market.Prices.ListChanged += (sender, eventArgs) =>
            {
                if (eventArgs.ListChangedType == ListChangedType.ItemAdded)
                {
                    float price = ((BindingList<float>)sender)[eventArgs.NewIndex];
                    WriteLine($"Binding list got a price of {price}");
                }
            };
            market.AddPrice(123);
        }
    }

}