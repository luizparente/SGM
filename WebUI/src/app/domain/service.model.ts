import { IRequest } from './request.model';
import { ITechnician } from './technician.model';

export interface IService {
    serviceGuid: string;
    title: string;
    request: IRequest;
    assignee?: ITechnician;
    address: string;
    scheduled?: Date;
    completed?: Date;
    response?: string;
}