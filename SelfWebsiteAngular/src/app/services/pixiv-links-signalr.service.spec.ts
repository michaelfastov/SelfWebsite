import { TestBed } from '@angular/core/testing';

import { PixivLinksSignalrService } from './pixiv-links-signalr.service';

describe('PixivLinksSignalrService', () => {
  let service: PixivLinksSignalrService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PixivLinksSignalrService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
