using Application.Interfaces;

namespace Infrastructure.Services;

public class CollectionDiffer : ICollectionDiffer
{
    public (IReadOnlyList<int> ToAdd, IReadOnlyList<int> ToRemove) GetChanges(
        IEnumerable<int> existingIds,
        IEnumerable<int> newIds)
    {
        var existing = existingIds.ToList();
        var incoming = newIds.ToList();
        return (incoming.Except(existing).ToList(), existing.Except(incoming).ToList());
    }
}
