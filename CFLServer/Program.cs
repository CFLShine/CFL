using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AustinHarris.JsonRpc;
using CFL_1;
using CFL_1.CFL_Data;
using CFLServer.ClientCommunication;

namespace CFLServer
{
    class Program
    {
        static public MiscRequests MiscRequests;
        static public DataRequests DataRequests;

        static void Main(string[] args)
        {
            // instances des classes JsonRpcService
            MiscRequests = new MiscRequests();    
            DataRequests = new DataRequests();
            // 

            var rpcResultHandler = new AsyncCallback(
                state =>
                {
                    var async = ((JsonRpcStateAsync)state);
                    var result = async.Result;
                    var writer = ((StreamWriter)async.AsyncState);

                    writer.WriteLine(result);
                    writer.FlushAsync();
                });

            SocketListener.start(3333, (writer, line) =>
            {
                var async = new JsonRpcStateAsync(rpcResultHandler, writer) { JsonRpc = line };
                JsonRpcProcessor.Process(async, writer);
            });
        }
    }
}
