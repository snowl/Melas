import {Message} from "../Message"
import Stores from "../../stores/Stores"
import User from "../../model/User"
import {plainToClass} from "class-transformer";

export default class UserInformation implements Message
{
    public handle(object: User)
    {
        // Copy the statistics into the current user if the statistics
        // packet is sent to the client before the UserInformation packet
        object.statistics = Stores.clientStore.user.statistics;
        Stores.clientStore.user = plainToClass(User, object);
        Stores.routing.push("/salem");
    }
}