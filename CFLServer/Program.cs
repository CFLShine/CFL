using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AustinHarris.JsonRpc;
using CFL_1;
using CFL_1.CFL_Data;

namespace CFLServer
{
    class Program
    {
        private static object _svc;

        static void Main(string[] args)
        {
            // must new up an instance of the service so it can be registered to handle requests.
            _svc = new CFLServerContext();

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
