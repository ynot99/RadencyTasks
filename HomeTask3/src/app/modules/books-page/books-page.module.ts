import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatTabsModule } from '@angular/material/tabs';

import { MatSnackBar } from '@angular/material/snack-bar';

import { BookListItemComponent } from './book-list/book-list-item/book-list-item.component';
import { BookListComponent } from './book-list/book-list.component';
import { BooksPageComponent } from './books-page.component';
import { EditBookComponent } from './edit-book/edit-book.component';
import { ViewBookComponent } from './view-book/view-book.component';

@NgModule({
  declarations: [
    BooksPageComponent,
    BookListComponent,
    BookListItemComponent,
    EditBookComponent,
    ViewBookComponent,
  ],
  imports: [
    CommonModule,

    MatDialogModule,
    MatGridListModule,
    MatInputModule,
    MatCardModule,
    ReactiveFormsModule,
    MatDividerModule,
    MatButtonModule,
    MatListModule,
    MatTabsModule,
    MatIconModule,
    HttpClientModule,
  ],
  exports: [BooksPageComponent],
  providers: [MatSnackBar, MatDialog],
  entryComponents: [BookListItemComponent, ViewBookComponent],
})
export class BooksPageModule {}
