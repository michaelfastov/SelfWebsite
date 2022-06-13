import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthInitialComponent } from './auth-initial.component';

describe('AuthInitialComponent', () => {
  let component: AuthInitialComponent;
  let fixture: ComponentFixture<AuthInitialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AuthInitialComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthInitialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
