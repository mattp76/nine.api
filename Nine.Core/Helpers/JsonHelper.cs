using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nine.Core.Interfaces;
using System;

namespace Nine.Core.Helpers
{
    public class JsonHelper : IJsonHelper
    {
        public bool CheckForValidJson<T>(T model)
        {
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var obj = JToken.Parse(json);
                return true;
            }
            catch (Exception e)
            {
                //Exception in parsing json
                return false;
            }
        }
    }
}
