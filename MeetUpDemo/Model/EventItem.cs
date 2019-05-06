using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpDemo.Model
{
    class EventItem
    {
        public string id { get; set; }
        public string utc_offset { get; set; }
        public string rsvp_limit { get; set; }
        public string headcount { get; set; }
       
        public string yes_rsvp_count { get; set; }
        public string duration { get; set; }
        public string photo_url { get; set; }
        public string visibility { get; set; }
        public string waitlist_count { get; set; }
        public string created { get; set; }
        public string maybe_rsvp_count { get; set; }
        public string how_to_find_us { get; set; }
        public string event_url { get; set; }
        public string name { get; set; }
        public string time { get; set; }
        public string updated { get; set; }
        public string status { get; set; }

        public group group { get; set; }

        public venue venue { get; set; }

        public fee fee { get; set; }

        public string description { get; set; }

    }
}
