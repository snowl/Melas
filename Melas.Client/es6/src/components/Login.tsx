import * as React from 'react'
import {inject, observer} from 'mobx-react'

@inject("routing", "clientStore")
@observer
export default class Login extends React.Component<any, any>
{
    public render()
    {
        return <div>
            Login
        </div>
    }
}