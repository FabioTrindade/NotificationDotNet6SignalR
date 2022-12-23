using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NotificationDotNet6SignalR.Domain.Entities;

public class User : IdentityUser<Guid>
{
    // Constructor
    public User()
    {
        CreatedAt = DateTime.Now;
    }

    public User(
        string firstName,
        string lastName,
        string userName,
        string email)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Email = email;
    }


    // Properties
    [StringLength(maximumLength: 100, ErrorMessage = "O primeiro nome deve ter no máximo {0} caracteres.")]
    public string FirstName { get; private set; }

    [StringLength(maximumLength: 100, ErrorMessage = "O sobrenome deve ter no máximo {0} caracteres.")]
    public string LastName { get; private set; }    

    public DateTime CreatedAt { get; init; }

    public DateTime? LastAccess { get; private set; }


    // Relationship
    public virtual ICollection<Notification> NotificationsFrom { get; private set; }

    public virtual ICollection<Notification> NotificationsTo { get; private set; }


    // Modifier
    public void SetFirstName(string firstName) => this.FirstName = firstName;
    
    public void SetLastName(string lastName) => this.LastName = lastName;
    
    public void SetEmailConfirmed(bool emailConfirmed) => this.EmailConfirmed = emailConfirmed;
    
    public void SetPhoneNumber(string phoneNumber) => this.PhoneNumber = phoneNumber;

    public void SetLastAccess(DateTime lastAccess) => this.LastAccess = lastAccess;
}