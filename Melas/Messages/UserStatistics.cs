using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melas.Messages
{
    public class UserStatistics : ServerMessage
    {
        public override byte ID => 74;
        public int Played { get; private set; }
        public int Won { get; private set; }
        public int Drawn { get; private set; }
        public int Disconnected { get; private set; }
        public int RankedPlayed { get; private set; }
        public int RankedWon { get; private set; }
        public int RankedDrawn { get; private set; }
        public int RankedDisconnected { get; private set; }
        public int ELO { get; private set; }
        public int Referred { get; private set; }
        public int RankedPracticePlayed { get; private set; }

        public override void Deserialize(ByteReader Data)
        {
            List<int> info = Data.ReadString().Split('*').Select(Int32.Parse).ToList();
            Played = info[0];
            Won = info[1];
            Drawn = info[2];
            Disconnected = info[3];
            RankedPlayed = info[4];
            RankedWon = info[5];
            RankedDrawn = info[6];
            RankedDisconnected = info[7];
            ELO = info[8];
        }

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
