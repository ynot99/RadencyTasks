import { Component } from '@angular/core';
import { ITab } from '../../shared/interfaces/';

@Component({
  selector: 'app-books-page',
  templateUrl: './books-page.component.html',
  styleUrls: ['./books-page.component.scss'],
})
export class BooksPageComponent {
  tabs: ITab[];

  constructor() {
    this.tabs = [
      { label: 'All', fetchurl: '/api/books' },
      { label: 'Recommended', fetchurl: '/api/books/recommended' },
    ];
  }
}
