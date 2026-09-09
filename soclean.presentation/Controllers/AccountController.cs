using Microsoft.AspNetCore.Mvc;
using soclean.business.Dtos.Account;
using soclean.business.Services.Abstract;

namespace soclean.presentation.Controllers;
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return View();

        var result = await _accountService.RegisterUserAsync(registerDto);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }

        var user = await _accountService.FindUserByEmailAsync(registerDto.Email);
        var token = await _accountService.GenerateEmailConfirmationTokenAsync(user);

        var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme);
        await _accountService.SendEmailAsync(
       user.Email,
       "Confirm Your Email",
       $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
</head>
<body style='margin:0;padding:0;background:#f4f6f9;font-family:Arial,sans-serif;'>

<table width='100%' cellpadding='0' cellspacing='0' style='padding:40px 0;'>
<tr>
<td align='center'>

<table width='600' cellpadding='0' cellspacing='0'
       style='background:#ffffff;border-radius:12px;overflow:hidden;box-shadow:0 5px 20px rgba(0,0,0,.1);'>

<tr>
<td style='background:#28a745;color:white;padding:25px;text-align:center;'>
    <h1 style='margin:0;'>SoClean</h1>
</td>
</tr>

<tr>
<td style='padding:40px;'>

<h2 style='color:#333;'>Welcome, {user.FullName}! 👋</h2>

<p style='font-size:16px;color:#555;line-height:1.7;'>
Thank you for creating your SoClean account.
To activate your account, please confirm your email address by clicking the button below.
</p>

<div style='text-align:center;margin:35px 0;'>
<a href='{confirmationLink}'
style='background:#28a745;
color:#fff;
padding:15px 35px;
text-decoration:none;
font-size:16px;
border-radius:8px;
display:inline-block;'>
Confirm Email
</a>
</div>

<p style='color:#777;font-size:14px;'>
If the button doesn't work, copy and paste the following link into your browser:
</p>

<p style='word-break:break-all;font-size:13px;color:#0d6efd;'>
{confirmationLink}
</p>

<hr style='margin:30px 0;'>

<p style='font-size:13px;color:#888;text-align:center;'>
This email was sent automatically by <strong>SoClean</strong>.<br>
If you did not create this account, you can safely ignore this email.
</p>

</td>
</tr>

</table>

</td>
</tr>
</table>

</body>
</html>"
   );
        return RedirectToAction("Login");
    }

    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (userId == null || token == null)
            return BadRequest("Invalid email confirmation request.");

        var user = await _accountService.FindUserByIdAsync(userId);
        if (user == null)
            return NotFound("User not found.");

        var result = await _accountService.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
            return View("ConfirmEmail");

        return BadRequest("Email confirmation failed.");
    }

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return View();

        var result = await _accountService.LoginUserAsync(loginDto);
        //if (!result.Succeeded)
        //{
        //    var userLogin = await _accountService.FindUserByEmailAsync(loginDto.Email);
        //    if (userLogin != null && userLogin.IsDisabled)
        //    {
        //        ModelState.AddModelError("", "Your account has been disabled. Please contact support.");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Username or password is incorrect.");
        //    }
        //    return View();
        //}

        var user = await _accountService.FindUserByEmailAsync(loginDto.Email);
        var redirectUrl = await _accountService.GetRedirectUrlAfterLogin(user);

        return Redirect(redirectUrl);
    }

    public async Task<IActionResult> Logout()
    {
        await _accountService.LogoutUserAsync();
        return RedirectToAction("Login", "Account");
    }

    //[HttpGet]
    //public IActionResult LoginWithGoogle()
    //{
    //    var redirectUrl = Url.Action("GoogleResponse", "Account");
    //    var properties = _accountService.GetGoogleLoginProperties(redirectUrl);
    //    return Challenge(properties, "Google");
    //}

    public async Task<IActionResult> GoogleResponse()
    {
        var info = await _accountService.GetExternalLoginInfoAsync();
        if (info == null)
            return RedirectToAction("Login", new { error = "Google login failed." });

        var user = await _accountService.HandleGoogleLoginAsync(info);
        if (user == null)
            return RedirectToAction("Login", new { error = "Google registration failed." });

        return RedirectToAction("Index", "Home");
    }

    public IActionResult ForgotPassword() => View();

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
        if (!ModelState.IsValid)
            return View();

        var user = await _accountService.FindUserByEmailAsync(forgotPasswordDto.Email);

        var token = await _accountService.GeneratePasswordResetTokenAsync(user);

        var resetLink = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

        await _accountService.SendEmailAsync(user.Email, "Password Reset",
            $"Click <a href='{resetLink}'>here</a> to reset your password.");


        return RedirectToAction("Login");
    }

    public IActionResult ResetPassword(string token, string email)
    {
        if (token == null || email == null)
            return BadRequest("Invalid password reset request.");

        var model = new ResetPasswordDto { Token = token, Email = email };
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        if (!ModelState.IsValid)
            return View(resetPasswordDto);

        var user = await _accountService.FindUser();

        var result = await _accountService.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(resetPasswordDto);
        }

        return RedirectToAction("Login");
    }


    //[HttpGet]
    //public async Task<IActionResult> EditProfile()
    //{
    //    var user = await _accountService.FindUser();

    //    var model = new EditProfileDto
    //    {
    //        UserName = user.UserName,
    //        Bio = user.Biography,
    //        PhoneNumber = user.PhoneNumber,
    //        IsPrivate = user.IsPrivate
    //    };

    //    return View(model);
    //}

    //[HttpPost]
    //public async Task<IActionResult> EditProfile(EditProfileDto editProfileDto)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return View(editProfileDto);
    //    }

    //    var user = await _accountService.FindUser();

    //    var success = await _accountService.EditProfileAsync(user, editProfileDto);

    //    return RedirectToAction("Index", "Profile");
    //}


    [HttpGet]
    public async Task<IActionResult> ChangePassword()
    {
        var user = await _accountService.FindUser();
        if (user == null)
        {
            return RedirectToAction("Login");
        }

        var hasPassword = await _accountService.UserHasPasswordAsync(user);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _accountService.FindUser();
        var result = await _accountService.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

        return RedirectToAction("Index", "Home");

    }



}