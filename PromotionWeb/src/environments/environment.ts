// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  auth: {
    identityServerUrl: 'http://localhost:5000/',
    identityServiceClient_id: 'ro.client',
    identityServiceClient_secret: 'secret',
    identityServiceGrant_type: 'password',
    clientID: '352027914482-cej4hjp6mruvaf3d06psjag2j1psl4kg.apps.googleusercontent.com',
    domain: 'localhost:5000', // e.g., you.auth0.com
    audience: 'http://localhost:4200', // e.g., http://localhost:3001
    redirect: 'http://localhost:4200/callback',
    scope: 'openid profile email'
  },
  api: {
    host: 'https://localhost:44394/api/'
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
