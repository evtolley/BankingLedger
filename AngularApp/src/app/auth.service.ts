import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Injectable()
export class AuthService {
constructor(private readonly toastr: ToastrService, private readonly router: Router){}

    isUserLoggedIn(): boolean {
        return localStorage.getItem('token') !== null;
    }

    login(token: string, email: string) {
        localStorage.setItem('token', token);
        this.toastr.success(`Welcome ${email}!`);
        this.router.navigate(['/ledger']);
    }

    logout() {
        localStorage.removeItem('token');
        this.router.navigate(['/account']);
    }
}
