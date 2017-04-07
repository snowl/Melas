export class LoginResult
{
    succeeded: boolean;
    message: string;
}

export class ClientBridge
{
    login: (username: string, password: string, callback: (result: string) => any) => void;
}

export class ServerBridge
{
    private callbacks: {(object: string): void;} [];
    
    constructor()
    {
        this.callbacks = [];
    }

    public pushMessage(object: string)
    {
        for (let callback of this.callbacks)
        {
            callback.call(undefined, object);
        }
    }

    public registerMessageListener(listener: {(object: string): void;})
    {
        this.callbacks.push(listener);
    }
}

// Expose the server bridge to C#
export var __bridge: ServerBridge = new ServerBridge();
(window as any).__bridge = __bridge;

declare var _bridge: ClientBridge;
export default _bridge;