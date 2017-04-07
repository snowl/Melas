import ClientStore from './ClientStore'
import {RouterStore} from 'mobx-react-router'

const Stores = 
{
    routing: new RouterStore(),
    clientStore: new ClientStore()
};

export default Stores;