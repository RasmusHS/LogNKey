namespace PassGenDomain.PasswordDomainService;

public class RandomPasswordGenerator
{
    public static string Generate(PasswordOptions options)
    {
        const string lowercaseChars = "bcdfghjkmnpqrstvwxyz"; // no aeiou
        const string uppercaseChars = "BCDFGHJKLMNPQRSTVWXYZ"; // no AEIOU
        const string numberChars = "23456789"; // no 01
        const string specialChars = "!@#$%^&*";

        var allChars = "";

        var positions = new List<char>();

        if (options.UseLowercase)
        {
            allChars += lowercaseChars;

            if (options.MinLowercase > 0)
            {
                for (var i = 0; i < options.MinLowercase; i++)
                {
                    positions.Add('l');
                }
            }
        }

        if (options.UseUppercase)
        {
            allChars += uppercaseChars;

            if (options.MinUppercase > 0)
            {
                for (var i = 0; i < options.MinUppercase; i++)
                {
                    positions.Add('u');
                }
            }
        }

        if (options.UseNumbers)
        {
            allChars += numberChars;

            if (options.MinNumbers > 0)
            {
                for (var i = 0; i < options.MinNumbers; i++)
                {
                    positions.Add('n');
                }
            }
        }

        if (options.UseSpecial)
        {
            allChars += specialChars;

            if (options.MinSpecial > 0)
            {
                for (var i = 0; i < options.MinSpecial; i++)
                {
                    positions.Add('s');
                }
            }
        }

        while (positions.Count < options.Length)
        {
            positions.Add('a');
        }

        var rnd = new CryptoRandom();

        ShuffleList(rnd, positions);

        var password = "";

        for (var i = 0; i < positions.Count; i++)
        {
            string positionChars = null;
            switch (positions[i])
            {
                case 'l':
                    positionChars = lowercaseChars;
                    break;

                case 'u':
                    positionChars = uppercaseChars;
                    break;

                case 'n':
                    positionChars = numberChars;
                    break;

                case 's':
                    positionChars = specialChars;
                    break;

                case 'a':
                    positionChars = allChars;
                    break;

                default:
                    break;
            }

            var randomCharIndex = rnd.Next(0, positionChars.Length - 1);
            var randomChar = positionChars[randomCharIndex];

            // no consecutive characters in a row
            if (i > 0 && password[i - 1] == randomChar)
            {
                i--;
                continue;
            }

            password += randomChar;
        }

        return password;
    }

    private static void ShuffleList<T>(Random rnd, List<T> list)
    {
        var length = list.Count - 1;
        while (length > 0)
        {
            var i = rnd.Next(0, length);

            (list[i], list[length]) = (list[length], list[i]);

            length--;
        }
    }
}