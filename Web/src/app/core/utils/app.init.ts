import { HttpClient } from "@angular/common/http";
import { AppConfigService } from "src/app/app-config.service";
import { AppConfig } from "../models/config";
import { KeycloakService } from "keycloak-angular";

export function initializeApp(
    http: HttpClient, 
    keycloak: KeycloakService, 
    config: AppConfigService): () => Promise<boolean> {
      return async (): Promise<boolean> => {
        try {
          await initializeKeycloak(http, keycloak, config);
          // await initializeSettings(
          //   generalAppConfig,
          //   settingService,
          //   generalSettingService,
          //   http,
          //   config
          // );
          //await getRoutePermissionDetails(accessService, routePermissionsService);
          return true;
        } catch (error) {
          return false;
        }
      };
    }

  // async function initializeKeycloak(
  //   http: HttpClient,
  //   keycloak: KeycloakService,
  //   config: AppConfigService
  // ): Promise<void> {
  //    console.log("rtereoop");
  //   const keycloakConfig = await http
  //     .get<AppConfig>('assets/config/app-config.json')
  //     .toPromise();
  //   config.apiUrl = keycloakConfig?.apiUrl;
  //   console.log(keycloakConfig);
  
  //   try {
  //     console.log("Initializing Keycloak...");
  //     await keycloak.init({
  //       config: keycloakConfig,
  //       initOptions: {
  //         onLoad: 'login-required',
  //         checkLoginIframe: false,
  //       },
  //     });
  //     console.log("Keycloak initialized successfully");
  //   } catch (error) {
  //     console.error("Keycloak initialization failed", error);
  //   }
  //   console.log("sdsad");
    // const timeoutMins = 255;
    //   const interval = setInterval(() => {
    //     const token = keycloak.getKeycloakInstance()?.token ?? '';
    //     const decodedToken: KeycloakResponse = jwtDecode(token);
    //     const sessionTimeout: SessionTimeout = {
    //       userId: decodedToken.sub,
    //       type: 'SessionTimeOut',
    //       dateTime: new Date(),
    //     };
    //     sessionTimeoutService
    //       .postSessionTimeout(sessionTimeout)
    //       .subscribe(() => {
    //         clearInterval(interval);
    //       });
    //   }, timeoutMins * 1000);
  //}

// import { HttpClient } from '@angular/common/http';
//  // Adjust the import based on your project structure
// import { KeycloakService } from 'keycloak-angular';
// import { AppConfigService } from 'src/app/app-config.service';
// import { AppConfig } from '../models/config';

// export async function initializeKeycloak(
//   keycloak: KeycloakService,
//   http: HttpClient,
//   configService: AppConfigService // Assuming AppConfigService is where you manage your application's configuration
// ) {
//   // const appConfig: AppConfig = await http
//   //   .get<AppConfig>('assets/config/app-config.json')
//   //   .toPromise();
//   const appConfig = await http
//     .get<AppConfig>('assets/config/app-config.json')
//     .toPromise();

//   configService.apiUrl = appConfig?.apiUrl; // Adjust based on how you want to use the apiUrl in your app

//   return keycloak.init({
//     config: {
//       url: "http://localhost:8080/auth/",
//       realm: "master",
//       clientId: "angular-client",
//     },
//     initOptions: {
//       onLoad: 'login-required',
//       checkLoginIframe: false,
//     },
//   });
// }

//import { KeycloakService } from "keycloak-angular";

// export function initializeKeycloak(
//   keycloak: KeycloakService
//   ) {
//     return () =>
//       keycloak.init({
//         config: {
//           url: 'http://localhost:8080/auth',
//           realm: 'nwsdb-lms',
//           clientId: 'web-client',
//         },
//         initOptions: {
    
//           //pkceMethod: 'S256', 
//           // must match to the configured value in keycloak
//           //redirectUri: 'http://localhost:4200',   
//           // this will solved the error 
//           onLoad:"login-required",
//           checkLoginIframe: false
//         }});
// }


async function initializeKeycloak(
  http: HttpClient,
  keycloak: KeycloakService,
  config: AppConfigService
): Promise<void> {
  const keycloakConfig = await http
    .get<AppConfig>('assets/config/app-config.json')
    .toPromise();
  config.apiUrl = keycloakConfig?.apiUrl;

  await keycloak.init({
            config: {
              url: 'http://localhost:8080/auth',
              realm: 'nwsdb-lms',
              clientId: 'web-client',
            },
            initOptions: {
        
              //pkceMethod: 'S256', 
              // must match to the configured value in keycloak
              //redirectUri: 'http://localhost:4200',   
              // this will solved the error 
              onLoad:"login-required",
              checkLoginIframe: false
            }});
}