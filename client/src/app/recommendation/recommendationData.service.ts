import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ItemDataService } from '../item/item-data.service';
import { Item } from '../item/item-model';
import { Recommendation } from './recommendation.model';

@Injectable({
  providedIn: 'root'
})
export class RecommendationDataService {

  private _keywords : string[];
  private _items : Item[] = [];

  constructor(private _httpClient : HttpClient) {  }

  addKeywords (keywords: string[]) {
    this._keywords = keywords;
  }

  addItem (item: Item) {
    if (this._items) {
      this._items = [...this._items, item];
    } else {
      this._items = [item]
    }
  }

  addFullRecommendation() : any {
    let recommendation: Recommendation = new Recommendation(this._keywords, this._items);
    
    return new Promise((resolve, reject)=> {
      this._httpClient.post(`${environment.apiUrl}/Recommendation`, recommendation.toJSON())
      .pipe(
        catchError((err) => {
          reject(err);
          return this.handleError(err);
        }),
      )
      .subscribe((recommendation: Recommendation) => {
        resolve(recommendation.recommendationId);
      })
      ;
    })
  }

  get keywords() : Array<string> {
    if (this._keywords){

      return this._keywords;
    } else {
      //TODO: mag weg, maar is nu om te testen
      this._keywords = ["serie", "cool"];
      return this._keywords;
    }
  }

  get items() : Array<Item> {
    return this._items;
  }

  deleteItem(title: string): void {
    this._items = this._items.filter((x) => x.title !== title);
  }

  handleError(err: any): Observable<never> {
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else if (err instanceof HttpErrorResponse) {
      errorMessage = `'${err.status} ${err.statusText}' when accessing '${err.url}'`;
    } else {
      errorMessage = err;
    }
    return throwError(errorMessage);
  }

  isRecommendationValid() {
    return this._keywords.length > 0 && this._items.length > 0;
  }

}
