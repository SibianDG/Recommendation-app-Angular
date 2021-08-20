import { NgModule } from '@angular/core';
import { MaterialModule } from '../material/material.module';
import {ItemListComponent} from './item-list/item-list.component';
import {ItemDetailComponent} from './item-detail/item-detail.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { RouterModule, Routes } from '@angular/router';
import { ItemFullDetailComponent } from './item-full-detail/item-full-detail.component';
import { EditItemComponent } from './edit-item/edit-item.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthGuard } from '../user/auth.guard';
const routes: Routes = [
  { path: 'items', component: ItemListComponent,
    canActivate : [AuthGuard]},
  {
    path: 'details/:id',
    component: ItemFullDetailComponent,
    canActivate: [AuthGuard],
  },
]

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    MatProgressSpinnerModule,
    RouterModule.forChild(routes),
  ],
  declarations: [
    ItemListComponent,
    ItemDetailComponent,
    ItemFullDetailComponent,
    EditItemComponent,
  ],
  exports: [
    ItemListComponent,
    ItemDetailComponent,
    ItemFullDetailComponent,
    EditItemComponent,
  ]
})
export class ItemModule { }
