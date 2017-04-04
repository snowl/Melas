using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melas.Structures
{
    public enum Status
    {
        Offline,
        Online,
        Active,
        Away,
        In_Game,
        In_Lobby
    }

    public class Friend
    {
        public int ID { get; private set; }
        public String Name { get; private set; }
        public Status Status { get; set; }

        public Friend(int ID, String Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }
}
