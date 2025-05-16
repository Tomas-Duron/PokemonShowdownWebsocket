using System;
using System.Text.Json;
using System.IO;

namespace PokemonShowdown.Utils
{
    public static class Utils
    {
        public static Dictionary<String, String> ReadJsonFile(string filePath)
        {
            Dictionary<string, string> keys = new Dictionary<string, string>();
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Json File: " + filePath + " not found");
                return keys;
            }
            else
            {
                string json = File.ReadAllText(filePath);
                keys = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            }

            return keys;
        }
    }
}