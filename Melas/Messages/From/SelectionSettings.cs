using System;

namespace Melas.Messages.From
{
    public class SelectionSettings : ServerMessage
    {
        public override byte ID => 46;

        public int CharacterChoice { get; private set; }
        public int HouseChoice { get; private set; }
        public int BackgroundChoice { get; private set; }
        public int PetChoice { get; private set; }
        public int LobbyIconChoice { get; private set; }
        public int DeathAnimationChoice { get; private set; }
        public int ScrollA { get; private set; }
        public int ScrollB { get; private set; }
        public int ScrollC { get; private set; }
        public String NameChoice { get; private set; }

        public override void Deserialize(ByteReader Data)
        {
            String[] options = Data.ReadString().Split(',');
            if (options.Length < 9)
            {
                throw new Exception("Invalid number of account options send from server!");
            }

            CharacterChoice = int.Parse(options[0]);
            HouseChoice = int.Parse(options[1]);
            BackgroundChoice = int.Parse(options[2]);
            PetChoice = int.Parse(options[3]);
            LobbyIconChoice = int.Parse(options[4]);
            DeathAnimationChoice = int.Parse(options[5]);
            ScrollA = int.Parse(options[6]);
            ScrollB = int.Parse(options[7]);
            ScrollC = int.Parse(options[8]);
            NameChoice = options[9];
        }
    }
}
