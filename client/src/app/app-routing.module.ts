import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './components/about/about.component';
import { CartComponent } from './components/cart/cart/cart.component';
import { ContactComponent } from './components/contact/contact.component';
import { NotFoundComponent } from './components/errors/not-found/not-found.component';
import { ServerErrorComponent } from './components/errors/server-error/server-error.component';
import { TestErrorsComponent } from './components/errors/test-errors/test-errors.component';
import { HomeComponent } from './components/home/home.component';
import { ItemListComponent } from './components/items/item-list/item-list.component';
import { AuthGuard } from './_guards/auth.guard';
import { UserDetailsProfileComponent } from './components/users/user-details-profile/user-details-profile.component';
import { ItemDetailsComponent } from './components/items/item-details/item-details.component';
import { EditUserDetailsProfileComponent } from './components/users/edit-user-details-profile/edit-user-details-profile.component';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { MyItemsComponent } from './components/items/my-items/my-items.component';
import { AddItemComponent } from './components/items/add-item/add-item.component';
import { UpdateItemComponent } from './components/items/update-item/update-item.component';

//array of objects, path is the path to the component
const routes: Routes = [
	{path: '', component: HomeComponent},
	{
		path: '',
		runGuardsAndResolvers:'always',
		canActivate:[AuthGuard],
		children:[
			{
				path: 'items',
				children:[
					{
						path:'', //items
						//pathMatch: "full",
						component: ItemListComponent
					},
					{ 
						path: ':id', //items/id
						component: ItemDetailsComponent
					}
				]
			},
			{
				path: 'profile',
				children:[
					{ 
						path: 'my-profile',	//profile/my-profle
						component: UserDetailsProfileComponent
					},
					{
						path: 'edit',	//profile/edit
						component: EditUserDetailsProfileComponent,
						canDeactivate: [PreventUnsavedChangesGuard]
					}
				]
			},
			{path: 'cart', component: CartComponent}, //cart
			{
				path: 'my-items',	//my-items
				children:[
					{
						path:'',
						component: MyItemsComponent,
					},
					{
						path:'add-item',	//my-items/add-item
						component:AddItemComponent
					},
					{
						path:'update/:id',	//my-items/update/#
						component:UpdateItemComponent
					}
				]
			}
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
