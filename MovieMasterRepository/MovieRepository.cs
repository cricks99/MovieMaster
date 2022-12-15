using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieMasterDTO;

namespace MovieMasterRepository
{
    public class MovieRepository
    {
        private IConfigurationRoot _configuration;
        private DbContextOptionsBuilder<ApplicationDBContext> _optionsBuilder;

        public MovieRepository()
        {
            BuildOptions();
        }

        private void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MovieManager"));
        }

        public bool AddMovie(Movie movieToAdd)
        {
            using (ApplicationDBContext db = new ApplicationDBContext(_optionsBuilder.Options))
            {
                //determine if item exists
                Movie existingMovie = db.Movies.FirstOrDefault(x => x.Title.ToLower() == movieToAdd.Title.ToLower());

                if (existingMovie == null)
                {
                    db.Movies.Add(movieToAdd);
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public List<Movie> SearchMovieByGenre(string genre)
        {
            using (ApplicationDBContext db = new ApplicationDBContext(_optionsBuilder.Options))
            {
                return db.Movies.Where(x => x.Genre.ToLower().Contains(genre.ToLower())).ToList();
            }
        }

        public List<Movie> SearchMovieByTitle(string title)
        {
            using (ApplicationDBContext db = new ApplicationDBContext(_optionsBuilder.Options))
            {
                return db.Movies.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
            }
        }
    }
}
