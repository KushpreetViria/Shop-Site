import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

//Purely for testing, go to ".../errors" route via browser url

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {
  baseApiUrl = 'https://localhost:5001/api/';
  valiationErrors: string[];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  get404Error(){
    this.http.get(this.baseApiUrl + 'bug/not-found').subscribe(
      res => console.log(res),
      err => console.log(err)
    );
  }

  get400Error(){
    this.http.get(this.baseApiUrl + 'bug/bad-request').subscribe(
      res => console.log(res),
      err => console.log(err)
    );
  }

  get500Error(){
    this.http.get(this.baseApiUrl + 'bug/server-error').subscribe(
      res => console.log(res),
      err => console.log(err)
    );
  }

  get401Error(){
    this.http.get(this.baseApiUrl + 'bug/auth').subscribe(
      res => console.log(res),
      err => console.log(err)
    );
  }

  get400ValidationError(){
    this.http.post(this.baseApiUrl + 'account/register',{}).subscribe(
      res => console.log(res),
      err => {
        console.log(err)
        this.valiationErrors =err;
      }
    );
  }

}
