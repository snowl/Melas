import * as React from "react"
import * as ReactDOM from "react-dom"
import Stores from './stores/Stores'
import Main from './components/Main'
import createBrowserHistory from 'history/createBrowserHistory';
import {syncHistoryWithStore} from 'mobx-react-router';
import {Provider} from 'mobx-react'
import {Route, Router} from 'react-router'
import 'reflect-metadata'

syncHistoryWithStore(createBrowserHistory(), Stores.routing);

ReactDOM.render(
    <Provider {...Stores}>
        <Router history={Stores.routing.history}>
            <Route path="/" component={Main}/>
        </Router>
    </Provider>,
    document.getElementById("react")
);