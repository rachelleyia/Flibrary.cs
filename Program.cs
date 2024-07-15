using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace FilmLibrary
{
    internal class Program
    {
        static List<string> moviesToWatch = new List<string>();
        static List<string> recentlyWatched = new List<string>();
        static List<string> favoriteMovies = new List<string>();

        static Dictionary<int, string> genreMap = new Dictionary<int, string>
        {
            { 1, "Action" },
            { 2, "Comedy" },
            { 3, "Drama" },
            { 4, "Horror" },
            { 5, "Science Fiction" }
        };
        class Movie
        {
            public string Title { get; set; }
            public string Synopsis { get; set; }
            public string Director { get; set; }
            public string ReleaseDate { get; set; }
            public string Duration { get; set; }
            public List<string> Casts { get; set; }
        }

        static Dictionary<string, List<Movie>> genreMovies = new Dictionary<string, List<Movie>>
        {
            {
                "Action", new List<Movie>
                {
                    new Movie { Title = "Outside the Wire", Synopsis = "In the near future, a drone pilot sent into a war zone finds himself paired with a top-secret android officer on a mission to stop a nuclear attack.", Director = "Mikael Håfström", ReleaseDate = "2021", Duration = "114 minutes", Casts = new List<string> { "Anthony Mackie", "Damson Idris", "Emily Beecham" } },
                    new Movie { Title = "The Forever Purge", Synopsis = "All the rules are broken as a sect of lawless marauders decides that the annual Purge does not stop at daybreak and instead should never end.", Director = "Everardo Gout", ReleaseDate = "2021", Duration = "103 minutes", Casts = new List<string> { "Ana de la Reguera", "Tenoch Huerta", "Josh Lucas" } },
                    new Movie { Title = "The Harder They Fall", Synopsis = "When an outlaw discovers his enemy is being released from prison, he reunites his gang to seek revenge.", Director = "Jeymes Samuel", ReleaseDate = "2021", Duration = "138 minutes", Casts = new List<string> { "Jonathan Majors", "Zazie Beetz", "Delroy Lindo" } },
                    new Movie { Title = "Blood Red Sky", Synopsis = "A woman with a mysterious illness is forced into action when a group of terrorists attempt to hijack a transatlantic overnight flight.", Director = "Peter Thorwarth", ReleaseDate = "2021", Duration = "121 minutes", Casts = new List<string> { "Peri Baumeister", "Carl Anton Koch", "Alexander Scheer" } },
                    new Movie { Title = "Space Sweepers", Synopsis = "Set in the year 2092 and follows the crew of a space junk collector ship called The Victory.", Director = "Jo Sung-hee", ReleaseDate = "2021", Duration = "136 minutes", Casts = new List<string> { "Song Joong-ki", "Kim Tae-ri", "Jin Seon-kyu" } }
                }
            },
            {
                "Comedy", new List<Movie>
                {
                    new Movie { Title = "Four Sisters and a Wedding", Synopsis = "Four sisters come together to stop the wedding of their younger brother.", Director = "Cathy Garcia-Molina", ReleaseDate = "2013", Duration = "125 minutes", Casts = new List<string> { "Bea Alonzo", "Toni Gonzaga", "Angel Locsin" } },
                    new Movie { Title = "Bakit Hindi Ka Crush ng Crush Mo?", Synopsis = "A girl hires a guy to pose as her boyfriend to make her ex-boyfriend jealous.", Director = "Joyce Bernal", ReleaseDate = "2013", Duration = "111 minutes", Casts = new List<string> { "Kim Chiu", "Xian Lim", "Ramon Bautista" } },
                    new Movie { Title = "Ang Tanging Ina", Synopsis = "A single mother juggles her dysfunctional family and tries to keep them together.", Director = "Wenn V. Deramas", ReleaseDate = "2003", Duration = "105 minutes", Casts = new List<string> { "Ai-Ai delas Alas", "Eugene Domingo", "Shaina Magdayao" } },
                    new Movie { Title = "Girl, Boy, Bakla, Tomboy", Synopsis = "Quadruplets who were separated at birth reunite and discover each other's unique personalities.", Director = "Wenn V. Deramas", ReleaseDate = "2013", Duration = "106 minutes", Casts = new List<string> { "Vice Ganda", "Maricel Soriano", "Ruffa Gutierrez" } },
                    new Movie { Title = "This Guy's in Love with U Mare!", Synopsis = "A man pretends to be gay to live with his ex-girlfriend after she discovers him living with another woman.", Director = "Wenn V. Deramas", ReleaseDate = "2012", Duration = "105 minutes", Casts = new List<string> { "Vice Ganda", "Toni Gonzaga", "Luis Manzano" } }
                }
            },
            {
                "Drama", new List<Movie>
                {
                    new Movie { Title = "Isa Pang Bahaghari", Synopsis = "A young man who lost his wife in a bus accident tries to move on while facing the challenges of single parenthood.", Director = "Joel Lamangan", ReleaseDate = "2020", Duration = "115 minutes", Casts = new List<string> { "Philip Salvador", "Nora Aunor", "Michael de Mesa" } },
                    new Movie { Title = "Death of Nintendo", Synopsis = "Four friends deal with growing up, puberty, and the complexities of life in 1990s Manila.", Director = "Raya Martin", ReleaseDate = "2020", Duration = "100 minutes", Casts = new List<string> { "Noel Comia Jr.", "Kim Chloie Oquendo", "Jigger Sementilla" } },
                    new Movie { Title = "Hinulid", Synopsis = "A story about a young man's struggle to escape his hometown and break free from his oppressive family.", Director = "Kristian Sendon Cordero", ReleaseDate = "2016", Duration = "96 minutes", Casts = new List<string> { "Julio Diaz", "Garry Cabalic", "Therese Malvar" } },
                    new Movie { Title = "Metamorphosis", Synopsis = "A teenager undergoes a transformation that challenges his family's understanding of identity and acceptance.", Director = "J.E. Tiglao", ReleaseDate = "2019", Duration = "110 minutes", Casts = new List<string> { "Gold Azeron", "Iana Bernardez", "Iyah Mina" } },
                    new Movie { Title = "Sila-Sila", Synopsis = "Two former lovers reunite and explore their complicated relationship during a gathering with old friends.", Director = "Giancarlo Abrahan", ReleaseDate = "2019", Duration = "110 minutes", Casts = new List<string> { "Gio Gahol", "Topper Fabregas", "Anna Luna" } }
                }
            },
            {
                "Horror", new List<Movie>
                {
                    new Movie { Title = "Hellcome Home", Synopsis = "A family moves into a mansion where they experience bizarre and frightening occurrences.", Director = "Bobby Bonifacio Jr.", ReleaseDate = "2019", Duration = "102 minutes", Casts = new List<string> { "Alyssa Muhlach", "Beauty Gonzalez", "Jude Rogacion" } },
                    new Movie { Title = "Spirit of the Glass 2: The Hunted", Synopsis = "Three fashionista friends who are popular on social media attempt to have some offline fun by trying to contact spirits using the age-old Spirit of the Glass game and this opens up to a series of horrifying incidents.", Director = "Jose Javier Reyes", ReleaseDate = "2017", Duration = "97 minutes", Casts = new List<string> { "Bea Alonzo", "Cristine Reyes", "Daniel Matsunaga" } },
                    new Movie { Title = "Eerie", Synopsis = "The unexpected and gruesome death of a student threatens the existence of an old Catholic school for girls. Pat Consolacion, the school guidance counselor, involves herself with the students in the hopes of helping them cope, and at the same time uncover the mysteries of the student's death.", Director = "Mikhail Red", ReleaseDate = "2019", Duration = "101 minutes", Casts = new List<string> { "Bea Alonzo", "Charo Santos-Concio", "Jake Cuenca" } },
                    new Movie { Title = "Pagpag: Siyam na Buhay", Synopsis = "A spirit of a recent dead teenager hunts down nine persons and tries to kill them after the victims ignored some superstitions during his wake.", Director = "Frasco Mortiz", ReleaseDate = "2013", Duration = "105 minutes", Casts = new List<string> { "Daniel Padilla", "Kathryn Bernardo", "Shaina Magdayao" } },
                    new Movie { Title = "The Ghost Bride", Synopsis = "To save her family from being homeless and her father suffering from a heart condition, Mayen desperately agrees to take the offer of a Chinese matchmaker for a huge amount of money. In exchange, Mayen must submit herself as a Ghost Bride to a wealthy but dead Chinese man.", Director = "Chito S. Roño", ReleaseDate = "2017", Duration = "103 minutes", Casts = new List<string> { "Kim Chiu", "Matteo Guidicelli", "Christian Bautista" } }
                }
            },
            {
                "Science Fiction", new List<Movie>
                {
                    new Movie { Title = "Clarita", Synopsis = "After learning about a child with an extraordinary ability, a streetwise reporter teams with the girl to solve a mystery.", Director = "Derrick Cabrido", ReleaseDate = "2019", Duration = "101 minutes", Casts = new List<string> { "Jodi Sta. Maria", "Alyssa Muhlach", "Arron Villaflor" } },
                    new Movie { Title = "Instalado", Synopsis = "A story of Leonardo Sandico, a brain implant developer who wanted to join an exclusive group called the Instalados. To do so, he needs to develop a program that would connect all the implants to one network called the Helix.", Director = "Jason Paul Laxamana", ReleaseDate = "2017", Duration = "105 minutes", Casts = new List<string> { "McCoy de Leon", "Jun-jun Quintana", "Francis Magundayao" } },
                    new Movie { Title = "Last Fool Show", Synopsis = "A successful, driven single mother is in love with a man who can never be hers. But when a certain journey brings them together, their lives are never the same again.", Director = "Cathy Garcia-Molina", ReleaseDate = "2019", Duration = "104 minutes", Casts = new List<string> { "Kris Aquino", "Dingdong Dantes", "J.C. de Vera" } },
                    new Movie { Title = "Block Z", Synopsis = "At a quarantined university, a disparate group of students must band together if they are going to survive during a deadly viral infection outbreak.", Director = "Mikhail Red", ReleaseDate = "2020", Duration = "101 minutes", Casts = new List<string> { "Julia Barretto", "Joshua Garcia", "Ian Veneracion" } },
                    new Movie { Title = "Paglipay", Synopsis = "A Buhay, a Negrito from Zambales, is in love with Mary Ann, a student from Manila. They try to mend their struggling relationship by having intimate journey to the hinterlands.", Director = "Zig Dulay", ReleaseDate = "2016", Duration = "115 minutes", Casts = new List<string> { "Garry Cabalic", "Anna Luna", "Miguel Longo" } }
                }
            }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Film Library System!");
            Console.Write("Enter your library number to log in: ");
            string userInput = Console.ReadLine();

            for (int i = 0; i < 3; i++)
            {
                string loginResult = Account.Login(userInput);
                if (loginResult == "Login successful")
                {
                    Menu.CreateMenus();
                    ShowMenu();
                    return;
                }
                else
                {
                    Console.WriteLine(loginResult);
                    Console.Write("Enter your library number to log in: ");
                    userInput = Console.ReadLine();
                }
            }

            Console.WriteLine("Maximum login attempts reached. Exiting application.");
        }

        static void ShowMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Menu.DisplayMenu();
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayTypesOfMovies();
                        break;
                    case "2":
                        DisplayMovies(moviesToWatch, "Movies to Watch");
                        break;
                    case "3":
                        DisplayMovies(recentlyWatched, "Recently Watched");
                        break;
                    case "4":
                        DisplayMovies(favoriteMovies, "Favorite Movies");
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        static void DisplayTypesOfMovies()
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\nTypes of Movies:");
                foreach (var genre in genreMap)
                {
                    Console.WriteLine($"{genre.Key}. {genre.Value}");
                }
                Console.WriteLine("0. Back");
                Console.WriteLine();

                Console.Write("Select a genre to view movies: ");
                if (int.TryParse(Console.ReadLine(), out int genreChoice))
                {
                    if (genreChoice == 0)
                    {
                        back = true;
                    }
                    else if (genreMap.ContainsKey(genreChoice))
                    {
                        DisplayMovies(genreMovies[genreMap[genreChoice]]);
                    }
                    else
                    {
                        Console.WriteLine("Invalid genre, please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, please try again.");
                }
            }
        }

        static void DisplayMovies(List<string> movies, string category)
        {
            Console.WriteLine($"\n{category}:");
            foreach (var movie in movies)
            {
                Console.WriteLine($"- {movie}");
            }
            Console.WriteLine();
        }

        static void DisplayMovies(List<Movie> movies)
        {
            Console.WriteLine("\nMovies:");
            for (int i = 0; i < movies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {movies[i].Title}");
            }
            Console.WriteLine();

            Console.Write("Select a movie to view details or press 0 to go back: ");
            if (int.TryParse(Console.ReadLine(), out int movieIndex) && movieIndex == 0)
            {
                return;
            }
            else if (movieIndex > 0 && movieIndex <= movies.Count)
            {
                DisplayMovieDetails(movies[movieIndex - 1]);
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }

        static void DisplayMovieDetails(Movie movie)
        {
            Console.WriteLine($"\nTitle: {movie.Title}");
            Console.WriteLine($"Synopsis: {movie.Synopsis}");
            Console.WriteLine($"Director: {movie.Director}");
            Console.WriteLine($"Release Date: {movie.ReleaseDate}");
            Console.WriteLine($"Duration: {movie.Duration}");
            Console.WriteLine("Casts: " + string.Join(", ", movie.Casts));

            Console.WriteLine();

            Console.WriteLine("Options:");

            Console.WriteLine("1. Add to Movies to Watch");
            Console.WriteLine("2. Add to Recently Watched");
            Console.WriteLine("3. Add to Favorite Movies");
            Console.WriteLine("0. Back");

            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddMovieToList(moviesToWatch, movie.Title, "Movies to Watch");
                    break;
                case "2":
                    AddMovieToList(recentlyWatched, movie.Title, "Recently Watched");
                    break;
                case "3":
                    AddMovieToList(favoriteMovies, movie.Title, "Favorite Movies");
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        static void AddMovieToList(List<string> list, string movieTitle, string listName)
        {
            if (!list.Contains(movieTitle))
            {
                list.Add(movieTitle);
                Console.WriteLine($"{movieTitle} has been added to {listName}.");
            }
            else
            {
                Console.WriteLine($"{movieTitle} is already in {listName}.");
            }
        }
    }
}