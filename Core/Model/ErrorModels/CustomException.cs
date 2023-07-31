using System;
using System.Net;
using Newtonsoft.Json;

namespace Core.Model.ErrorModels
{
    [JsonObject (MemberSerialization.OptIn)]
    public class CustomException: SystemException
    {
        
        [JsonProperty]
        public HttpStatusCode StatusCode;
        [JsonProperty]
        public string ErrorMessage;

        public CustomException(HttpStatusCode statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}