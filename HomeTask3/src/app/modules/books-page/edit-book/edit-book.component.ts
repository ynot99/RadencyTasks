import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.scss'],
})
export class EditBookComponent {
  form: FormGroup;
  selectedFileName: string;
  selectedFileData: string;

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.selectedFileName = '';
    this.selectedFileData = '';
    this.form = this.fb.group({
      title: '',
      genre: '',
      author: '',
      content: '',
      cover: '',
    });
  }

  onSubmit() {
    this.form.patchValue({ cover: this.selectedFileData });
    console.log(this.form.value);

    this.http.post('/api/endpoint', this.form.value).subscribe((response) => {
      // handle response
    });
  }

  formReset() {
    this.selectedFileName = '';
    this.selectedFileData = '';
    this.form.reset();
  }

  onFileSelected(fileInput: HTMLInputElement) {
    const files = fileInput.files;
    if (files && files.length > 0) {
      this.selectedFileName = files[0].name;
      this.convertFileToBase64(files[0]).then(
        (base64) => (this.selectedFileData = base64)
      );
    } else {
      this.selectedFileName = '';
      this.selectedFileData = '';
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
