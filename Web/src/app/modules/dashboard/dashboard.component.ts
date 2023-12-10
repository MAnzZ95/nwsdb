import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit(): void {
  }


  getData() {
    this.http.get<any[]>(this.baseUrl + 'weatherforecast').subscribe(result => {
      //get data response from backend
    }, error => console.error(error));
  }
}
