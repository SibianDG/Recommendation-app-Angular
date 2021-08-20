import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { Item } from 'src/app/item/item-model';
import { Recommendation } from '../recommendation.model';
import { RecommendationDataService } from '../recommendationData.service';

@Component({
  selector: 'app-recommendation-list',
  templateUrl: './recommendation-list.component.html',
  styleUrls: ['./recommendation-list.component.scss']
})
export class RecommendationListComponent {
  private _isRecmmomendationValid: boolean = false;
  public filterItemName: string;
  public filterItem$ = new Subject<string>();
  public addNewRecommendationString: string;

  constructor(private _recommendationsService: RecommendationDataService) {
    this.filterItem$
    .pipe(
      distinctUntilChanged(),
      debounceTime(500),
      map(val => val.toLowerCase())
    )
    .subscribe(val => (this.filterItemName = val));
  }

  applyFilter(filter: string) {
    this.filterItemName = filter;
  }

  get keywords(): Array<string> {
    return this._recommendationsService.keywords;
  }

  get items(): Array<Item> {
    return this._recommendationsService.items;
  }

  get isRecommendationValid(){
    return this._isRecmmomendationValid;
  }

  addNewKeywords(keywords: Array<string>) {
    this._recommendationsService.addKeywords(keywords);
    this._isRecmmomendationValid = this._recommendationsService.isRecommendationValid();
  }

  addNewItem(item: Item) {
    this._recommendationsService.addItem(item);
    this._isRecmmomendationValid = this._recommendationsService.isRecommendationValid();
  }

  async addThisRecommendation(){
    try {
      let answerFromAPI = this._recommendationsService.addFullRecommendation()
        .then((answer) => {
          if (typeof(answer) === "number"){
            this.addNewRecommendationString = "Successfully added this recommendation";
          } else {
            this.addNewRecommendationString = "Failed posting the recommendation. Please try again."
          }
      })
      .catch((err) => {
        this.addNewRecommendationString = "Failed posting the recommendation. Please try again."
        this.addNewRecommendationString += "\n"+ this.handleError(err);
      });
    } catch (e) {
      this.addNewRecommendationString = "Failed posting the recommendation. Please try again."
    }
  }

  handleError(err: any): string {
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else if (err instanceof HttpErrorResponse) {
      errorMessage = `'${err.status} ${err.statusText}' when accessing '${err.url}'`;
    } else {
      errorMessage = err;
    }
    return errorMessage;
  }
}
