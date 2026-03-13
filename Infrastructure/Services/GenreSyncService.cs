using Application.Interfaces;
using Core.Interfaces;

namespace Infrastructure.Services;

public class GenreSyncService : IGenreSyncService
{
    private readonly ICollectionDiffer _collectionDiffer;

    public GenreSyncService(ICollectionDiffer collectionDiffer)
    {
        _collectionDiffer = collectionDiffer;
    }

    public void Sync<T>(ICollection<T> collection, IEnumerable<int> newGenreIds)
        where T : class, IGenreJoin, new()
    {
        var (toAdd, toRemove) = _collectionDiffer.GetChanges(
            collection.Select(x => x.GenreId),
            newGenreIds);

        foreach (var id in toRemove)
            collection.Remove(collection.First(x => x.GenreId == id));

        foreach (var id in toAdd)
            collection.Add(new T { GenreId = id });
    }
}
