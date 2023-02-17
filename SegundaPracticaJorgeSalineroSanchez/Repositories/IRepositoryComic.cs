using SegundaPracticaJorgeSalineroSanchez.Models;

namespace SegundaPracticaJorgeSalineroSanchez.Repositories
{
    public interface IRepositoryComic
    {
        public List<Comic> GetComics();
        public void InsertarComic(Comic comic);
    }
}
