import {Message} from "../Message"

export default class EmptyPacket implements Message
{
    public handle(object: any) { }
}