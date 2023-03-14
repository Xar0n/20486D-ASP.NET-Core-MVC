using ActorsRazorPages.Models;

namespace ActorsRazorPages.Interfaces
{
    public interface IData
    {
        List<Actor> ActorsList { get; set; }

        List<Actor> ActorsInitializeData();

        Actor GetActorById(int? id);
    }
}
