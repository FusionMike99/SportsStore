using System.Text.Json;

namespace SportsStore.Infrastructure
{
    public static class CookieExtensions
    {
        public static void SetJson(this IResponseCookies responseCookie, string key, object value)
        {
            responseCookie.Append(key, JsonSerializer.Serialize(value));
        }

        public static T? GetJson<T>(this IRequestCookieCollection requestCookie, string key)
        {
            var cookieData = requestCookie[key];
            return cookieData == null
                ? default
                : JsonSerializer.Deserialize<T>(cookieData);
        }
    }
}
