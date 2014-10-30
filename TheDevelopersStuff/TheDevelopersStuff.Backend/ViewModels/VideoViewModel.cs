using System;
using System.Collections.Generic;

namespace TheDevelopersStuff.Backend.ViewModels
{
    namespace Obsolete
    {
    

    public class VideoViewModel
    {
        public VideoViewModel()
        {
            Tags = new List<string>();
        }

        public string Url { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public List<string> Tags { get; set; }
    }
}

    public class VideoViewModel
    {
        public VideoViewModel()
        {
            Tags = new List<string>();
        }

        public string Url { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public List<string> Tags { get; set; }

        public ChannelInfoViewModel ChannelInfo { get; set; }
    }

    public class ChannelInfoViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}