import { TestBed } from '@angular/core/testing';

import { ResumeChildrenInteractionService } from './resume-children-interaction.service';

describe('ResumeChildrenInteractionService', () => {
  let service: ResumeChildrenInteractionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ResumeChildrenInteractionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
