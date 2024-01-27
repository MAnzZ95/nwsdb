import { HttpClient } from "@angular/common/http";
import { AppConfigService } from "src/app/app-config.service";
import { AppConfig } from "../models/config";
import { KeycloakService } from "keycloak-angular";

export function initializeApp(
    http: HttpClient, 
    //keycloak: KeycloakService, 
    config: AppConfigService): () => Promise<boolean> {
      return (): Promise<boolean> => {
        return new Promise<boolean>(async (resolve, reject) => {
          try {
            let keycloakConfig = await http.get<AppConfig>('assets/config/app-config.json').toPromise();
            
            config.apiUrl = keycloakConfig?.apiUrl;
            console.log(config.apiUrl);
          //   await keycloak.init({
          //     config: keycloakConfig,
          //     initOptions: {
          //       onLoad: 'login-required',
          //         checkLoginIframe: false
          //     },
          //   });
            resolve(true);
          } catch (error) {
            reject(error);
          }
        });
      };
  }

  async function initializeKeycloak(
    http: HttpClient,
    keycloak: KeycloakService,
    config: AppConfigService
  ): Promise<void> {
    const keycloakConfig = await http
      .get<AppConfig>('assets/config/app-config.json')
      .toPromise();
    config.apiUrl = keycloakConfig?.apiUrl;
    console.log(config.apiUrl);
  
    // await keycloak.init({
    //   config: keycloakConfig,
    //   initOptions: {
    //     onLoad: 'login-required',
    //     checkLoginIframe: false,
    //   },
    // });
  }