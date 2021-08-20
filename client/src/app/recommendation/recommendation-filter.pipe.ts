import { Pipe, PipeTransform } from '@angular/core';
import { Item } from '../item/item-model';

@Pipe({
  name: 'recommendationFilter'
})
export class RecommendationFilterPipe implements PipeTransform {

  transform(items: Array<Item>, stringValue: string): Array<Item> {

    if (stringValue){
      stringValue = stringValue.trim().toLowerCase();

      if (stringValue.length === 0) {
        return items;
      }

      return items.filter(item => {
        item.title.toLowerCase().includes(stringValue)
        ||
        item.typeName.toLowerCase().includes(stringValue)
      });
    } else {
      return items;
    }
  }

}
