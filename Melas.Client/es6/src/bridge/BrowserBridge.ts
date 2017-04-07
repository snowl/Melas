export class LoginResult
{
    succeeded: boolean;
    message: string;
}

export class Bridge
{
    login: (username: string, password: string) => Promise<any>;
}

declare var _bridge: Bridge;
export default _bridge;