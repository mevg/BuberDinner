using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;

namespace BuberDinner.Domain.Menu;

public sealed class Menu : AggregateRoot<MenuId>
{
    private readonly List<MenuSection> _section = new();
    private readonly List<DinnerId> _dinnersIds = new();
    private readonly List<MenuReviewId> _menuReviewsIds = new();

    public string Name { get; }
    public string Description { get; }
    public AverageRating AverageRating { get; }
    public HostId HostId { get; }
    public IReadOnlyList<MenuSection> Sections => _section.AsReadOnly();
    public IReadOnlyList<DinnerId> DinnerIds => _dinnersIds.AsReadOnly();
    public IReadOnlyList<MenuReviewId> menuReviewIds => _menuReviewsIds.AsReadOnly();

    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }
    private Menu(MenuId id, string name, string description, AverageRating averageRating, HostId hostId, DateTime createdDateTime, DateTime updatedDateTime) : base(id)
    {
        Name = name;
        Description = description;
        AverageRating = averageRating;
        HostId = hostId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Menu Create(string name, string description, AverageRating averageRating, HostId hostId, DateTime createdDateTime, DateTime updatedDateTime)
    {
        return new(
            MenuId.CreateUnique(),
            name,
            description,
            averageRating,
            hostId,
            createdDateTime,
            updatedDateTime);
    }


}
