using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    // Base class for different types of tiles
    public abstract class Tile
    {
        public string Name { get; private set; }

        public Tile(string name)
        {
            Name = name;
        }

        // Virtual method for performing actions specific to each tile
        public virtual void PerformAction(Player player)
        {
        }

        // Virtual method to display tile
        public virtual void Display()
        {
            Console.Write("| " + Name + " |" + "-->");

        }

    }
}
