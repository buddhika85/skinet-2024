import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../shared/models/Product';
import { ShopService } from '../../core/services/shop.service';
import { MatCard } from '@angular/material/card';
import { ProductItemComponent } from "../product-item/product-item.component";

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    MatCard,
    ProductItemComponent
],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit 
{
  
  products: Product[] = [];
  private shopService = inject(ShopService);

  ngOnInit(): void 
  {
    this.shopService.getProducts().subscribe({
      next: response => this.products = response.data,
      error: error => console.error(error),
      //complete: () => console.log('Complete')
    });
  }

}
