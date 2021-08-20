/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { RecommendationDataService } from './recommendationData.service';

describe('Service: RecommendationData', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RecommendationDataService]
    });
  });

  it('should ...', inject([RecommendationDataService], (service: RecommendationDataService) => {
    expect(service).toBeTruthy();
  }));
});
