import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { AuthGuard } from './user/auth.guard';
import { SelectivePreloadStrategy } from './SelectivePreloadStrategy';

const appRoutes: Routes = [
  {
    path: 'items',
    canActivate: [AuthGuard],
    loadChildren: () =>
      import('./item/item.module').then((mod) => mod.ItemModule),
    data: { preload: true },
  },
  {
    path: 'recommendations',
    canActivate: [AuthGuard],
    loadChildren: () =>
      import('./recommendation/recommendation.module').then((mod) => mod.RecommendationModule),
    data: { preload: true },
  },
  { path: '', redirectTo: 'items', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent },
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(appRoutes, {
      preloadingStrategy: SelectivePreloadStrategy,
    }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
