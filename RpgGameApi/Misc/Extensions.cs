using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace RpgGame.Misc;

public static class Extensions
{
    public static ulong GetId(this ClaimsPrincipal? user)
        => Convert.ToUInt64(user?.FindFirstValue("Id"));

    public static string GetErrors(this ModelStateDictionary modelState)
    {
        string result = string.Empty; 
        List<string> values = modelState.Values
            .SelectMany(v => v.Errors)
            .Select(v => v.ErrorMessage)
            .ToList();
        values.ForEach(v => result += v + Environment.NewLine);
        return result.TrimEnd();
    }
}
