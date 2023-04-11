import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookTabsWindowComponent } from './book-tabs-window.component';

describe('BookTabsWindowComponent', () => {
  let component: BookTabsWindowComponent;
  let fixture: ComponentFixture<BookTabsWindowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BookTabsWindowComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookTabsWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
