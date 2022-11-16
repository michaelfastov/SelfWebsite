import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PixivLinksComponent } from './pixiv-links.component';

describe('PixivLinksComponent', () => {
  let component: PixivLinksComponent;
  let fixture: ComponentFixture<PixivLinksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PixivLinksComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PixivLinksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
