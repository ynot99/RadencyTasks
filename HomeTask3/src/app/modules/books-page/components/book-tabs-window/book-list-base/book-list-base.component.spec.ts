import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookListBaseComponent } from './book-list-base.component';

describe('BookListBaseComponent', () => {
  let component: BookListBaseComponent;
  let fixture: ComponentFixture<BookListBaseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BookListBaseComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookListBaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
