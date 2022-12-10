import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ApiHttpInterceptorInterceptor } from './interceptors/api-http-interceptor.interceptor';
import { HttpErrorInterceptorInterceptor } from './interceptors/http-error-interceptor.interceptor';

export const httpInterceptorProviders = [
  {provide: HTTP_INTERCEPTORS, useClass: ApiHttpInterceptorInterceptor, multi: true},
  {provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptorInterceptor, multi: true}
];
