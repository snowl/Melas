import {Message} from "../Message"
import Stores from "../../stores/Stores"
import {UserStats} from "../../model/User"
import {plainToClass} from "class-transformer";

export default class UserStatistics implements Message
{
    public handle(object: UserStats)
    {
        Stores.clientStore.user.statistics = plainToClass(UserStats, object);
    }
}