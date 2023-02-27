import { Component, Input, OnInit } from '@angular/core';
import { map, Observable } from 'rxjs';
import { IApiResponse, IBookData } from '../../../shared/interfaces/';
import { BookService } from '../../services/book.service';
import { SignalService } from '../../services/signal.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss'],
})
export class BookListComponent implements OnInit {
  @Input() fetchurl: string;
  books$: Observable<IBookData[]>;

  constructor(
    private bookService: BookService,
    private signalService: SignalService
  ) {
    this.books$ = {} as Observable<IBookData[]>;
    this.fetchurl = '';
  }

  ngOnInit(): void {
    this.signalService.getSignal().subscribe((_: boolean) => {
      this.fetchBooks();
    });
    this.fetchBooks();
  }

  fetchBooks(): void {
    this.books$ = this.bookService
      .getAllBooks()
      .pipe(map((response: IApiResponse<IBookData[]>) => response.data));
  }
}
