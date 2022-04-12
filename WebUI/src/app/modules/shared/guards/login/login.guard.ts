import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/modules/core/services/user/user.service';

@Injectable({
  providedIn: 'root'
})
export class LoginGuard implements CanActivate {
  constructor(private _userService: UserService, private _router: Router) { }

  public canActivate(route: ActivatedRouteSnapshot,
                    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let isLoggedIn: boolean = this._userService.isLoggedIn();

    if (!isLoggedIn) {
      this._router.navigate(['/login']);
    }
    
    return isLoggedIn;
  }
  
}
