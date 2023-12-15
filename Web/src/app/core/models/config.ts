// This is a sample model
export interface AppConfig {
    apiUrl?: string;
    /**
     * URL to the Keycloak server, for example: http://keycloak-server/auth
     */
    url?: string;
    /**
     * Name of the realm, for example: 'myrealm'
     */
    realm: string;
    /**
     * Client identifier, example: 'myapp'
     */
    clientId: string;
  }