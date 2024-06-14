using Microsoft.AspNetCore.Identity;

namespace Planer_Lekcyjny_TEB.Server.CustomIdentityErrorDescriber;

public class CustomIdentityErrorDescriber : IdentityErrorDescriber
{
    // Długość hasła - min 6 znaków
    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description =
                $"Hasło musi składać się z co najmniej {length} znaków."
        };
    }

    // Min jeden znak specjalny
    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description =
                "Hasło musi zawierać przynajmniej jeden znak specjalny."
        };
    }

    // Min jedna duża litera
    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description =
                "Hasło musi zawierać przynajmniej jedną wielką literę ('A'-'Z')."
        };
    }

    // Min jedna mała litera
    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresLower),
            Description =
                "Hasło musi zawierać przynajmniej jedną małą literę ('a'-'z')."
        };
    }

    // Min jedna cyfra
    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description =
                "Hasło musi zawierać przynajmniej jedną cyfrę ('0'-'9')."
        };
    }

    // Nazwa uzytkownika jest juz zajeta 
    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description =
                "Nie można zakończyć rejestracji. Sprawdź wprowadzone dane."
        };
    }

    // Email jest juz zajety
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description =
                $"Email '{email}' jest już zajęty."
        };
    }

    // Dodaj więcej metod dla innych komunikatów, które chcesz dostosować
}
