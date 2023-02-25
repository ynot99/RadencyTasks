import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

import { IBookData } from '../../shared/interfaces/book-data.interface';

@Component({
  selector: 'app-view-book',
  templateUrl: './view-book.component.html',
  styleUrls: ['./view-book.component.scss'],
})
export class ViewBookComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public bookData: IBookData) {}
}
