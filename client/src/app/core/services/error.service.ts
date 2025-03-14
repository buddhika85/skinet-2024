import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorService 
{
  baseUrl = "https://localhost:5001/api/";
  controller = "buggy/";
  private http = inject(HttpClient);

  get404Error()
  {
    return this.http.get(this.baseUrl + this.controller + "notfound");
  }

  get400Error()
  {
    return this.http.get(this.baseUrl + this.controller + "badrequest");
    
  }
  get401Error()
  {
    return this.http.get(this.baseUrl + this.controller + "unauthorized");

  }

  get500Error()
  {
    return this.http.get(this.baseUrl + this.controller + "internalerror");
  }

  get400ValidationError()
  {
    return this.http.get(this.baseUrl + this.controller + "validationerror");
  }
}
