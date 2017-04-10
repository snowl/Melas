import {Expose} from 'class-transformer'
import {observable} from 'mobx'

export default class User
{
    @Expose({ name: "AccountName" })
    @observable
    public Username: string;
    @observable
    public PaidCurrency: number;
    @observable
    public FreeCurrency: number;
    @observable
    public statistics: UserStats;
}

export class UserStats
{
    @observable
    public Disconnected: number;
    @observable
    public Drawn: number;
    @observable
    public ID: number;
    @observable
    public Played: number;
    @observable
    public RankedDisconnected: number;
    @observable
    public RankedDrawn: number;
    @observable
    public RankedPlayed: number;
    @observable
    public RankedPracticePlayed: number;
    @observable
    public RankedWon: number;
    @observable
    public Referred: number;
    @observable
    public Won: number
}