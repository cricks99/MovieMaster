using MovieMasterDTO;
using MovieMasterRepository;

namespace MovieMasterDomain
{
    public class MovieInteractor
    {
        private MovieRepository _repo;

        public MovieInteractor()
        {
            _repo = new MovieRepository();
        }

        public bool AddNewMovie(Movie movieToAdd)
        {
            if (string.IsNullOrEmpty(movieToAdd.Title)
                || string.IsNullOrEmpty(movieToAdd.Genre)
                || movieToAdd.Runtime == 0)
            {
                throw new ArgumentException("Move Title, genre, and run time must contain valid data.");
            }

            return _repo.AddMovie(movieToAdd);
        }

        public List<Movie> SearchByGenre(string genre)
        {
            return _repo.SearchMovieByGenre(genre);
        }

        public List<Movie> SearchByTitle(string title)
        {
            return _repo.SearchMovieByTitle(title);
        }
    }
}