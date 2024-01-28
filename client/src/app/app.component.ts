import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Data Marketplace';
  user: any;


  constructor (private http:HttpClient){}

  ngOnInit(): void {
  this.http.get('https://localhost:5001/api/user').subscribe({
    next: response => this.user=response,
    error: error => console.log(error),
    complete : () => console.log('Members in Marvel Team')
  })  
  }

}
