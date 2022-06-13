import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { InteractionStatus } from '@azure/msal-browser';
import { filter, map, Observable, shareReplay } from 'rxjs';
import { Router } from '@angular/router';
import { ColorSchemeService } from './shared/color-scheme.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'korepetynder-spa';
  isIframe = false;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(
    private msalBroadcastService: MsalBroadcastService,
    private authService: MsalService,
    private breakpointObserver: BreakpointObserver,
    private router: Router,
    public colorSchemeService: ColorSchemeService
  ) {
    this.colorSchemeService.load();
  }

  ngOnInit(): void {
    this.isIframe = window !== window.parent && !window.opener;

    this.msalBroadcastService.inProgress$
      .pipe(
        filter((status: InteractionStatus) => status === InteractionStatus.None)
      )
      .subscribe(() => {
        this.checkAndSetActiveAccount();
      })

    console.log(this.colorSchemeService.currentActive());
  }

  navigateToSettings(tabId: number): void {
    this.router.navigate(['settings'], {
      queryParams: {
        tabId
      }
    });
  }

  navigateToSuggestions(): void {
    this.router.navigate(['suggestions'], {});
  }

  logout(): void {
    this.authService.logout();
  }

  private checkAndSetActiveAccount(): void {
    /**
     * If no active account set but there are accounts signed in, sets first account to active account.
     * To use active account set here, subscribe to inProgress$ first in the component.
     */
    let activeAccount = this.authService.instance.getActiveAccount();

    if (!activeAccount && this.authService.instance.getAllAccounts().length > 0) {
      let accounts = this.authService.instance.getAllAccounts();
      this.authService.instance.setActiveAccount(accounts[0]);
    }
  }

  changeTheme(): void {
    this.colorSchemeService.update(this.colorSchemeService.currentActive() === 'light' ? 'dark' : 'light');
  }
}
