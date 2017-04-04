using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melas.Messages
{
    public class SettingsInformation : ServerMessage
    {
        public override byte ID => 37;
        public bool FilterChat { get; private set; }
        public bool MuteMusic { get; private set; }
        public bool MuteEffects { get; private set; }

        public bool DisplaySkinsToTown { get; private set; }
        public bool ClassicSkinsOnly { get; private set; }
        public bool DisplayPets { get; private set; }
        public byte SelectedQueue { get; private set; }
        public byte TipBehaviour { get; private set; }

        public override void Deserialize(ByteReader Data)
        {
            FilterChat = Data.ReadBoolean();
            MuteMusic = Data.ReadBoolean();
            MuteEffects = Data.ReadBoolean();
            DisplaySkinsToTown = Data.ReadBoolean();
            ClassicSkinsOnly = Data.ReadBoolean();
            DisplayPets = Data.ReadBoolean();
            Data.ReadByte(); // Music Volume
            Data.ReadByte(); // Effects Volume
            SelectedQueue = Data.ReadByte();
            Data.ReadByte(); // Unused
            TipBehaviour = Data.ReadByte();
            Data.ReadByte(); // 00
        }

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
