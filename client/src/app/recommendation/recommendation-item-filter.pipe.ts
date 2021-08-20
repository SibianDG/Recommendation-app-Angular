import { Pipe, PipeTransform } from '@angular/core';
import { Item } from '../item/item-model';

@Pipe({
  name: 'recommendationItemFilter'
})
export class RecommendationItemFilterPipe implements PipeTransform {

  transform(items: Item[], stringValue: string): Item[] {

    if (stringValue){
      stringValue = stringValue.trim().toLowerCase();

      if (stringValue.length === 0) {
        return items;
      }

      return items.filter(item => {
        return item.title.toLowerCase().includes(stringValue)
      });
    } else {
      return items;
    }
  }

}
