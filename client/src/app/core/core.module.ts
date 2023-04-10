import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { HomeComponent } from '../home/home.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    NavBarComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[
    NavBarComponent
  ]
})
export class CoreModule { }
