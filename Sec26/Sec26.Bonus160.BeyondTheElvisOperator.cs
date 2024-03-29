﻿using Autofac.Core.Activators;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;

namespace Sec26.Bonus160
{
    public static class Maybe
    {
        public static TResult With<TInput, TResult>(this TInput o,
            Func<TInput, TResult> evaluator)
           where TResult : class
            where TInput : class
        {
            if (o == null)
                return null;
            else return evaluator(o);
        }

        public static TInput If<TInput>(this TInput o,
            Func<TInput, bool> evaluator)
            where TInput : class
        {
            if (o == null)
                return null;
            return evaluator(o) ? o : null;
        }

        public static TInput Do<TInput>(this TInput o, Action<TInput> action)
        where TInput : class
        {
            if (o == null)
                return null;
            action(o);
            return o;
        }

        public static TResult Return<TInput, TResult>(this TInput o,
            Func<TInput, TResult> evaluator, TResult failureValue)
            where TInput : class
        {
            if (o == null)
                return failureValue;
            return evaluator(o);
        }

        public static TResult WithValue<TInput, TResult>(this TInput o,
            Func<TInput, TResult> evaluator)
            where TInput : struct
        {
            return evaluator(o);
        }
    }


    //==============================================================================================
    public class Person
    {
        public Address Address { get; set; }
    }
    public class Address
    {
        public string PostCode { get; set; }
    }

    public class MaybeMonadDemo
    {
        public void MyMethod(Person p)
        {
            //string postcode;
            //if (p != null)
            //{
            //    if (HasMedicalRecord(p) && p.Address != null)
            //    {
            //        CheckAddress(p.Address);
            //        if (p.Address.PostCode != null)
            //            postcode = p.Address.PostCode.ToString();
            //        else
            //            postcode = "UNKNOWN";
            //    }
            //}
            string postcode = p.With(x => x.Address).With(x => x.PostCode);

            postcode = p
                .If(HasMedicalRecord)
                .With(x => x.Address)
                .Do(CheckAddress)
                .Return(x => x.PostCode, "Unknown");

        }

        private void CheckAddress(Address address)
        {

        }

        private bool HasMedicalRecord(Person person)
        {
            return true;
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

        }
    }

    //==============================================================================================
    [TestFixture]
    public class Tests
    {
        private void CheckAddress(Address address)
        {

        }

        private bool HasMedicalRecord(Person person)
        {
            return true;
        }
        [Test]
        public void BasicTest()
        {
            const string PostCode = "98000";
            Person p = new Person { Address = new Address() { PostCode = PostCode } };
            string postcode = p.With(x => x.Address).With(x => x.PostCode);

            postcode = p
                .If(HasMedicalRecord)
                .With(x => x.Address)
                .Do(CheckAddress)
                .Return(x => x.PostCode, "Unknown");

            Assert.AreEqual(postcode, PostCode);
        }
    }
}
