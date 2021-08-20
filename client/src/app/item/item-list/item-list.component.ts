import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { EMPTY, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ItemDataService } from '../item-data.service';
import { Item } from '../item-model';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.scss']
})
export class ItemListComponent implements OnInit {
  types: any = [
      {key: "Doens't matter", value: null}
      , {key: "Book", value: "book"}
      , {key: "Movie", value: "movie"}
      , {key: "Serie", value: "serie"}
  ];
  selected = null;
  private _fetchItems$: Observable<Item[]>;

  public errorMessage: string = '';

  constructor(private _itemService : ItemDataService) { }

  getNewRecommendationItem(keywords: string) : void {
    try {
      this._itemService
        .getNewRecommendationItem(keywords, this.selected)
        .then((answer) => {
          if (typeof(answer) !== "number"){
            this.errorMessage = "Failed posting the recommendation. Please try again."
            this.errorMessage += "\n"+ this.handleError(answer);
          }
        })
        .catch((err) => {
          this.errorMessage = "Failed posting the recommendation. Please try again."
          this.errorMessage += "\n"+ this.handleError(err);
        });
    } catch (e) {
      this.errorMessage = "Failed posting the recommendation. Please try again."
    } 
  }
  
  get recommendations(): Item[] {
    return this._itemService.recommendations;
  }

  ngOnInit(): void {
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
