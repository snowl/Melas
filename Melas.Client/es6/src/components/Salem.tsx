import * as React from 'react'
import {inject, observer} from 'mobx-react'
import {observable} from 'mobx'

@inject("routing", "clientStore")
@observer
export default class Salem extends React.Component<any, any>
{
    public render()
    {
        return <div>
            <div className="user-info">
                hi
            </div>
        </div>
    }
}