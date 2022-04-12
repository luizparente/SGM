import { IUser } from './user.model';

export interface IRequest {
    requestGuid: string;
    author: IUser;
    category: string;
    header: string;
    body: string;
    address: string;
    dateOpened: Date;
    dateClosed?: Date;
    rating?: number;
    response?: string;
}