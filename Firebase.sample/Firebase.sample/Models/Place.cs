using System;
namespace Firebase.sample.Models
{
    public class Place : FirebaseModel
    {
        public string PlaceName { get; set; }
        public string PlaceImage { get; set; }
        public string Description { get; set; }
    }
}
