using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NotificationDotNet6SignalR.Domain.Entities;

public class User : IdentityUser
{
    // Constructor
    public User()
    {
        CreatedAt = DateTime.Now;
    }


    // Properties
    [StringLength(maximumLength: 100, ErrorMessage = "O primeiro nome deve ter no máximo {0} caracteres.")]
    public string FirstName { get; private set; }

    [StringLength(maximumLength: 100, ErrorMessage = "O sobrenome deve ter no máximo {0} caracteres.")]
    public string LastName { get; private set; }

    public DateTime CreatedAt { get; }

    public DateTime? LastAccess { get; private set; }


    // Modifier
    public void SetFirstName(string firstName)
    {
        this.FirstName = firstName;
    }

    public void SetLastName(string lastName)
    {
        this.LastName = lastName;
    }

    public void SetEmailConfirmed(bool emailConfirmed)
    {
        this.EmailConfirmed = emailConfirmed;
    }

    public void SetPhoneNumber(string phoneNumber)
    {
        this.PhoneNumber = phoneNumber;
    }

    public void SetLastAccess(DateTime lastAccess)
    {
        this.LastAccess = lastAccess;
    }
}