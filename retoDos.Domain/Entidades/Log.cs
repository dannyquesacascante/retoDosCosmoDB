using System.Text.Json.Serialization;

namespace retoDos.Domain.Entidades
{
    public class Log
    {

        public string id { get; set; } = Guid.NewGuid().ToString();
        public string LogMessage { get; set; }

        public enum UserLog
        {
            Info,
            Warning,
            Error

        }

        [JsonConverter(typeof(JsonStringEnumConverter))] 
        public UserLog Logtype { get; set; }

        

    }
}
