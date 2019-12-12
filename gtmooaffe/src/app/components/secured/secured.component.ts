import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-secured',
  templateUrl: './secured.component.html',
  styleUrls: ['./secured.component.scss']
})
export class SecuredComponent implements OnInit {

  public values$: Observable<string[]>;

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
    this.values$ = this.httpClient.get<string[]>(`${environment.baseUrl}/HttpTrigger1`);
  }
}