using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using soclean.business.Dtos.Account;
using soclean.business.Exceptions;
using soclean.business.Services.Abstract;
using soclean.core.Entities;
using System.Security.Claims;

namespace soclean.business.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IEmailService _emailService;
    private readonly ICloudManager _cloudinaryManager;
    private readonly IHttpContextAccessor _http;

    public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService, ICloudManager cloudinaryManager, IHttpContextAccessor http)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _cloudinaryManager = cloudinaryManager;
        _http = http;
    }


    public async Task<string> GetRedirectUrlAfterLogin(AppUser user)
    {
        if (await _userManager.IsInRoleAsync(user, "Admin"))
        {
            return "/Admin/Dashboard";
        }

        return "/Home/Index";
    }

    public async Task<AppUser> FindUser()
    {
        var userid = _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userid is null)
        {
            throw new NotFoundException("user not found");

        }
        var user = await FindUserByIdAsync(userid);
        if (user == null)
        {
            throw new NotFoundException("user not found");
        }
        return user;
    }

    public async Task<bool> UserHasPasswordAsync(AppUser user)
    {
        return await _userManager.HasPasswordAsync(user);
    }
    public async Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto)
    {
      
        var user = new AppUser
        {
            UserName = registerDto.UserName,
             FullName= registerDto.FullName,
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber,
            //CreatedTime = DateTime.UtcNow,
            //UpdateTime = DateTime.UtcNow,
            EmailConfirmed = false,
  
        };

        return await _userManager.CreateAsync(user, registerDto.Password);
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user) =>
        await _userManager.GenerateEmailConfirmationTokenAsync(user);

    public async Task<AppUser> FindUserByEmailAsync(string email)
    {
        if (email == null)
        {
            throw new NotFoundException("not found");
        }

        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            throw new NotFoundException("not found");
        }

        return user;
    }

    public async Task<AppUser> FindUserByIdAsync(string userId)
    {
        if (userId == null)
        {
            throw new NotFoundException("not found");
        }
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            throw new NotFoundException("not found");

        }
        return user;
    }

    public async Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token) =>
        await _userManager.ConfirmEmailAsync(user, token);

    public async Task<SignInResult> LoginUserAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null || !user.EmailConfirmed/* || user.IsDisabled == true*/)
            return SignInResult.Failed;

        return await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, true);
    }

    public async Task LogoutUserAsync() =>
        await _signInManager.SignOutAsync();

    public AuthenticationProperties GetGoogleLoginProperties(string redirectUrl)
    {
        return _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
    }

    public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync() =>
        await _signInManager.GetExternalLoginInfoAsync();

    public string GetId()
    {
        var userid = _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userid is null)
        {
            throw new NotFoundException("user not found");
        }
        return userid;

    }
    public async Task<AppUser> HandleGoogleLoginAsync(ExternalLoginInfo info)
    {
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        if (email is null)
        {
            throw new SignInException();
        }
        var userName = info.Principal.FindFirstValue(ClaimTypes.Name);

        if (!string.IsNullOrEmpty(userName))
        {
            userName = userName.Replace(" ", "_");
        }

        var existingUser = await _userManager.FindByEmailAsync(email);

        //if (existingUser != null)
        //{
        //    if (existingUser.IsDisabled == false)
        //    {
        //        await _signInManager.SignInAsync(existingUser, isPersistent: false);
        //        return existingUser;
        //    }
        //    else
        //    {
        //        throw new SignInException("Your account has been disabled. Please contact support.");
        //    }

        //}

        var newUser = new AppUser
        {
            UserName = userName ?? $"google_{Guid.NewGuid()}",
            //NickName = userName ?? $"google_{Guid.NewGuid()}",
            Email = email,
            //CreatedTime = DateTime.UtcNow,
            //UpdateTime = DateTime.UtcNow,
            EmailConfirmed = true,
            //ProfilePhotoUrl = info.Principal.FindFirstValue("picture") ?? "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png"
        };

        var createResult = await _userManager.CreateAsync(newUser);
        if (!createResult.Succeeded) return null;

        await _userManager.AddLoginAsync(newUser, info);

        await _signInManager.SignInAsync(newUser, isPersistent: false);

        return newUser;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        _emailService.SendEmail(to, subject, body);
    }
    public async Task<string> GeneratePasswordResetTokenAsync(AppUser user) =>
    await _userManager.GeneratePasswordResetTokenAsync(user);

    public async Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword) =>
        await _userManager.ResetPasswordAsync(user, token, newPassword);

    

    public async Task<IdentityResult> ChangePasswordAsync(AppUser user, string oldPassword, string newPassword)
    {
        var hasPassword = await UserHasPasswordAsync(user);
        if (hasPassword)
        {
            if (string.IsNullOrEmpty(oldPassword))
            {
                throw new NullException("Old password is required.");
            }

            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }
        else
        {

            return await _userManager.AddPasswordAsync(user, newPassword);
        }
    }
}