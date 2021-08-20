import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { RecommendationListComponent } from './recommendation-list/recommendation-list.component';
import { RecommendationFilterPipe } from './recommendation-filter.pipe';
import { RecommendationDetailComponent } from './recommendation-detail/recommendation-detail.component';
import { RecommendationItemDetailComponent } from './recommendation-item-detail/recommendation-item-detail.component';
import { AddRecommendationComponent } from './add-recommendation/add-recommendation.component';
import { AddItemComponent } from './add-item/add-item.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RecommendationItemFilterPipe } from './recommendation-item-filter.pipe';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../user/auth.guard';

const routes: Routes = [
  { path: 'add-recommendation',
  component: RecommendationListComponent,
  canActivate: [AuthGuard],
  }
]

@NgModule({
  declarations: [		
    AddItemComponent,
    AddRecommendationComponent,
    RecommendationListComponent,
    RecommendationDetailComponent,
    RecommendationItemDetailComponent,
    RecommendationFilterPipe,
    RecommendationItemFilterPipe
   ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)

  ],
  exports: [
    RecommendationListComponent,
    RecommendationDetailComponent,
    RecommendationItemDetailComponent,
    AddRecommendationComponent,
    AddItemComponent,
  ]
})
export class RecommendationModule { }
