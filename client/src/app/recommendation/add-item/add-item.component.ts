import { HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ItemDataService } from 'src/app/item/item-data.service';
import { Item } from 'src/app/item/item-model';

@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.scss']
})
export class AddItemComponent implements OnInit {
  @Output() public newItem = new EventEmitter<Item>();
  public item: FormGroup;
  public readonly types = ['Book', 'Movie', 'Serie'];
  public selectedType: string = '';
  public errorMessage: string;
  public fetching: boolean = false;

  constructor(private fb: FormBuilder, private _dataService: ItemDataService) { }

  onSubmit() {
    this.newItem.emit(new Item(
      this.item.value.title,
      this.item.value.summary,
      this.item.value.image,
      this.item.value.url,
      this.item.value.pages,
      this.item.value.duration,
      this.item.value.numberOfEpisodes,
      this.selectedType
    ));
  }

  ngOnInit() {
    this.item = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(2)]],
      summary: ['', [Validators.required, Validators.minLength(5)]],
      image: [''],
      url: [''],
      pages: [''],
      duration: [''],
      numberOfEpisodes: [''],
    });
  }

  async fetchURL(){
    this.fetching = true;
    console.log(this.item.value.url);
    
    try {
      this._dataService.fetchItem(this.item.value.url)
      .then((item: Item) => {
        console.log(item.title);
        this.item.controls['title'].setValue(item.title);
        this.item.controls['summary'].setValue(item.summary);
        this.item.controls['pages'].setValue(item.pages);
        this.item.controls['image'].setValue(item.image);
        this.selectedType = item.typeName.toLowerCase();
      })
      .then(() => this.fetching = false)
      .catch((err) => {
        this.errorMessage = "Failed posting the recommendation. Please try again."
        this.errorMessage += "\n"+ this.handleError(err);
      });
    } catch (e) {
      this.errorMessage = "Failed posting the recommendation. Please try again."
    } 
  }

  getErrorMessage(errors: any): string {
    if (errors.required) {
      return 'is required';
    } else if (errors.minlength) {
      return `needs at least ${errors.minlength.requiredLength} 
        characters (got ${errors.minlength.actualLength})`;
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
