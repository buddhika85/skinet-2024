import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { ErrorService } from '../../core/services/error.service';

@Component({
  selector: 'app-test-error',
  standalone: true,
  imports: [
    MatButton
  ],
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.scss'
})
export class TestErrorComponent 
{

  private errorService: ErrorService = inject(ErrorService);
  errorMessage: string = "";
 
  get404Error()
  {
    this.errorService.get404Error().subscribe({
      next: response => console.log(response),
      error: error => {
        console.error(error);
        this.errorMessage = error.message;
      }
    });
  }

  get400Error()
  {
    this.errorService.get400Error().subscribe({
      next: response => console.log(response),
      error: error => {
        console.error(error);
        this.errorMessage = error.message;
      }
    });
  }

  get401Error()
  {
    this.errorService.get401Error().subscribe({
      next: response => console.log(response),    
      error: error => {
        console.error(error);
        this.errorMessage = error.message;
      }
    });

  }

  get500Error()
  {
    this.errorService.get500Error().subscribe({
      next: response => console.log(response),    
      error: error => {
        console.error(error);
        this.errorMessage = error.message;
      }
    });
  }

  get400ValidationError()
  {
    this.errorService.get400ValidationError().subscribe({
      next: response => console.log(response),    
      error: error => {
        console.error(error);
        this.errorMessage = error.message;
      }
    });
  }
}
