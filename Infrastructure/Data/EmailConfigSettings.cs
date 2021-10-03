using System;

namespace Infrastructure.Data
{
    public class EmailConfigSettings
    {
        public string SmtpServer { get; set; }

        public int SmtpPort { get; set; }

        public string SmtpUsername { get; set; }

        public string SmtpPassword { get; set; }

        public string SmtpMessageTo {get; set;}
    }
}