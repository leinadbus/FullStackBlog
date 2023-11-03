import { Component, OnDestroy } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { CategoryService } from '../Services/category.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnDestroy {

  model: AddCategoryRequest;
  private addCategorySubscription?: Subscription;

  constructor(private categorySerice: CategoryService) {
    this.model = {
      name: '',
      urlHandle: ''
    };
  }

  onFormSubmit() {
    this.addCategorySubscription = this.categorySerice.addCategory(this.model)
    .subscribe({
      next: (response) => {
        console.log('This was successful!');
      },
      error: (error) => {

      }
    });
  }

  ngOnDestroy(): void {
    this.addCategorySubscription?.unsubscribe();
  }

}
