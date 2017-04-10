import {__bridge} from '../bridge/BrowserBridge'
import {Message} from "./Message"
import EmptyPacket from "./Messages/EmptyPacket"
import UserInformation from './Messages/UserInformation';
import UserStatistics from "./Messages/UserStatistics"

export default class MessageHandler
{
    private handlers: Message[]

    constructor()
    {
        __bridge.registerMessageListener(this.messageRecieved);
        this.handlers = [];
        this.handlers[1] = new EmptyPacket(); // Unk1
        this.handlers[28] = new UserInformation();
        this.handlers[37] = new EmptyPacket(); // SettingsInformation
        this.handlers[43] = new EmptyPacket(); // PurchasedCharacters
        this.handlers[44] = new EmptyPacket(); // PurchasedHouses
        this.handlers[45] = new EmptyPacket(); // PurchasedBackgrounds
        this.handlers[46] = new EmptyPacket(); // SelectionSettings
        this.handlers[51] = new EmptyPacket(); // SetLastBonusWinTime
        this.handlers[43] = new EmptyPacket(); // PurchasedLobbyIcons
        this.handlers[54] = new EmptyPacket(); // PurchasedDeathAnimations
        this.handlers[74] = new UserStatistics();
        this.handlers[88] = new EmptyPacket(); // CurrencyMultiplier
        this.handlers[195] = new EmptyPacket(); // ActiveEvents
    }

    private messageRecieved = (object: any) =>
    {
        let handler = this.handlers[object.ID];
        if (handler != undefined)
        {
            handler.handle(object);
        }
        else
        {
            console.warn("Unable to find handler for " + object.ID + ":");
            console.warn(object);
        }
    }
}