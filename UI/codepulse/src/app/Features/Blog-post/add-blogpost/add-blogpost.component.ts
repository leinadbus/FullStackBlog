import { Component } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent {
  model: AddBlogPost;

  constructor() {
    this.model = {
      Title: '',
      ShortDescription: '',
      UrlHandle: '',
      Content: '',
      FeaturedImageUrl: '',
      Author: '',
      IsVisible: true,
      PublishedDate: new Date()
    }
  }


  onFormSubmit(): void {
    console.log(this.model);
  }
}
