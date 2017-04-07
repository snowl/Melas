import * as React from 'react'
import Login from './Login'
import Salem from './Salem'
import {Switch, Route, Redirect} from 'react-router'
import {inject, observer} from 'mobx-react'

@inject("routing", "clientStore")
@observer
export default class Main extends React.Component<any, any>
{
    componentWillMount()
    {
        if (!this.props.clientStore.loggedIn)
        {
            this.props.routing.push("/login");
        }
    }

    public render()
    {
        return <div>
            <Switch>
                <Route path="/login" component={Login}/>
                <Route path="/salem" component={Salem}/>
            </Switch>
        </div>
    }
}