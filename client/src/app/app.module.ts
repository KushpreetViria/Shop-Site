import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { FormsModule } from '@angular/forms';

import { SharedModule } from './shared.module';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './components/app.component';
import { NavComponent } from './components/nav/nav.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { ItemListComponent } from './components/items/item-list/item-list.component';
import { ItemCardComponent } from './components/items/item-card/item-card.component';
import { CartComponent } from './components/cart/cart/cart.component';
import { CartCardComponent } from './components/cart/cart-card/cart-card.component'
import { ContactComponent } from './components/contact/contact.component';
import { AboutComponent } from './components/about/about.component';
import { ItemDetailsComponent } from './components/items/item-details/item-details.component';
import { UserDetailsProfileComponent } from './components/users/user-details-profile/user-details-profile.component';

import { TestErrorsComponent } from './components/errors/test-errors/test-errors.component';
import { NotFoundComponent } from './components/errors/not-found/not-found.component';
import { ServerErrorComponent } from './components/errors/server-error/server-error.component';

import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    ItemListComponent,
    ItemCardComponent,
    ItemDetailsComponent,
    CartComponent,
    CartCardComponent,
    ContactComponent,
    AboutComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    UserDetailsProfileComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor,   multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
