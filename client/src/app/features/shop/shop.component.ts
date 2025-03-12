import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../shared/models/Product';
import { ShopService } from '../../core/services/shop.service';
import { ProductItemComponent } from "../product-item/product-item.component";
import { MatDialog } from '@angular/material/dialog';
import { MatButton } from '@angular/material/button';
import { FiltersDialogComponent } from '../filters-dialog/filters-dialog.component';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { ShopParams } from '../../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [   
    ProductItemComponent,
    MatButton,
    MatIcon,
    MatMenu,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger
],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit 
{
  
  products: Product[] = [];
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low-High', value: 'priceAsc' },
    { name: 'Price: High-Low', value: 'priceDesc' }
  ];

  shopParams = new ShopParams();

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
    
    this.getProducts();
  }

  getProducts() {    
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => this.products = response.data,
      error: error => console.error(error),
      //complete: () => console.log('Complete')
    });
  }

  openFiltersDialog()
  {
    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {                                                 // data to pass to the dialog
        selectedBrands: this.shopParams.brands,
        selectedTypes: this.shopParams.types
      }
    });

    // after closing the dialog access selected brands and types
    dialogRef.afterClosed().subscribe(result => {
      if(result)
      {
        console.log(result);
        this.shopParams.brands = result.selectedBrands;
        this.shopParams.types = result.selectedTypes;

        // apply filters
        this.getProducts();
      }
    });

  }

  onSortChange(event: MatSelectionListChange) {
    const selectedOption = event.options[0];
    if(selectedOption)
    {
      this.shopParams.sort = selectedOption.value;
      console.log(this.shopParams.sort);
      this.getProducts();
    } 
  }

}

