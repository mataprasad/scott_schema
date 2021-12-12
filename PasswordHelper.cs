using System;
using System.Security.Cryptography;

public static class PasswordHelper
{
    private const char HashSeparator = ':';
    private const string Empty = "";

    public static string SecurePassword(string plainPassword, string salt = Empty, int prefixOrPostfix = -1,
        bool returnHashOnly = false, bool emptyPlainPasswordAllowed = false)
    {
        if (!emptyPlainPasswordAllowed &&
            (string.IsNullOrWhiteSpace(plainPassword) || string.IsNullOrEmpty(plainPassword))
        )
        {
            throw new Exception("Empty password not allowed.");
        }

        if (prefixOrPostfix == -1)
        {
            prefixOrPostfix = 0; // 0-> pre, 1-> post
        }

        if (string.IsNullOrWhiteSpace(salt) || string.IsNullOrEmpty(salt))
        {
            salt = Guid.NewGuid().ToString("N").ToLower();
        }

        var hash = ComputeHash(plainPassword, salt, prefixOrPostfix);
        if (returnHashOnly)
        {
            return hash;
        }

        var finalPassword = string.Concat(hash, HashSeparator, salt, HashSeparator, prefixOrPostfix);
        return finalPassword;
    }

    public static bool CheckSecurePassword(string plainPassword, string securePassword)
    {
        var passwordParts = securePassword.Split(HashSeparator);
        var prefixOrPostfix = 0;
        var salt = Empty;
        var hash = Empty;
        if (passwordParts.Length > 0)
        {
            hash = passwordParts[0];
        }

        if (passwordParts.Length > 1)
        {
            salt = passwordParts[1];
        }

        if (passwordParts.Length > 2)
        {
            prefixOrPostfix = Convert.ToInt32(passwordParts[2]);
        }

        var newSecurePasswordHash = SecurePassword(plainPassword, salt, prefixOrPostfix, true);
        return newSecurePasswordHash.Equals(hash);
    }

    private static string ComputeHash(string password, string salt, int prefixOrPostfix)
    {
        var subject = prefixOrPostfix == 0 ? string.Concat(salt, password) : string.Concat(password, salt);
        using var sha1 = new SHA1Managed();
        var hash = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(subject));

        return Convert.ToBase64String(hash);
    }
}