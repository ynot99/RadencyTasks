import { Component } from '@angular/core';
import { map } from 'rxjs';
import { IApiResponse, IBookData } from '../../../../shared/interfaces/';
import { BookListBaseComponent } from '../book-list-base/book-list-base.component';

@Component({
  selector: 'app-book-list',
  templateUrl: './../book-list-base/book-list-base.component.html',
  styleUrls: ['./../book-list-base/book-list-base.component.scss'],
})
export class BookListComponent extends BookListBaseComponent {
  override fetchBooks(): void {
    this.books$ = this.bookService
      .getAllBooks()
      .pipe(map((response: IApiResponse<IBookData[]>) => response.data));
  }
}
