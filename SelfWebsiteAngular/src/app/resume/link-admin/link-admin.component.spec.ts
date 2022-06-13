import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LinkAdminComponent } from './link-admin.component';

describe('LinkAdminComponent', () => {
  let component: LinkAdminComponent;
  let fixture: ComponentFixture<LinkAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LinkAdminComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LinkAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
