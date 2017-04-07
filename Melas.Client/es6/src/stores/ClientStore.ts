import {__bridge} from '../bridge/BrowserBridge'
import {observable, action} from 'mobx'

export default class ClientStore
{
    @observable
    public loggedIn: boolean;

    constructor()
    {
        __bridge.registerMessageListener(this.messageRecieved);
    }

    private messageRecieved(object: string)
    {
        console.log(object);
    }
}