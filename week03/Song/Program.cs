using System;
using System.Collections.Generic;

namespace Song
{
    public enum SongGenre
    {
        Unclassified = 0,
        Pop = 0b1,
        Rock = 0b10,
        Blues = 0b100,
        Country = 0b1_000,
        Metal = 0b10_000,
        Soul = 0b100_000
    }

    public class Program
    {
        static void Main(string[] args)
        {
            //To test the constructor and the ToString method
            Console.WriteLine(new Song("Baby", "Justin Bebier", 3.35, SongGenre.Pop));

            //This is first time that you are using the bitwise or. It is used to specify a combination of genres
            Console.WriteLine(new Song("The Promise", "Chris Cornell", 4.26, SongGenre.Country | SongGenre.Rock));

            Library.LoadSongs("/Users/fanik/Downloads/Week_03_lab_09_songs4.txt");     //Class methods are invoke with the class name
            Console.WriteLine("\n\nAll songs");
            Library.DisplaySongs();

            SongGenre genre = SongGenre.Rock;
            Console.WriteLine($"\n\n{genre} songs");
            Library.DisplaySongs(genre);

            string artist = "Bob Dylan";
            Console.WriteLine($"\n\nSongs by {artist}");
            Library.DisplaySongs(artist);

            double length = 5.0;
            Console.WriteLine($"\n\nSongs more than {length}mins");
            Library.DisplaySongs(length);

        }
    }

    public class Song
    {
        public string Artist { get; }
        public string Title { get; }
        public double Length { get; }
        public SongGenre Genre { get; }

        public Song(string title, string artist, double length, SongGenre genre)
        {
            Artist = artist;
            Title = title;
            Length = length;
            Genre = genre;
        }

        public override string ToString()
        {
            string genre = "";

            if (Genre.HasFlag(SongGenre.Pop))
            {
                genre += "Pop, ";
            }
            if (Genre.HasFlag(SongGenre.Rock))
            {
                genre += "Rock, ";
            }
            if (Genre.HasFlag(SongGenre.Blues))
            {
                genre += "Blues, ";
            }
            if (Genre.HasFlag(SongGenre.Country))
            {
                genre += "Country, ";
            }
            if (Genre.HasFlag(SongGenre.Metal))
            {
                genre += "Metal, ";
            }
            if (Genre.HasFlag(SongGenre.Soul))
            {
                genre += "Soul, ";
            }
            // remove the trailing ", " if exists
            genre = genre.Length > 0 ? genre.Substring(0, genre.Length - 2) : genre;

            return $"{Title} by {Artist} ({genre}) {Length}min";
        }
    }

    public static class Library
    {
        private static List<Song> songs = new List<Song>();

        public static void LoadSongs(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                string title = reader.ReadLine();
                while (title != null)
                {
                    string artist = reader.ReadLine();
                    double length = double.Parse(reader.ReadLine());
                    SongGenre genre = (SongGenre)Enum.Parse(typeof(SongGenre), reader.ReadLine());
                    songs.Add(new Song(title, artist, length, genre));
                    title = reader.ReadLine();
                }
            }
        }

        public static void DisplaySongs()
        {
            foreach (var song in songs)
            {
                Console.WriteLine(song);
            }
        }

        public static void DisplaySongs(double longerThan)
        {
            foreach (var song in songs.Where(s => s.Length > longerThan))
            {
                Console.WriteLine(song);
            }
        }

        public static void DisplaySongs(SongGenre genre)
        {
            foreach (var song in songs.Where(s => s.Genre.HasFlag(genre)))
            {
                Console.WriteLine(song);
            }
        }

        public static void DisplaySongs(string artist)
        {
            foreach (var song in songs.Where(s => s.Artist == artist))
            {
                Console.WriteLine(song);
            }
        }
    }
}
