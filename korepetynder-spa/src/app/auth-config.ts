import { MsalGuardConfiguration, MsalInterceptorConfiguration } from "@azure/msal-angular";
import { BrowserCacheLocation, InteractionType, IPublicClientApplication, LogLevel, PublicClientApplication } from "@azure/msal-browser";
import { environment } from "src/environments/environment";

const b2cPolicies = {
  names: {
    signUpSignIn: "B2C_1_SignUpSignIn",
  },
  authorities: {
    signUpSignIn: {
      authority: "https://korepetynder.b2clogin.com/korepetynder.onmicrosoft.com/B2C_1_SignUpSignIn"
    }
  },
  authorityDomain: "korepetynder.b2clogin.com"
};

const korepetynderApiConfig = {
  endpoint: environment.apiUrl,
  scopes: ["https://korepetynder.onmicrosoft.com/korepetynder-api/Api.Access"]
};

export function msalInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: "2958059a-2c2b-48bd-8a1c-6986309cdaf6",
      authority: b2cPolicies.authorities.signUpSignIn.authority,
      knownAuthorities: [b2cPolicies.authorityDomain],
      redirectUri: '/auth'
    },
    cache: {
      cacheLocation: BrowserCacheLocation.LocalStorage,
      storeAuthStateInCookie: false
    },
    system: {
      loggerOptions: {
        logLevel: LogLevel.Info,
        piiLoggingEnabled: false,
        loggerCallback: (logLevel, message) => console.log(message)
      }
    }
  });
}

export function msalInterceptorConfigFactory(): MsalInterceptorConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap: new Map([
      [korepetynderApiConfig.endpoint, korepetynderApiConfig.scopes]
    ])
  };
}

export function msalGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: {
      scopes: korepetynderApiConfig.scopes
    }
  };
}
