import {observable, action} from 'mobx'

export default class ClientStore
{
    @observable
    public loggedIn: boolean;
}