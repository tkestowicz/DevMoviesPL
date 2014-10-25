using System;
using System.Collections.Generic;

namespace TheDevelopersStuff.Backend.ViewModels
{
    public class VideoViewModel
    {
        public VideoViewModel()
        {
            Tags = new List<string>();
        }

        public string Url { get; internal set; }

        public string Id { get; internal set; }

        public string Name { get; internal set; }
        public DateTime PublicationDate { get; internal set; }
        public string Description { get; internal set; }
        public int Views { get; internal set; }
        public int Likes { get; internal set; }
        public int Dislikes { get; internal set; }
        public List<string> Tags { get; internal set; }
    }
}