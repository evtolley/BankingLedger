import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { LedgerService } from './ledger/ledger.service';

@Injectable()
export class AuthService {
constructor(private readonly toastr: ToastrService, private readonly ledgerService: LedgerService, private readonly router: Router){}

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

        // we need get of existing data so the next user to login doesn't see data from the previous user
        this.ledgerService.transactions$.next([]);
        this.ledgerService.accountBalance$.next(0);

        this.router.navigate(['/account']);
    }
}
