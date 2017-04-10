import MessageHandler from '../bridge/MessageHandler'
import User, {UserStats} from "../model/User"
import {observable} from 'mobx'

export default class ClientStore
{
    @observable
    public loggedIn: boolean;

    @observable
    public user: User;

    private handler: MessageHandler;

    constructor()
    {
        this.handler = new MessageHandler();
        this.user = new User();
    }
}