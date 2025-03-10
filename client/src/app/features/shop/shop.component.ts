import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../shared/models/Product';
import { ShopService } from '../../core/services/shop.service';
import { ProductItemComponent } from "../product-item/product-item.component";
import { MatDialog } from '@angular/material/dialog';
import { MatButton } from '@angular/material/button';
import { FiltersDialogComponent } from '../filters-dialog/filters-dialog.component';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [   
    ProductItemComponent,
    MatButton,
    MatIcon
],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit 
{
  
  products: Product[] = [];
  private shopService = inject(ShopService);
  private  dialogService = inject(MatDialog);

  ngOnInit(): void 
  {
    this.initialiZeShop();
  }

  initialiZeShop() 
  {
    this.shopService.getTypes();
    this.shopService.getBrands();
    this.shopService.getProducts().subscribe({
      next: response => this.products = response.data,
      error: error => console.error(error),
      //complete: () => console.log('Complete')
    });
  }

  openFiltersDialog()
  {
    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
    });
    dialogRef.ope
  }
}
