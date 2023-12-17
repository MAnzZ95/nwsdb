import { HttpClient } from "@angular/common/http";
import { AppConfigService } from "src/app/app-config.service";
import { AppConfig } from "../models/config";

export function initializeKeycloak(
    http: HttpClient, 
    // keycloak: KeycloakService, 
    config: AppConfigService): () => Promise<boolean> {
    return (): Promise<boolean> => {
      return new Promise<boolean>(async (resolve, reject) => {
        try {
          let keycloakConfig = await http.get<AppConfig>('assets/config/app-config.json').toPromise();
          
          config.apiUrl = keycloakConfig?.apiUrl;
  
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