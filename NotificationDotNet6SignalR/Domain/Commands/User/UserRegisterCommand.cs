using System.ComponentModel.DataAnnotations;
using Flunt.Notifications;
using Flunt.Validations;
using NotificationDotNet6SignalR.Domain.Contracts;

namespace NotificationDotNet6SignalR.Domain.Commands.User;

public class UserRegisterCommand : Notifiable<Notification>, ICommand
{
    [Required(ErrorMessage = "O nome é obrigatório", AllowEmptyStrings = false)]
    [Display(Name = "Nome")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "O sobrenome é obrigatório", AllowEmptyStrings = false)]
    [Display(Name = "Sobrenome")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório", AllowEmptyStrings = false)]
    [StringLength(100, ErrorMessage = "O {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 5)]
    [EmailAddress(ErrorMessage = "O e-mail não é um endereço válido.")]
    [Display(Name = "E-mail")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatório.", AllowEmptyStrings = false)]
    [StringLength(20, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "O campo Confirmar Senha é obrigatório.", AllowEmptyStrings = false)]
    [DataType(DataType.Password)]
    [Display(Name = "Confirme a senha")]
    [Compare("Password", ErrorMessage = "As senhas não conferem")]
    public string? ConfirmPassword { get; set; }


    // Validations
    public void Validate()
    {
        AddNotifications(new Contract<Notification>().Requires()
            .IsNotNullOrEmpty(FirstName, "FirstName", "O nome deve ser informado.")
            .IsNotNullOrEmpty(LastName, "LastName", "O sobrenome deve ser informado.")
            .IsNotNullOrEmpty(Email, "Email", "O e-mail deve ser informado.")
            .IsNotNullOrEmpty(Password, "Password", "A senha deve ser informado.")
            .IsNotNullOrEmpty(ConfirmPassword, "ConfirmPassword", "A confirmação de senha deve ser informado.")
        );

        if (!string.IsNullOrEmpty(FirstName))
        {
            AddNotifications(new Contract<Notification>().Requires()
                .IsBetween(FirstName.Length, 3, 100, FirstName, "O nome deve ter mais de 3 e menos de 100 caracteres.")
            );
        }

        if (!string.IsNullOrEmpty(LastName))
        {
            AddNotifications(new Contract<Notification>().Requires()
                .IsBetween(LastName.Length, 3, 100, LastName, "O sobrenome deve ter mais de 3 e menos de 100 caracteres.")
            );
        }

        if (!string.IsNullOrEmpty(Email))
        {
            AddNotifications(new Contract<Notification>().Requires()
                .IsEmail(Email, "Email", "O e-mail informado parece não ser válido.")
            );
        }

        if (!string.IsNullOrEmpty(Password))
        {
            AddNotifications(new Contract<Notification>().Requires()
                .IsBetween(Password.Length, 6, 20, Password, "O senha deve ter mais de 6 e menos de 40 caracteres.")
            );
        }

        if (!string.IsNullOrEmpty(ConfirmPassword))
        {
            AddNotifications(new Contract<Notification>().Requires()
                .IsBetween(ConfirmPassword.Length, 6, 20, ConfirmPassword, "A confirmação de senha deve ter mais de 6 e menos de 40 caracteres.")
            );
        }

        if (!string.IsNullOrEmpty(Password) || !string.IsNullOrEmpty(ConfirmPassword))
        {
            AddNotifications(new Contract<Notification>().Requires()
                .AreEquals(Password, ConfirmPassword, "Senhas", "As senhas não conferem.")
            );
        }
    }
}