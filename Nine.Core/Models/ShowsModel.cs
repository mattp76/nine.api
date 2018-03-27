using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nine.Core.Models
{
    public class ShowsModel
    {
        public string country { get; set; }
        public string description { get; set; }
        public bool drm { get; set; }
        public int episodeCount { get; set; }
        public string genre { get; set; }
        public ShowImageModel image { get; set; }
        public string language { get; set; }
        public NextEpisode nextEpisode { get; set; }
        public string primaryColour { get; set; }
        public List<SeasonSlugModel> seasons { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
        public string tvChannel { get; set; }
    }

    public class ShowImageModel
    {
        public string showImage { get; set; }
    }

    public class SeasonSlugModel
    {
        public string slug { get; set; }
    }

    public class NextEpisode
    {
        public string channel { get; set; }
        public string channelLogo { get; set; }
        public string date { get; set; }
        public string html { get; set; }
        public string url { get; set; }
    }
}
