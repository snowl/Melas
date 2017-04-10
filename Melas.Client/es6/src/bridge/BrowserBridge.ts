export class LoginResult
{
    succeeded: boolean;
    message: string;
}

export class ClientBridge
{
    focus: () => void;
    flash: () => void;

    getSavedCredentials: () => Promise<string>;
    login: (username: string, password: string, save: boolean, callback: (result: string) => any) => void;
    logout: () => Promise<void>;
}

export class ServerBridge
{
    private callbacks: {(object: any): void;} [];
    
    constructor()
    {
        this.callbacks = [];
    }

    public pushMessage(object: any)
    {
        for (let callback of this.callbacks)
        {
            callback.call(undefined, object);
        }
    }

    public registerMessageListener(listener: {(object: any): void;})
    {
        this.callbacks.push(listener);
    }
}

// Expose the server bridge to C#
export var __bridge: ServerBridge = new ServerBridge();
(window as any).__bridge = __bridge;

declare var _bridge: ClientBridge;
export default _bridge;