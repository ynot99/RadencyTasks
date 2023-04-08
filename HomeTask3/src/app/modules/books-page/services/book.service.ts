import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import {
  IApiResponse,
  IBook,
  IBookData,
  IBookReviewsData,
  ISaveBook,
} from '../../shared/interfaces/';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  private apiUrl;

  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiBaseUrl + '/api/books';
  }

  getAllBooks(): Observable<IApiResponse<IBookData[]>> {
    return this.http.get<IApiResponse<IBookData[]>>(this.apiUrl);
  }

  getRecommendedBooks(): Observable<IApiResponse<IBookData[]>> {
    return this.http.get<IApiResponse<IBookData[]>>(
      `${this.apiUrl}/recommended`
    );
  }

  getBookById(id: number): Observable<IApiResponse<IBook>> {
    return this.http.get<IApiResponse<IBook>>(`${this.apiUrl}/onlybook/${id}`);
  }

  getBookWithReviewsById(
    id: number
  ): Observable<IApiResponse<IBookReviewsData>> {
    return this.http.get<IApiResponse<IBookReviewsData>>(
      `${this.apiUrl}/${id}`
    );
  }

  saveBook(book: ISaveBook): Observable<ISaveBook> {
    return this.http.post<ISaveBook>(`${this.apiUrl}/save`, book);
  }
}
