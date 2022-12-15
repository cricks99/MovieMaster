using MovieMasterDomain;
using MovieMasterDTO;

MovieInteractor _movieInteractor = new MovieInteractor();
//InitialLoadData();  //uncomment this to load movie sample data

bool leaveFromMenu = false;

do
{
    Console.Clear();
    Console.WriteLine("Welcome to the Movie Database\n");

    Console.WriteLine("1. Search by genre");
    Console.WriteLine("2. Search by title");
    Console.WriteLine("3. Exit");

    switch (ChooseMenu())
    {
        case 1:
            Console.Write("\nEnter the genre (leave blank for all): ");
            DisplayMovieInformation(_movieInteractor.SearchByGenre(Console.ReadLine()));
            break;
        case 2:
            Console.Write("\nEnter the title (leave blank for all): ");
            DisplayMovieInformation(_movieInteractor.SearchByTitle(Console.ReadLine()));
            break;
        case 3:
        default:
            leaveFromMenu = true;
            break;
    }
}
while (!leaveFromMenu && AskSearchMoreMovies());

Console.WriteLine("\nThanks for searching the movie database.  Press any key to exit.");
Console.ReadKey();

int ChooseMenu()
{
    bool isValid = false;
    int menuOption = -1;

    while (!isValid)
    {
        Console.Write("\nPlease choose an option: ");

        try
        {
            menuOption = int.Parse(Console.ReadLine());

            if (!(isValid = menuOption > 0 && menuOption < 4))
                Console.WriteLine("Only menu choices 1 to 3 can be chosen.");
        }
        catch(FormatException)
        {
            Console.WriteLine("That's not a valid menu choice.  A menu number is expected.");
        }
    }

    return menuOption;
}

void DisplayMovieInformation(List<Movie> movies)
{
    Console.WriteLine();

    if (movies.Count == 0)
        Console.WriteLine("No movies matched your search.");
    else
    {
        string s = movies.Count > 1 ? "s were" : " was";
        Console.WriteLine($"The following movie result{s} found:\n");
        Console.WriteLine($"{"Title", -50} {"Genre", -20} {"Run Time", 3}");
        Console.WriteLine($"{"-".PadRight(50, '-')} {"-".PadRight(20, '-')} {"-".PadRight(8, '-')}");

        foreach (Movie movie in movies)
            Console.WriteLine($"{movie.Title, -50} {movie.Genre, -20} {movie.Runtime, 8}");    
    }
}

bool AskSearchMoreMovies()
{
    string userInput;

    while (true)
    {
        Console.Write("\nWould you like to search again (Y/n)? ");
        userInput = Console.ReadLine();

        if (userInput.ToLower() != "y" && userInput.ToLower() != "n" && userInput != "")
        {
            Console.WriteLine("I don't understand.  Try again.");
            continue;
        }

        return userInput.ToLower() == "y" || userInput == "";
    }
}

void InitialLoadData()
{
    foreach (Movie movie in BuildMovieCollection())
    {
        try
        {
            if (_movieInteractor.AddNewMovie(movie))
                Console.WriteLine($"{movie.Title} was added to the database.");
            else
                Console.WriteLine($"{movie.Title} was not added to the database.");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

List<Movie> BuildMovieCollection()
{
    List<Movie> movieList = new List<Movie>();
    movieList.Add(new Movie() { Title = "Gone with the Wind", Genre = "Drama", Runtime = 238 });
    movieList.Add(new Movie() { Title = "Once Upon a Time in America", Genre = "Crime", Runtime = 229 });
    movieList.Add(new Movie() { Title = "Lawrence of Arabia", Genre = "Crime", Runtime = 218 });
    movieList.Add(new Movie() { Title = "Ben-Hur", Genre = "Adventure", Runtime = 212 });
    movieList.Add(new Movie() { Title = "Seven Samurai", Genre = "Action", Runtime = 207 });
    movieList.Add(new Movie() { Title = "The Godfather Part II", Genre = "Crime", Runtime = 202 });
    movieList.Add(new Movie() { Title = "The Lord of the Rings: The Return of the King", Genre = "Adventure", Runtime = 201 });
    movieList.Add(new Movie() { Title = "Schindler's List", Genre = "Biography", Runtime = 195 });
    movieList.Add(new Movie() { Title = "Gandhi", Genre = "Biography", Runtime = 191 });
    movieList.Add(new Movie() { Title = "The Green Mile", Genre = "Drama", Runtime = 189 });
    movieList.Add(new Movie() { Title = "Barry London", Genre = "Adventure", Runtime = 185 });
    movieList.Add(new Movie() { Title = "The Deer Hunter", Genre = "Drama", Runtime = 183 });
    movieList.Add(new Movie() { Title = "Avengers: Endgame", Genre = "Action", Runtime = 181 });
    movieList.Add(new Movie() { Title = "The Wolf of Wall Street", Genre = "Biography", Runtime = 180 });
    movieList.Add(new Movie() { Title = "The Lord of the Rings: The Two Towers", Genre = "Action", Runtime = 179 });

    return movieList;
}