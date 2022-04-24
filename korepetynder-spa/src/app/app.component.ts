import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { InteractionStatus } from '@azure/msal-browser';
import { filter, map, Observable, shareReplay } from 'rxjs';

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
    private breakpointObserver: BreakpointObserver
  ) {

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
  }

  setSelectedTab(id: number): void {

  }

  private checkAndSetActiveAccount() {
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
}
