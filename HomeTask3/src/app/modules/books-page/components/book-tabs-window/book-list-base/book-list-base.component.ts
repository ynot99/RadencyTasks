import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBookData } from '../../../../shared/interfaces/';
import { BookService } from '../../../services/book.service';
import { SignalService } from '../../../services/signal.service';

@Component({
  selector: 'app-book-list-base',
  templateUrl: './book-list-base.component.html',
  styleUrls: ['./book-list-base.component.scss'],
})
export class BookListBaseComponent implements OnInit {
  protected books$: Observable<IBookData[]>;

  constructor(
    protected bookService: BookService,
    private signalService: SignalService
  ) {
    this.books$ = {} as Observable<IBookData[]>;
  }

  ngOnInit(): void {
    this.signalService.getSignal().subscribe((_: boolean) => {
      this.fetchBooks();
    });
    this.fetchBooks();
  }

  fetchBooks(): void {
    throw new Error('Base class method is called');
  }
}
