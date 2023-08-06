using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using JWT.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Helper.Extensions;

public static class HelperExtensions
{
    public static string HashingWithKey(string source, string key)
    {
        HMACSHA256 hmac = new HMACSHA256(Encoding.ASCII.GetBytes(key));
        var hashResult = hmac.ComputeHash(Encoding.ASCII.GetBytes(source));
        return Convert.ToBase64String(hashResult);
        //return Encoding.UTF8.GetString(hashResult);
    }

    public static string GetRandomKey(int length)
    {
        var seedStr = "0123456789abcdefghijklmnopqrstuvwxyz";
        string result = String.Empty;
        for (int i = 1; i <= length; i++)
        {
            result += seedStr[new Random().Next(seedStr.Length)];
        }

        return result;
    }
}