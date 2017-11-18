using System;
using System.Collections.Generic;
using System.Linq;
using AustinHarris.JsonRpc;
using CFL_1.CFL_System;
using CFL_1.CFL_System.DB;

namespace CFLServer.ClientCommunication
{
    public class DataRequests : JsonRpcService
    {
        [JsonRpcMethod]
        private CFLConfig GetConfig()
        {
            return CFLDBConnection.instance.Config;
        }

        [JsonRpcMethod]
        private bool SetConfig(CFLConfig cflConfig)
        {
            return true;
        }
    }
}
