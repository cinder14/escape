using System;

namespace Escape
{
    public class Rescue
    {
        public Rescue()
        {
        }

        public Guid rescue_id { get; set; }
        public DateTime stamp_utc { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}

