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

namespace Sec22.Strategy138
{
    public enum OutputFormat
    {
        Markdown,
        Html
    }
    public interface IListStrategy
    {
        void Start(StringBuilder sb);
        void End(StringBuilder sb);
        void AddlistItem(StringBuilder sb, string item);
    }
    public class HtmlListStrategy : IListStrategy
    {
        public void Start(StringBuilder sb)
        {
            sb.AppendLine("<ul>");
        }

        public void End(StringBuilder sb)
        {
            sb.AppendLine("</ul>");
        }

        public void AddlistItem(StringBuilder sb, string item)
        {
            sb.AppendLine("   <li>{item}</li>");
        }
    }

    public class MarkdownListStrategy : IListStrategy
    {
        public void AddlistItem(StringBuilder sb, string item)
        {
            sb.AppendLine($" * {item}");
        }

        public void End(StringBuilder sb)
        {
        }

        public void Start(StringBuilder sb)
        {
        }
    }
    public class TextProcessor<LS> where LS: IListStrategy, new()
    {
        private StringBuilder sb = new StringBuilder();
        private IListStrategy listStrategy = new LS();

        //public void SetOutputFormat(OutputFormat format)
        //{
        //    switch (format)
        //    {
        //        case OutputFormat.Markdown:
        //            listStrategy = new MarkdownListStrategy();
        //            break;
        //        case OutputFormat.Html:
        //            listStrategy = new HtmlListStrategy();
        //            break;
        //        default:
        //            throw new ArgumentNullException();
        //    }
        //}
        public void AppendList(IEnumerable<string> items)
        {
            listStrategy.Start(sb);
            foreach (var item in items)
                listStrategy.AddlistItem(sb, item);
            listStrategy.End(sb);
        }
        public StringBuilder Clear()
        {
            return sb.Clear();
        }
        public override string ToString()
        {
            return sb.ToString();
        }
    }
    public class Demo
    {
        static void Main(string[] args)
        {
            main();
            ReadLine();
        }
        static void main()
        {
            var tp = new TextProcessor<MarkdownListStrategy>();
            tp.AppendList(new[] { "foo", "bar", "baz" });
            WriteLine(tp);

        }
    }
}
