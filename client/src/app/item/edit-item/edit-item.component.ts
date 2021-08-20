import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Item } from '../item-model';

@Component({
  selector: 'app-edit-item',
  templateUrl: './edit-item.component.html',
  styleUrls: ['./edit-item.component.scss']
})
export class EditItemComponent implements OnInit {
  @Input() public item: Item;
  @Output() public editedItem = new EventEmitter<Item>();
  public editItemForm: FormGroup;
  public readonly types = ['Book', 'Movie', 'Serie'];
  public selectedType: string;

  constructor(private fb: FormBuilder) {
  }

  onSubmit() {
    this.editedItem.emit(new Item(
      this.editItemForm.value.title,
      this.editItemForm.value.summary,
      this.editItemForm.value.image,
      this.editItemForm.value.url,
      this.editItemForm.value.pages,
      this.editItemForm.value.duration,
      this.editItemForm.value.numberOfEpisodes,
      this.selectedType,
      this.item.itemId
    ));
  }

  ngOnInit() {
    this.selectedType = this.item.typeName;
    
    this.editItemForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(2)]],
      summary: ['', [Validators.required, Validators.minLength(5)]],
      image: [''],
      url: [''],
      pages: [''],
      duration: [''],
      numberOfEpisodes: [''],
    });
    this.editItemForm.patchValue({
      title: this.item.title, 
      summary: this.item.summary,
      image: this.item.image,
      url: this.item.url,
    });

    if (this.item.pages){
      this.editItemForm.patchValue({
        pages: this.item.pages,
      });
    }
    if (this.item.duration){
      this.editItemForm.patchValue({
        duration: this.item.duration,
      });
    }
    if (this.item.numberOfEpisodes){
      this.editItemForm.patchValue({
        numberOfEpisodes: this.item.numberOfEpisodes,
      });
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

}
