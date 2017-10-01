

using AustinHarris.JsonRpc;
using CFL_1.CFL_System;
using CFL_1.CFL_Data;
using CFL_1.CFL_System.DB;

namespace CFLServer.ClientCommunication
{
    public class MiscRequests : JsonRpcService
    {
        /// <summary>
        /// Permet au client de tester que le serveur est en route.
        /// </summary>
        [JsonRpcMethod]
        private bool Ping()
        {
            return true;
        }

        [JsonRpcMethod]
        private bool IsConfigComplete()
        {
            CFLConfig _config = CFLDBConnection.instance.Config;

            if(_config != null && _config.IsComplete())
                return true;

            if(!Gate.load.config(ref _config))
                return false;
            if(_config == null || (!_config.IsComplete()))
                return false;

            CFLDBConnection.instance.Config = _config;
            return true;
        }
    }
}
