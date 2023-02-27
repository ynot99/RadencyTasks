import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

import { ISaveBook } from 'src/app/modules/shared/interfaces/';
import { BookService } from '../../services/book.service';
import { FormService } from '../../services/form.service';
import { SignalService } from '../../services/signal.service';

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.scss'],
})
export class EditBookComponent implements OnInit {
  @ViewChild('nextInput') nextInput!: ElementRef;
  form: FormGroup;
  selectedFileName: string;
  selectedFileData: string;
  isFormLoading: boolean;
  isEditMode: boolean;
  // BAD
  editableBookId: number;

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private bookService: BookService,
    private formService: FormService,
    private signalService: SignalService
  ) {
    this.editableBookId = 0;
    this.isEditMode = false;
    this.isFormLoading = false;
    this.selectedFileName = '';
    this.selectedFileData = '';
    this.form = this.fb.group({
      title: ['', Validators.required],
      genre: ['', Validators.required],
      author: ['', Validators.required],
      content: [
        '',
        [
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(1000),
        ],
      ],
      coverName: [''],
    });
  }

  ngOnInit(): void {
    // to make behavior of form more predictable
    this.formReset();
    // FIX it sends unnecessary request
    this.formService.currentData.subscribe((bookId: number) => {
      // TMP
      if (typeof bookId !== 'number') return;
      this.bookService.getBookById(bookId).subscribe((_data) => {
        const data = _data.data;
        this.editableBookId = data.id;
        this.selectedFileData = data.cover;
        this.form = new FormGroup({
          cover: new FormControl(data.cover),
          content: new FormControl(data.content),
          genre: new FormControl(data.genre),
          title: new FormControl(data.title),
          author: new FormControl(data.author),
          coverName: new FormControl('Old cover'),
        });
        this.isEditMode = true;
      });
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action);
  }

  onSubmit(fileInput: HTMLInputElement) {
    this.form.get('title')?.updateValueAndValidity();
    this.form.get('cover')?.updateValueAndValidity();
    this.form.get('coverName')?.updateValueAndValidity();
    if (this.selectedFileName !== '') {
      this.form.get('coverName')?.setErrors(null);
    }
    this.form.get('genre')?.updateValueAndValidity();
    this.form.get('author')?.updateValueAndValidity();
    this.form.get('content')?.updateValueAndValidity();
    if (!this.form.valid) {
      this.snackBar.open('Please fix all the errors and press "Add"', 'OK');
      return;
    }
    const re = new RegExp(
      '^data:image\\/(png|jpg|jpeg);base64,([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)?$'
    );
    re.test(this.selectedFileData);
    this.isFormLoading = true;

    const body: ISaveBook = {
      id: this.isEditMode ? this.editableBookId : undefined,
      title: this.form.value.title,
      cover: this.selectedFileData,
      genre: this.form.value.genre,
      author: this.form.value.author,
      content: this.form.value.content,
    };

    this.bookService.saveBook(body).subscribe({
      next: () => {
        this.formReset(fileInput);
        this.openSnackBar('Book saved', 'OK');
        if (this.isEditMode) {
          this.editableBookId = 0;
          this.isEditMode = false;
        }
        // BAD unexpected behavior
        // sends signal for reloading books list
        this.signalService.sendSignal(true);
      },
      error: () => {
        this.isFormLoading = false;
        this.openSnackBar('Error saving book', 'OK');
      },
      complete: () => {
        this.isFormLoading = false;
      },
    });
  }

  formReset(fileInput: HTMLInputElement | null = null) {
    this.form.reset();
    this.selectedFileName = '';
    this.selectedFileData = '';
    // To not show errors on reset
    if (fileInput != null) fileInput.value = '';
    this.form.get('title')?.markAsPending();
    this.form.get('coverName')?.markAsPending();
    this.form.get('genre')?.markAsPending();
    this.form.get('author')?.markAsPending();
    this.form.get('content')?.markAsPending();
  }

  onFileSelected(fileInput: HTMLInputElement) {
    const file: File | undefined = fileInput.files?.[0];
    if (file) {
      const allowedTypes = ['image/jpeg', 'image/png', 'image/jpg'];
      if (!allowedTypes.includes(file.type)) {
        this.snackBar.open('Please select an image file', 'OK');
        return;
      }
      this.convertFileToBase64(file).then((base64) => {
        this.selectedFileName = file.name;
        this.selectedFileData = base64;
        this.nextInput.nativeElement.focus();
        this.form.get('coverName')?.markAsPending();
      });
    } else {
      this.selectedFileName = '';
      this.selectedFileData = '';
      this.form.get('coverName')?.updateValueAndValidity();
    }
  }

  convertFileToBase64(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result as string);
      reader.onerror = (error) => reject(error);
    });
  }
}
