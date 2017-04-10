import {RouterStore} from 'mobx-react-router'
import * as React from 'react'
import ClientStore from '../stores/ClientStore'
import {inject, observer} from 'mobx-react'
import {observable} from 'mobx'
import ClientBridge from '../bridge/BrowserBridge'

@inject("routing", "clientStore")
@observer
export default class Salem extends React.Component<{routing?: RouterStore, clientStore?: ClientStore}, any>
{
    @observable
    private friendListOpen: boolean;

    private logout = () =>
    {
        ClientBridge.logout().then(() => {
            // Restart the client instance
            window.location.href = "/";
        });
    }

    private toggleFriendsList = () =>
    {
        this.friendListOpen = !this.friendListOpen;
    }

    public render()
    {
        let mainContainerClass = "main-container";
        let friendListClass = "friend-list";

        if (this.friendListOpen)
        {
            mainContainerClass += " open-friend-list";
            friendListClass += " open";
        }

        return <div className="melas">
            <div className="user-info">
                <div className="user-name">{this.props.clientStore.user.Username}</div>
                <div className="log-out"
                     onClick={this.logout}>logout</div>
            </div>
            <div className="salem-container">
                <div className={mainContainerClass}>

                </div>
                <div className={friendListClass}>
                    <div className="friends">
                        <div className="friend">
                            <div className="status online"/>
                            <div className="user-name">user123</div>
                        </div>
                    </div>
                    <div className="add-friend">
                        <input className="friend-input"
                               placeholder="friend name"
                               type="text"/>
                    </div>
                </div>
            </div>
            <div className="chat-bar">
                <div className="friend">
                    <div className="status online"/>
                    <div className="user-name">user123</div>
                </div>
                <div className="open-friend-list"
                     onClick={this.toggleFriendsList}>
                    friends
                </div>
            </div>
        </div>
    }
}