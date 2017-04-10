import ClientStore from '../stores/ClientStore';
import { RouterStore } from 'mobx-react-router';
import * as React from 'react';
import ClientBridge, {LoginResult} from '../bridge/BrowserBridge'
import {inject, observer} from 'mobx-react'
import {observable} from 'mobx'

@inject("routing", "clientStore")
@observer
export default class Login extends React.Component<{routing?: RouterStore, clientStore?: ClientStore}, any>
{
    @observable
    private loginStatus: LoginResult;
    @observable
    private connecting: boolean;
    @observable
    private username: string;
    @observable
    private password: string;
    @observable
    private rememberMe: boolean;
    @observable
    private loaded: boolean;

    constructor()
    {
        super();

        ClientBridge.getSavedCredentials().then((data: string) => {
            let user = JSON.parse(data);
            this.username = user.Username;
            this.password = user.Password;

            if (this.username.length > 0)
            {
                this.rememberMe = true;
            }

            this.loaded = true;
        });
    }

    private setUsername = (event) =>
    {
        this.username = event.target.value;
    }

    private setPassword = (event) =>
    {
        this.password = event.target.value;
    }

    private toggleRememberMe = () =>
    {
        this.rememberMe = !this.rememberMe;
    }

    private connect = () =>
    {
        this.connecting = true;
        let $this = this;
        ClientBridge.login(this.username, this.password, this.rememberMe, function(data: string)
        {
            let result: LoginResult = JSON.parse(data);
            if (result.succeeded)
            {
                $this.props.clientStore.loggedIn = true;
            }
            else
            {
                $this.loginStatus = result;
                $this.connecting = false;
            }
        });
    };

    private handleKeyPress = (event) =>
    {
        if (event.key == 'Enter')
        {
            this.connect();
        }
    }

    public render()
    {
        if (!this.loaded)
            return null;

        let error;
        let errorClass = " hide";

        if (this.loginStatus != undefined
        && !this.loginStatus.succeeded)
        {
            error = this.loginStatus.message;
            errorClass = "";
        }

        let rememberMeClass = "text-center remember-me";
        if (this.rememberMe)
        {
            rememberMeClass += " toggled";
        }

        return <div className="Aligner">
            <div className="login-box">
                <div className="text-center header">Melas</div>
                {
                    !this.connecting ?
                    <div>
                        <div className={"text-center error" + errorClass}>{error}</div>
                        <div className="input-holder">
                            <input type="text"
                                className="text-box"
                                placeholder="Username"
                                value={this.username}
                                onChange={this.setUsername}
                                onKeyPress={this.handleKeyPress}/>
                        </div>
                        <div className="input-holder">
                            <input type="password"
                                className="text-box"
                                placeholder="Password"
                                value={this.password}
                                onChange={this.setPassword}
                                onKeyPress={this.handleKeyPress}/>
                        </div>
                        <div className={rememberMeClass}
                             onClick={this.toggleRememberMe}>remember me</div>
                        <div className="text-center connect"
                             onClick={this.connect}>connect</div>
                    </div>

                    :
                    
                    <div className="text-center connecting">connecting</div>
                }
            </div>
        </div>
    }
}