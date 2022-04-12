import { Injectable } from '@angular/core';
import { IUser } from 'src/app/domain/user.model';
import { Observable, Observer, Subject } from 'rxjs';
import { IRole } from 'src/app/domain/role.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private loginSubject = new Subject<IUser>();        // The subject being observed. Represents the message content.
  public onLogin = this.loginSubject.asObservable();  // The event that will be propagated.

  constructor() {
  }

  public authenticate(username: string, password: string): Observable<IUser> {
    return Observable.create((observer: Observer<IUser>) => {
      let user: IUser;

      if (username == 'citizen') {
        user = <IUser>{
          username: 'Cidadão',
          roles: [
            <IRole>{ name: 'citizen' }
          ]
        };
      }
      else if (username == 'field-dispatcher' ) {
        user = <IUser>{
          username: 'Gerente de Campo',
          roles: [
            <IRole>{ name: 'field-dispatcher' }
          ]
        };
      }
      else if (username == 'field-technician') {
        user = <IUser>{
          username: 'Tecnico de Campo',
          roles: [
            <IRole>{ name: 'field-technician' }
          ]
        };
      }
      else {
        throw new Error("User unknown.");
      }

      this.setUser(user);
      this.notifyLoggedIn(user);
      observer.next(user);      
    });
  }

  public setUser(user?: IUser): void {
    if (user) {
      document.cookie = `username=${user?.username}`;
      document.cookie = `roles=${user?.roles.map(x => x.name)}`;
    }
    else {
      document.cookie = `username=;expires=Thu, 01 Jan 1970 00:00:01 GMT`;
      document.cookie = `roles=;expires=Thu, 01 Jan 1970 00:00:01 GMT`;
    }
  }

  public getUser(): IUser | null {
    const username = this.getCurrentUsername();
    const role = this.getCurrentRole();

    if (username && role) {
      const user = <IUser>{
        username: username,
        roles: [
          <IRole>{
            name: role
          }
        ]
      };

      return user;
    }

    return null;
  }

  public logout() : void {
    this.setUser();    
    this.notifyLoggedIn(undefined);
  }
  
  public isCurrentUserInRole(role: string): boolean {
    const user = this.getUser();

    if (user) 
      return user.roles.map(role => role.name).includes(role);

    return false;
  }

  public isLoggedIn() : boolean {
    const user = this.getUser();

    if (user)
      return true;

    return false;
  }

  private notifyLoggedIn(user?: IUser): void {
    this.loginSubject.next(user); // Propagating the logged user.
  }

  private getCurrentUsername(): string | null {
    let name = "username";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');

    for(let i = 0; i < ca.length; i++) {
      let c = ca[i];

      while (c.charAt(0) == ' ') {
        c = c.substring(1);
      }

      if (c.indexOf(name) == 0) {
        return c.substring(name.length + 1, c.length);
      }
    }
    
    return null;
  }

  private getCurrentRole(): string | null {
    let name = "roles";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');

    for(let i = 0; i < ca.length; i++) {
      let c = ca[i];

      while (c.charAt(0) == ' ') {
        c = c.substring(1);
      }

      if (c.indexOf(name) == 0) {
        return c.substring(name.length + 1, c.length);
      }
    }
    
    return null;
  }
}
