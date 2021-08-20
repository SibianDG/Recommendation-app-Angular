import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Item } from './item-model';

@Injectable({
  providedIn: 'root'
})
export class ItemDataService {

  private _items$ = new BehaviorSubject<Item[]>([]);
  private _items : Array<Item> = new Array<Item>();

  constructor(private _httpClient : HttpClient) { }

  getNewRecommendationItem(keywords: string = null, type: string = null) : any {
    const apiBase: string = `${environment.apiUrl}/Items/getrecommendationitem`;
    let api: string = `${environment.apiUrl}/Items/getrecommendationitem`;

    if (keywords && !(keywords.trim() === "")){
      
      if (api === apiBase) {
        api += "?";
      } else {
        api += "&";
      }
      api = api + "keywords=" + keywords.replace(/\s/g, "");
    }

    if (type){
      
      if (api === apiBase) {
        api += "?";
      } else {
        api += "&";
      }
      api += "type=" + type;
    }
    
    return new Promise((resolve, reject) => {
      this._httpClient.get(api)
      .pipe(
        catchError((err) => {
          reject(err);
          return this.handleError(err);
        }),
        map((json: any) => Item.fromJSON(json))
      )
      .subscribe((recommendationItem : Item) => {
        this._items = [...this._items, recommendationItem];
        this._items$.next(this._items);
        resolve(recommendationItem.itemId);
      });
    })
  }

  getRecommendationBy(id: number): Promise<Item> {
    return new Promise((resolve, reject) => {
      this._httpClient.get(`${environment.apiUrl}/Items/${id}`)
      .pipe(
        catchError((err) => {
          reject(err);
          return this.handleError(err);
        }),
        map((json: any) => Item.fromJSON(json))
      )
      .subscribe((item : Item) => {
        resolve(item);
      });
    })
  }

  edit(item: Item): Promise<Item> {
    return new Promise((resolve, reject) => {
      this._httpClient.put(`${environment.apiUrl}/${item.typeName}/${item.itemId}`, item.toJSONForEdit())
      .pipe(
        catchError((err) => {
          reject(err);
          return this.handleError(err);
        }),
      )
      .subscribe((json : any) => {
        if (json)
          reject(json)
        resolve(json);
      });
    })
  }

  get recommendations() : Item[] {
    return this._items;
  }

  deleteRecommendation(id: number): void {
    this._items = this._items.filter((x) => x.itemId !== id);
  }

  async fetchItem(url: any): Promise<Item> {

    return new Promise((resolve, reject) => {
      let params = new HttpParams();
      params = params.append('link', url);
      this._httpClient.get(`${environment.apiUrl}/Items/FetchURL/`, {params: params})
      .pipe(
        catchError((err) => {
          reject(err);
          return this.handleError(err);
        }),
        map((json: any) => Item.fromJSON(json))
      )
      .subscribe((recommendationItem : Item) => {
        resolve(recommendationItem);
      });
    })

  }

  handleError(err: any) {
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
}
