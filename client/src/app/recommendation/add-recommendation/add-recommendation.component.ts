import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Recommendation } from '../recommendation.model';

@Component({
  selector: 'app-add-recommendation',
  templateUrl: './add-recommendation.component.html',
  styleUrls: ['./add-recommendation.component.scss']
})
export class AddRecommendationComponent implements OnInit {
  @Output() public newKeywords = new EventEmitter<Array<string>>();
  public keywordsFG: FormGroup;

  constructor() { }

  onSubmit() {
    let keywords = this.keywordsFG.value.keywordslist;
    keywords = keywords.replace(/\s/g, "");

    let keywordsArray: Array<string>;

    if (keywords && keywords.trim() !== ""){
      keywordsArray = keywords.split(',');
      this.newKeywords.emit(keywordsArray);
    }
  }

  ngOnInit() {
    this.keywordsFG = new FormGroup({
      keywordslist: new FormControl('',
      [Validators.required, Validators.minLength(3)])
    });
  }

  getErrorMessage(errors: any): string {
    if (errors.required) {
      return 'is required';
    } else if (errors.minlength) {
      return `needs at least ${errors.minlength.requiredLength} 
        characters (got ${errors.minlength.actualLength})`;
    }
  }

}
