import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../shared/models/product';
import { Pagination } from '../shared/models/pagination';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseURL='https://localhost:5001/api/';

  constructor(private  http: HttpClient) { }

  //getProducts(brandId?: number,typeId? : number,sort? : string){
    getProducts(shopParams:ShopParams){
    let params=new HttpParams();
    if (shopParams.brandId>0) params=params.append('brandId',shopParams.brandId);
    if (shopParams.typeId>0) params=params.append('typeId',shopParams.typeId);
    params=params.append('sort',shopParams.sort);
    params=params.append('pageIndex',shopParams.pageNumber);
    params=params.append('pageSize',shopParams.pageSize);
    if(shopParams.search) params=params.append('search',shopParams.search);

 //return this.http.get<Pagination<Product[]>>(this.baseURL +'products?pageSize=50');
 return this.http.get<Pagination<Product[]>>(this.baseURL +'products',{params});
  }
getBrands(){
  return this.http.get<Brand[]>(this.baseURL + 'products/brands');
}

getTypes(){
  return this.http.get<Type[]>(this.baseURL + 'products/types');
}


}
