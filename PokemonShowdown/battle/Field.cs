using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonShowdown.Battle;

namespace PokemonShowdown.Battle
{
    public class Field
    {
        Mon Pokemon0 { get; set; }
        Mon Pokemon1 { get; set; }
        Mon Pokemon2 { get; set; }
        Mon Pokemon3 { get; set; }

        Player Player0 { get; set; }
        Player Player1 { get; set; }

        int FieldEffect;
    
        
    }
}
