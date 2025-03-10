import { Component, Input } from '@angular/core';
import { Product } from '../../shared/models/Product';
import { MatCard } from '@angular/material/card';

@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [
    MatCard
  ],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.scss'
})
export class ProductItemComponent {

  @Input() product?: Product;
}
