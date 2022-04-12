import { IRole } from './role.model';

export interface IUser {
    username: string;
    roles: Array<IRole>;
}