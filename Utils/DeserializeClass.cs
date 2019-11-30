using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishGram.Utils
{
    public class PhotoIns
    {
        public Data data { get; set; }
        public Graphql graphql { get; set; }
    }

    public class Graphql
    {
        public User user { get; set; }
        public ShortCode shortcode_media { get; set; }
    }

    public class Data
    {
        public User user { get; set; }
    }

    public class User
    {
        public Timeline edge_owner_to_timeline_media { get; set; }
    }

    public class Timeline
    {
        public PageInfo page_info { get; set; }
        public List<Edges> edges { get; set; }
    }

    public class PageInfo
    {
        public bool has_next_page { get; set; }

        public string end_cursor { get; set; }
    }

    public class Edges
    {
        public Nodes node { get; set; }
    }

    public class ShortCode
    {
        public User user { get; set; }
        public Timeline edge_sidecar_to_children { get; set; }
    }

    public class Nodes
    {
        public string shortcode { get; set; }
        public string display_url { get; set; }
    }
}