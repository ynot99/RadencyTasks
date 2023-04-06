import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { map, Observable } from 'rxjs';
import {
  IApiResponse,
  IBookReviewsData,
} from 'src/app/modules/shared/interfaces';

import { BookService } from '../../services/book.service';

@Component({
  selector: 'app-view-book',
  templateUrl: './view-book.component.html',
  styleUrls: ['./view-book.component.scss'],
})
export class ViewBookComponent {
  bookData$: Observable<IBookReviewsData>;

  constructor(
    @Inject(MAT_DIALOG_DATA) public bookId: number,
    private bookService: BookService
  ) {
    this.bookData$ = {} as Observable<IBookReviewsData>;
  }

  ngOnInit(): void {
    this.bookData$ = this.bookService
      .getBookWithReviewsById(this.bookId)
      .pipe(map((response: IApiResponse<IBookReviewsData>) => response.data));
  }
}
