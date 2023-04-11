import { Component, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { IBookData } from 'src/app/modules/shared/interfaces/book-data.interface';
import { FormService } from '../../../../services/form.service';
import { ViewBookComponent } from '../../../view-book/view-book.component';

@Component({
  selector: 'app-book-list-item',
  templateUrl: './book-list-item.component.html',
  styleUrls: ['./book-list-item.component.scss'],
})
export class BookListItemComponent {
  @Input() book: IBookData;

  constructor(private dialog: MatDialog, private formService: FormService) {
    this.book = {} as IBookData;
  }

  openDialogWithBookInfo() {
    this.dialog.open(ViewBookComponent, {
      data: this.book.id,
      width: '700px',
    });
  }

  sendData() {
    this.formService.changeData(this.book.id);
  }
}
