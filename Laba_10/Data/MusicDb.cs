using Laba_10.Models;
namespace Laba_10.Data
{
    public static class MusicDb
    {
        public static List<Track> Tracks = new List<Track>
        {
            new Track
            {
                Id = 1,
                Title = "Что ни день, то новость",
                Artist = "Никита Мастяк",
                FilePath = "music/track_1.mp3",
                Year = 2021
            },
            new Track
            {
                Id = 2,
                Title = "Хомо Саспенс",
                Artist = "DEEP-EX-SENSE",
                FilePath = "music/track_2.mp3",
                Year = 2018
            },
            new Track
            {
                Id = 3,
                Title = "From the D 2 the LBC",
                Artist = "Eminem, Snoop Dogg",
                FilePath = "music/track_3.mp3",
                Year = 2022
            },
            new Track
            {
                Id = 4,
                Title = "Субстрат",
                Artist = "Лжедмитрий IV",
                FilePath = "music/track_4.mp3",
                Year = 2021
            },
            new Track
            {
                Id = 5,
                Title = "Клубок",
                Artist = "verch.fate",
                FilePath = "music/track_5.mp3",
                Year = 2024
            },
        };
    }
}
