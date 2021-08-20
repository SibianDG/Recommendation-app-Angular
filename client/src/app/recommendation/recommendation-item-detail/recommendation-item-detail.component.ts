import { Component, Input, OnInit } from '@angular/core';
import { Item } from 'src/app/item/item-model';
import { RecommendationDataService } from '../recommendationData.service';

@Component({
  selector: 'app-recommendation-item-detail',
  templateUrl: './recommendation-item-detail.component.html',
  styleUrls: ['./recommendation-item-detail.component.scss']
})
export class RecommendationItemDetailComponent implements OnInit {
  @Input() public item: Item;

  constructor(private _dataService: RecommendationDataService) {  }

  public deleteItem(){
    this._dataService.deleteItem(this.item.title);
  }

  ngOnInit() {   }

}
