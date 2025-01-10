namespace TrackEase.Seeding;

public class Seeding
{
    public const string SeedUsername = "admin";
    public static readonly string SeedPassword;
    public const string SeedCurrency = "Npr";
    static Seeding()
    {
        SeedPassword = HashPassword("password");
    }

    private static string HashPassword(string password)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}