import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ShopComponent } from './features/shop/shop.component';
import { ProductDetailsComponent } from './features/shop/product-details/product-details.component';

export const routes: Routes = [

    {
        path: '',   // home URL path
        component: HomeComponent
    },
    {
        path: 'shop',   // shop URL path
        component: ShopComponent
    },
    {
        path: 'shop/:id',   
        component: ProductDetailsComponent
    },
    {
        path: '**',                 // wid card route
        redirectTo: '',             // go to Home
        pathMatch: 'full'                   // match the whole URL path
    }
];
