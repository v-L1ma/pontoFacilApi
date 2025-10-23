import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './interceptors/auth/auth.interceptor';
import { mensagensInterceptor } from './interceptors/mensagens/mensagens.interceptor';
import { provideCharts } from 'ng2-charts';
import { BarController, Colors, Legend } from 'chart.js';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor, mensagensInterceptor])),
    provideCharts({ registerables: [BarController, Legend, Colors] })
  ]
};
