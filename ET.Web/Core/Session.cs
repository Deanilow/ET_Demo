using Newtonsoft.Json;

namespace ET.Web.Core
{
    public static class Session
    {
        public static async Task<T> GetData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(data));
        }

        public static async Task SetData(this ISession session, string key, object value)
        {
            session.SetString(key, await Task.Factory.StartNew(() => JsonConvert.SerializeObject(value)));
        }
    }
}
