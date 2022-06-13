import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ResumeChildrenInteractionService {
  private saveActivatedSource = new Subject<void>();
  saveActivated$ = this.saveActivatedSource.asObservable();

  constructor() { }

  updateChildren() {
    this.saveActivatedSource.next();
  }
}
