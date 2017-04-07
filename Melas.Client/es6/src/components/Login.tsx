import * as React from 'react'
import Bridge, {LoginResult} from '../bridge/BrowserBridge'
import {inject, observer} from 'mobx-react'
import {observable} from 'mobx'

@inject("routing", "clientStore")
@observer
export default class Login extends React.Component<any, any>
{
    @observable
    private loginStatus: LoginResult;
    @observable
    private username: string;
    @observable
    private password: string;

    constructor()
    {
        super();
        this.username = "";
        this.password = "";
    }

    private setUsername = (event) =>
    {
        this.username = event.target.value;
    }

    private setPassword = (event) =>
    {
        this.password = event.target.value;
    }

    private connect = () =>
    {
        var $this = this;
        Bridge.login(this.username, this.password).then((data) =>
        {
            console.log(data);
        });
    };

    public render()
    {
        let error;

        if (this.loginStatus != undefined
        && !this.loginStatus.succeeded)
        {
            error = this.loginStatus.message;
        }

        return <div className="Aligner">
            <div className="login-box">
                <div className="text-center header">Melas</div>
                <div className="text-center error">{error}</div>
                <div className="input-holder">
                    <input type="text"
                           className="text-box"
                           placeholder="Username"
                           value={this.username}
                           onChange={this.setUsername}/>
                </div>
                <div className="input-holder">
                    <input type="password"
                           className="text-box"
                           placeholder="Password"
                           value={this.password}
                           onChange={this.setPassword}/>
                </div>
                <div className="text-center connect"
                     onClick={this.connect}>connect</div>
            </div>
        </div>
    }
}