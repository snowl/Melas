using System;

namespace Melas.Messages
{
    public class Packets
    {
        public static Type[] List = new Type[199];

        static Packets()
        {
            List[1] = typeof(Unk1);
            List[20] = typeof(FriendList);
            List[28] = typeof(UserInformation);
            List[37] = typeof(SettingsInformation);
            List[43] = typeof(PurchasedCharacters);
            List[44] = typeof(PurchasedHouses);
            List[45] = typeof(PurchasedBackgrounds);
            List[46] = typeof(SelectionSettings);
            List[51] = typeof(SetLastBonusWinTime);
            List[52] = typeof(EarnedAchievements);
            List[53] = typeof(PurchasedLobbyIcons);
            List[54] = typeof(PurchasedDeathAnimations);
            List[74] = typeof(UserStatistics);
            List[75] = typeof(RankedTimeoutDuration);
            List[88] = typeof(CurrencyMultiplier);
            List[195] = typeof(ActiveEvents);
            List[196] = typeof(CauldronStatus);
        }
    }
}
