import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './components/about/about.component';
import { CartComponent } from './components/cart/cart/cart.component';
import { ContactComponent } from './components/contact/contact.component';
import { NotFoundComponent } from './components/errors/not-found/not-found.component';
import { ServerErrorComponent } from './components/errors/server-error/server-error.component';
import { TestErrorsComponent } from './components/errors/test-errors/test-errors.component';
import { HomeComponent } from './components/home/home.component';
import { ItemCardComponent } from './components/items/item-card/item-card.component';
import { ItemListComponent } from './components/items/item-list/item-list.component';
import { AuthGuard } from './_guards/auth.guard';
import { UserDetailsProfileComponent } from './components/users/user-details-profile/user-details-profile.component';

//array of objects, path is the path to the component
const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers:'always',
    canActivate:[AuthGuard],
    children:[
      {path: 'items', component: ItemListComponent},
      {path: 'item/:id', component: ItemCardComponent},
      {path: 'profile', component: UserDetailsProfileComponent},
      {path: 'cart', component: CartComponent},
    ]
  },
  {path: '404-not-found',component: NotFoundComponent},
  {path: '500-server-error',component:ServerErrorComponent},
  {path: 'contact', component: ContactComponent},
  {path: 'about', component: AboutComponent},
  {path: 'errors', component: TestErrorsComponent},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
