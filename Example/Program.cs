using Melas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            RunConsole();

            while (true) ;
        }

        private static void RunConsole()
        {
            Connection salem = new Connection(Server.Live);
            Console.Write("Type your username in: ");
            salem.Username = Console.ReadLine();
            Console.Write("Type your password in: ");
            salem.Password = Console.ReadLine();
            salem.Connect();
            salem.MessageReceived += Salem_MessageReceived;

            Console.Read();
        }

        private static void Salem_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
        }
    }
}
