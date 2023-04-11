import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTabsModule } from '@angular/material/tabs';

import { MatSnackBar } from '@angular/material/snack-bar';

import { BookListBaseComponent } from './components/book-tabs-window/book-list-base/book-list-base.component';
import { BookListComponent } from './components/book-tabs-window/book-list/book-list.component';
import { BookTabsWindowComponent } from './components/book-tabs-window/book-tabs-window.component';
import { RecommendedBookListComponent } from './components/book-tabs-window/recommended-book-list/recommended-book-list.component';
import { BookListItemComponent } from './components/book-tabs-window/shared/book-list-item/book-list-item.component';
import { BooksPageComponent } from './components/books-page.component';
import { EditBookComponent } from './components/edit-book/edit-book.component';
import { ViewBookComponent } from './components/view-book/view-book.component';
import { BookService } from './services/book.service';
import { FormService } from './services/form.service';
import { SignalService } from './services/signal.service';

@NgModule({
  declarations: [
    BooksPageComponent,
    BookListComponent,
    BookListItemComponent,
    EditBookComponent,
    ViewBookComponent,
    BookTabsWindowComponent,
    RecommendedBookListComponent,
    BookListBaseComponent,
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
    MatProgressSpinnerModule,
  ],
  exports: [BooksPageComponent],
  providers: [MatSnackBar, BookService, FormService, SignalService],
})
export class BooksPageModule {}
