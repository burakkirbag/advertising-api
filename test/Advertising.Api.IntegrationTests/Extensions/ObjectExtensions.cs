using Advertising.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Advertising.Api.IntegrationTests.Extensions
{
    public static class ObjectExtensions
    {
        public static ByteArrayContent ToByteArrayContent(this object @object) => new ByteArrayContent(@object.ToByteArray());

        public static ByteArrayContent ToJsonStringContent(this object @object) => new StringContent(JsonConvert.SerializeObject(@object), Encoding.UTF8, "application/json");

        public static byte[] ToByteArray(this object @object)
        {
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, @object);
                return ms.ToArray();
            }
        }

        public static async Task<string> ToQueryStringParameters(this object obj)
        {
            using (HttpContent content = new FormUrlEncodedContent(obj.ToDictionary()))
                return await content.ReadAsStringAsync();
        }

        public static Dictionary<string, string> ToDictionary(this object obj)
        {
            var serializedData = obj.ToJson();
            return serializedData.ToDeserialize<Dictionary<string, string>>();
        }
    }
}