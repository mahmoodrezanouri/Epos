import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component} from '@angular/core';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent  {

  constructor(private http: HttpClient) {

  }

  public print() {

    this.http.post<{}>('/print', {}, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'my-auth-token'
      })
    }).subscribe(() => {
      return;

    });

  }
}





