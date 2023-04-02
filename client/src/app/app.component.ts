import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product } from './models/product';
import { Pagination } from './models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Skinet';
  //products: any[] =[];
  products:Product[] = [];

  constructor(private http:HttpClient){}


  ngOnInit(): void {
    this.http.get<Pagination<Product[]>>('https://localhost:5001/api/products?pageSize=50').subscribe({
      //next:response=>console.log(response),//what to do next
      next:(response)=>this.products=response.data,
      error:error=>console.log(error),//what to do id there is an error
      complete: ()=>{
        console.log('requested completed');
        console.log('extra statment');

      }
    })
  }
}
