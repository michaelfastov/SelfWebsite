import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResumeAdminComponent } from './resume-admin.component';

describe('ResumeAdminComponent', () => {
  let component: ResumeAdminComponent;
  let fixture: ComponentFixture<ResumeAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResumeAdminComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResumeAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
