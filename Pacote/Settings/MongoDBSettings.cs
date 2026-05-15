using System;
using System.Collections.Generic;
using System.Text;

namespace Pacote.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; }
        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}
