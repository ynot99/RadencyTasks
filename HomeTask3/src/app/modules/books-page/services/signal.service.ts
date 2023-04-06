import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SignalService {
  private subject = new Subject<boolean>();

  sendSignal(value: boolean) {
    this.subject.next(value);
  }

  getSignal() {
    return this.subject.asObservable();
  }
}
