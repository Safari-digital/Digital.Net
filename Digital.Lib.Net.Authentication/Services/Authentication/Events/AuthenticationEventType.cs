using System.ComponentModel.DataAnnotations;

namespace Digital.Lib.Net.Authentication.Services.Authentication.Events;

public enum AuthenticationEventType
{
    Unknown,
    [Display(Name = "login")]
    Login,
    [Display(Name = "logout")]
    Logout,
    [Display(Name = "logout - all devices")]
    LogoutAll
}