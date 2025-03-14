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
 
  get404Error()
  {
    this.errorService.get404Error().subscribe({
      next: response => console.log(response),
      error: error => console.error(error)
    });
  }

  get400Error()
  {
    
  }
  get401Error()
  {
    

  }

  get500Error()
  {
    
  }

  get400ValidationError()
  {
    
  }
}
