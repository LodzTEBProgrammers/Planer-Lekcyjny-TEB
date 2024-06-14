using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planer_Lekcyjny_TEB.Server.Classes;
using Planer_Lekcyjny_TEB.Server.Dataa;
using Planer_Lekcyjny_TEB.Server.Models;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Planer_Lekcyjny_TEB.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SecureWebsiteController(
    ApplicationDbContext context,
    SignInManager<User> sm,
    UserManager<User> um) : ControllerBase
{
    private readonly SignInManager<User> signInManager = sm;
    private readonly UserManager<User> userManager = um;

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(User user)
    {
        // Validate the email domain
        if (!user.Email.EndsWith("teb.edu.pl"))
            return BadRequest(new
            {
                message =
                    "Rejestracja możliwa tylko dla kont z domeną teb.edu.pl."
            });

        IdentityResult result = new();

        try
        {
            User user_ = new()
            {
                Name = user.Name,
                Email = user.Email,
                UserName = user.Email
            };

            result = await userManager.CreateAsync(user_, user.PasswordHash);

            if (!result.Succeeded)
                return BadRequest(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                message = "Coś poszło nie tak, spróbuj ponownie." + ex.Message
            });
        }

        return Ok(new
            { message = "Rejestracja zakończona sukcesem", result = result });
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginUser(Login login)
    {
        // Check if the email ends with 'teb.edu.pl'
        if (!login.Email.EndsWith("teb.edu.pl"))
            return Unauthorized(new
            {
                message =
                    "Tylko uczniowie TEB (teb.edu.pl) mają dostęp do strony."
            });

        string message = "";

        try
        {
            User user_ = await userManager.FindByEmailAsync(login.Email);

            if (user_ != null)
            {
                login.Username = user_.UserName;

                if (!user_.EmailConfirmed)
                    user_.EmailConfirmed = true;

                SignInResult? result =
                    await signInManager.PasswordSignInAsync(user_,
                        login.Password, login.Remember, false);

                if (!result.Succeeded)
                    return Unauthorized(new
                    {
                        message =
                            "Sprawdź swoje dane logowania i spróbuj ponownie"
                    });

                user_.LastLogin = DateTime.UtcNow;
                IdentityResult? updateResult =
                    await userManager.UpdateAsync(user_);
            } else
                return BadRequest(new
                {
                    message = "Sprawdź swoje dane logowania i spróbuj ponownie"
                });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                message = "Coś poszło nie tak, spróbuj ponownie." + ex.Message
            });
        }

        return Ok(new { message = "Logowanie pomyślne! :)" });
    }

    [HttpGet("logout")]
    [Authorize]
    public async Task<ActionResult> LogoutUser()
    {
        try
        {
            await signInManager.SignOutAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                message = "Coś poszło nie tak, spróbuj ponownie." + ex.Message
            });
        }

        return Ok(new { message = "Jesteś wolny :D See ya!" });
    }

    // Adding new annoucement to the database
    [HttpPost("admin/announcement")]
    public async Task<ActionResult<Announcement>> PostAnnouncement(
        Announcement announcement
    )
    {
        try
        {
            context.Announcements.Add(announcement);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log the exception message to your logging framework
            return BadRequest(new
            {
                message =
                    "An error occurred while trying to save the announcement: " +
                    ex.Message
            });
        }

        return CreatedAtAction(nameof(GetAnnouncement),
            new { id = announcement.Id }, announcement);
    }

    [HttpGet("admin/announcement/{id}")]
    public async Task<ActionResult<Announcement>> GetAnnouncement(int id)
    {
        Announcement? announcement = await context.Announcements.FindAsync(id);

        if (announcement == null) return NotFound();

        return announcement;
    }

    [HttpGet("admin/announcement")]
    public async Task<ActionResult<IEnumerable<Announcement>>>
        GetAnnouncements()
    {
        return await context.Announcements.ToListAsync();
    }

    [HttpGet("home/{email}")]
    [Authorize]
    public async Task<ActionResult> HomePage(string email)
    {
        User userInfo = await userManager.FindByEmailAsync(email);

        if (userInfo == null)
            return NotFound(new { message = "User not found." });

        return Ok(new { userInfo = userInfo });
    }

    [HttpGet("xhtlekd")]
    public async Task<ActionResult> CheckUser()
    {
        User currentUser = new();

        try
        {
            ClaimsPrincipal? user_ = HttpContext.User;
            ClaimsPrincipal? principals = new(user_);
            bool result = signInManager.IsSignedIn(principals);

            // Check if user is signed in
            if (result)
                currentUser =
                    await signInManager.UserManager.GetUserAsync(principals);
            else
                return Forbid();
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                message = "Something went wrong, please try again." + ex.Message
            });
        }

        return Ok(new { message = "Logged in", user = currentUser });
    }
}
