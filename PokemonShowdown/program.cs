using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using PokemonShowdown;
using PokemonShowdown.ShowdownClient;

namespace PokemonShowdown
{
    internal class program
    {
        static async Task Main(string[] args)
        {
            PSClient client = new PSClient(true);
            await client.Login();
        }
    }
}
