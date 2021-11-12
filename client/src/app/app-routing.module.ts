import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { CartComponent } from './cart/cart.component';
import { ContactComponent } from './contact/contact.component';
import { HomeComponent } from './home/home.component';
import { ItemDetailComponent } from './items/item-detail/item-detail.component';
import { ItemListComponent } from './items/item-list/item-list.component';
import { ListsComponent } from './lists/lists.component';

//array of objects, path is the path to the component
const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'contact', component: ContactComponent},
  {path: 'about', component: AboutComponent},
  {path: 'items', component: ItemListComponent},
  {path: 'item/:id', component: ItemDetailComponent},
  {path: 'lists', component: ListsComponent},
  {path: 'cart', component: CartComponent},
  {path: '**', component: HomeComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
