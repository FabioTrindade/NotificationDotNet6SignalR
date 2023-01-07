using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotificationDotNet6SignalR.Domain.Commands;
using NotificationDotNet6SignalR.Domain.Commands.User;
using NotificationDotNet6SignalR.Domain.Entities;
using NotificationDotNet6SignalR.Domain.Providers;
using NotificationDotNet6SignalR.Domain.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NotificationDotNet6SignalR.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserConnectionManagerProvider _userConnectionManagerProvider;
 
        public UserService(UserManager<User> userManager
            , SignInManager<User> signInManager
            , IHttpContextAccessor httpContextAccessor
            , IUserConnectionManagerProvider userConnectionManagerProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userConnectionManagerProvider = userConnectionManagerProvider;
        }

        public async Task<GenericCommandResult> Handle(UserRegisterCommand command)
        {
            // Executa as validacoes
            command.Validate();

            // Utilizado para retornar as validações caso aponte algo
            if (!command.IsValid)
                return new GenericCommandResult(false, "", command.Notifications);

            // Verifica se o e-mail esta em uso
            var isEmailInUse = await _userManager.FindByEmailAsync(command.Email);

            // Se e-mail existe adiciona uma notificação e devolve
            if (isEmailInUse != null)
            {
                command.AddNotification("Email", $"O e-mail {command.Email} já está sendo usado.");
                return new GenericCommandResult(false, "", command.Notifications);
            }

            // Preenche a classe IdentityUser
            var user = new User(
                firstName: command.FirstName,
                lastName: command.LastName,
                userName: command.Email,
                email: command.Email
            );

            // Pra criar o user com e-mail confirmado
            user.EmailConfirmed = true;

            // Cria usuário na tabela AspNetUsers
            var userCreate = await _userManager.CreateAsync(user, command.Password);

            // Caso apresente algum erro na criação do usuário adiciona as notificações e devolve
            if (!userCreate.Succeeded)
            {
                userCreate.Errors.ToList().ForEach(erro =>
                {
                    command.AddNotification("", erro.Description);
                });

                return new GenericCommandResult(false, "", command.Notifications);
            }

            return new GenericCommandResult(true, "", userCreate);
        }

        public async Task<GenericCommandResult> Handle(UserLoginCommand command)
        {
            // Executa as validacoes
            command.Validate();

            // Utilizado para retornar as validações caso aponte algo
            if (!command.IsValid)
                return new GenericCommandResult(false, "", command.Notifications);

            // Verifica se o e-mail existe
            var signedUser = await _userManager.FindByEmailAsync(command.Email);

            // Caso não existe adicionar uma notificação e devolve
            if (signedUser == null)
            {
                command.AddNotification("", "E-mail e/ou senha inválida.");
                return new GenericCommandResult(false, "", command.Notifications);
            }

            // Verifica se o e-mail foi confirmado
            if (!await _userManager.IsEmailConfirmedAsync(signedUser))
            {
                command.AddNotification("", "Verifique sua caixa de e-mail e confirme o cadastro.");
                return new GenericCommandResult(false, "", command.Notifications);
            }

            // Verifica se o usuário esta bloqueado
            if (await _userManager.IsLockedOutAsync(signedUser))
            {
                command.AddNotification("", "Conta bloqueada, entre em contato com suporte.");
                return new GenericCommandResult(false, "", command.Notifications);
            }

            // Realiza o login e autentica no identity
            SignInResult login = await _signInManager.PasswordSignInAsync(signedUser, command.Password, false, lockoutOnFailure: false);

            if (!login.Succeeded)
            {
                command.AddNotification("", "E-mail e/ou senha inválida.");
                return new GenericCommandResult(false, "", command.Notifications);
            }

            // Register last access in application
            signedUser.SetLastAccess(DateTime.Now);

            // Set active for visible especific notifcation
            signedUser.SetActive(true);

            // Salva as alterações realizadas
            await _userManager.UpdateAsync(signedUser);

            return new GenericCommandResult(true, "", login);
        }

        public async Task<GenericCommandResult> GetUserActive()
        {
            var user = await _userConnectionManagerProvider.LogCurrentUser();

            var users = await _userManager.Users
                                .Where(w => w.Active == true && w.Id != user.Id)
                                .AsNoTracking()
                                .ToListAsync();

            var result = users.ConvertAll(c => new SelectListItem(
                text: string.Format("{0} {1}", c.FirstName, c.LastName),
                value: c.Id.ToString()));

            return new GenericCommandResult(true, "", result);
        }

        public async Task Logout()
        {
            // User logado
            var user = await _userConnectionManagerProvider.LogCurrentUser();

            // Verifica se o e-mail existe
            var signedUser = await _userManager.FindByIdAsync(user.Id.ToString());

            // Set inactive for visible especific notifcation
            signedUser.SetActive(false);

            // Salva as alterações realizadas
            await _userManager.UpdateAsync(signedUser);

            // Realiza o logout da aplicação
            await _signInManager.SignOutAsync();
        }
    }
}