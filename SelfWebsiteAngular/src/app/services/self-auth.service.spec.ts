import { TestBed } from '@angular/core/testing';

import { SelfAuthService } from './self-auth.service';

describe('AuthService', () => {
  let service: SelfAuthService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SelfAuthService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
