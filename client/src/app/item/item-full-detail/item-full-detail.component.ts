import { HttpErrorResponse } from '@angular/common/http';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ItemDataService } from '../item-data.service';
import { Item } from '../item-model';

@Component({
  selector: 'app-item-full-detail',
  templateUrl: './item-full-detail.component.html',
  styleUrls: ['./item-full-detail.component.scss']
})
export class ItemFullDetailComponent implements OnInit {
  public item: Item = {} as Item;
  public error: string;
  public isLoading: boolean;
  public edit: boolean = false;
  public successMessage: string;

  public icon: string;
  private _map = new Map([
    ['book', 'book'],
    ['movie', 'movie'],
    ['serie', 'slideshow']
  ]);

  constructor(private route: ActivatedRoute, private itemDataService: ItemDataService) {
    this.isLoading = true;
    try {
      const id = parseInt(this.route.snapshot.paramMap.get('id'));
      this.itemDataService.getRecommendationBy(id)
      .then((item) => {
        try {
          this.item = item;
        } catch (e) {
          this.error = "Failed getting the recommendation. Please try again."
        }
      })
      .then(() => {
        this.isLoading = false;
        this.icon = this._map.get(this.item.typeName.toLowerCase());
      })
      .catch((err) => {
        this.error = "Failed getting the recommendation. Please try again."
        this.error += "\n"+ this.handleError(err);
      });
    } catch (e) {
      this.error = "Failed getting the recommendation. Please try again."
    }
  }

  wantEdit(){
    this.edit = true;
  }

  editItem(item: Item){
    try {
      this.itemDataService.edit(item)
      .then(() => {
        this.successMessage = "You have successfully changed the item."
      })
      .catch((err) => {
        this.error = "Failed editing the recommendation. Please try again."
        this.error += "\n"+ this.handleError(err);
      });
    } catch (e) {
      this.error = "Failed editing the item. Please try again."
    }
  }

  ngOnInit() {
    this.isLoading = false;
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
