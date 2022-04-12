import { IUser } from './user.model';

export interface ITechnician {
    technicianGuid: string;
    user: IUser;    
}