import { TestBed } from '@angular/core/testing';

import { RedisCacheService } from './redis-cache.service';

describe('RedisCacheService', () => {
  let service: RedisCacheService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RedisCacheService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
