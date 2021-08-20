import { Component, Input, OnInit } from '@angular/core';
import { Item } from '../item-model';
import { ItemDataService } from '../item-data.service';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.scss']
})
export class ItemDetailComponent implements OnInit {
  private _map = new Map([
    ['book', 'book'],
    ['movie', 'movie'],
    ['serie', 'slideshow']
  ]);

  @Input() public item: Item;
  public icon: string;
  public link: string = "/details/";

  constructor(private _dataService: ItemDataService) {
  }

  public deleteRecommendation(){
    this._dataService.deleteRecommendation(this.item.itemId);
  }

  ngOnInit() {
    this.icon = this._map.get(this.item.typeName.toLowerCase());
    this.link += this.item.itemId;
  }

}
