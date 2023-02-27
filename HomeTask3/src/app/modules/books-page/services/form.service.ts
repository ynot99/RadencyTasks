import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FormService {
  private bookData = new BehaviorSubject<any>({});
  currentData = this.bookData.asObservable();

  constructor() {}

  changeData(data: any) {
    this.bookData.next(data);
  }
}
