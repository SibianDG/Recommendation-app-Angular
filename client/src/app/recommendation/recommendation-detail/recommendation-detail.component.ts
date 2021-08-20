import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-recommendation-detail',
  templateUrl: './recommendation-detail.component.html',
  styleUrls: ['./recommendation-detail.component.scss']
})
export class RecommendationDetailComponent implements OnInit {
  @Input() public keywords: Array<string>;

  constructor() {  }

  ngOnInit() {  }

}
