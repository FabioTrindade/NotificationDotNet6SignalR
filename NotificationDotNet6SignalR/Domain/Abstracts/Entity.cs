using System.ComponentModel.DataAnnotations;

namespace NotificationDotNet6SignalR.Domain.Abstracts;

public abstract record Entity
{
    // Constructor
    public Entity()
    {
        Active = true;
    }


    // Properties
    [Key]
    public Guid Id { get; init;  }

    public DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; private set; }

    public bool Active { get; private set; }


    // Modifier
    public void SetUpdatedAt(DateTime? updatedAt)
    {
        this.UpdatedAt = updatedAt;
    }

    public void SetActive(bool active)
    {
        this.Active = active;
    }
}