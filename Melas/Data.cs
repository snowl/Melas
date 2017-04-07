namespace Melas
{
    public enum GameMode
    {
        Classic = 1,
        Custom = 2,
        All_Any = 3,
        Rapid = 4,
        Vigilantics = 5,
        Ranked = 6,
        Rainbow = 7,
        Ranked_Practice = 8
    }

    public enum AFKStatus
    {
        Online,
        AFK,
        DCDueToInactivity
    }
    
    public enum InviteAction
    {
        Deny = 1,
        Accept = 3
    }

    public enum Server
    {
        Live,
        PTR,
        Testing
    }

    public enum Roles
    {
        Bodyguard,
        Doctor,
        Escort,
        Investigator,
        Jailor,
        Lookout,
        Mayor,
        Medium,
        Retributionist,
        Sheriff,
        Spy,
        Transporter,
        Vampire_Hunter,
        Veteran,
        Vigilante,
        Blackmailer,
        Consigliere,
        Consort,
        Disguiser,
        Forger,
        Framer,
        Godfather,
        Janitor,
        Mafioso,
        Amnesiac,
        Arsonist,
        Executioner,
        Jester,
        Serial_Killer,
        Survivor,
        Vampire,
        Werewolf,
        Witch
    }
}
