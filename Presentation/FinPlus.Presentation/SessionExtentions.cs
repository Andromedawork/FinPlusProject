namespace FinPlus.Presentation
{
    using System.Text.Json;

    public static class SessionExtentions
    {
        public static void Set<T>(ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? Get<T>(ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
