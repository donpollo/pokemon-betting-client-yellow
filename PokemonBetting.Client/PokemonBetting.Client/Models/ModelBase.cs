using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using PokemonBetting.Client.Helpers;

namespace PokemonBetting.Client.Models
{
    public class ModelBase<T>
    {
        //Use this to convert a user to json and send it to the server
        public StringContent ToJson()
        {
            var json = LowerCaseSerializer.SerializeObject(this);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public static T FromJson(string json)
        {
            return LowerCaseSerializer.DeserializeObject<T>(json);
        }

        public static List<T> FromJsonList(string json)
        {
            return LowerCaseSerializer.DeserializeObject<List<T>>(json);
        }
    }
}