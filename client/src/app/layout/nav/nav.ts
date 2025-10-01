import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../../core/services/account-service';
import { RouterModule, Router } from '@angular/router';
import { RouterUpgradeInitializer } from '@angular/router/upgrade';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterModule],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {
  protected accountService = inject(AccountService);
  protected creds: any = {};
  protected router = inject(Router);

  login(): void {
    this.accountService.login(this.creds).subscribe({
      next: response => {
        this.router.navigateByUrl("/members");
        this.creds = {};
      },
      error: error => alert(error.message)
    });
  }

  logout(): void {
    this.accountService.logout();
    this.router.navigateByUrl("/");
  }
}

