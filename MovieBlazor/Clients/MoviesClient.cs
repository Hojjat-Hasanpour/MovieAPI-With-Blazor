using MovieBlazor.Models;

namespace MovieBlazor.Clients
{
    public class MoviesClient(HttpClient httpClient)
    {
        public async Task<Movie[]> GetMoviesAsync() => await httpClient.GetFromJsonAsync<Movie[]>("movies") ?? [];
        public async Task<Movie> GetMovieByIdAsync(int id) => await httpClient.GetFromJsonAsync<Movie>($"movies/{id}") ?? throw new Exception("Could not find movie");
        public async Task UpdateMovieAsync(Movie movie) => await httpClient.PutAsJsonAsync($"movies/{movie.Id}", movie);
        public async Task AddMovieAsync(Movie movie) => await httpClient.PostAsJsonAsync("movies", movie);
        public async Task DeleteMovieAsync(int id) => await httpClient.DeleteAsync($"movies/{id}");
    }
}
