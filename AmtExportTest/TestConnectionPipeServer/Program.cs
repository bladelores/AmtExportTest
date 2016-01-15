using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.IO.Pipes;
using Modem.Amt.Export.Utility;

namespace TestConnectionPipeServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("TestConnectionPipe", PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            pipeServer.WaitForConnection();
            Console.WriteLine("Connected");
            StreamString ss = new StreamString(pipeServer);
            for (int i = 0; i < 5; i++)
            {
                ss.WriteString("11 12 13 14 15");
                Thread.Sleep(1000);
            }
            ss.WriteString("end");
            //Thread.Sleep(10000);
            pipeServer.Close();
        }
    }
}
