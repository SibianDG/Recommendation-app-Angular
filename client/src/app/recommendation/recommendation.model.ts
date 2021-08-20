import { Item, ItemJson } from "../item/item-model";

interface RecommendationJson {
  rating: number;
  keywords: string[];
  items: ItemJson[];
}
export class Recommendation {

  // tslint:disable-next-line:variable-name
    private _recommendationId: number;
  // tslint:disable-next-line:variable-name
    private _rating: number;
    // private _items: Item[];
  // tslint:disable-next-line:variable-name
    private _keywords: string[];
    private _items: Array<Item>;


  constructor(keywords: string[], items: Array<Item>, recommendationId?: number, rating?: number,) {
    this._keywords = keywords;
    this._items = items;
    this._recommendationId = recommendationId;
    if (!rating)
      rating = null;
    this._rating = rating;
  }


  get recommendationId(): number {
    return this._recommendationId;
  }

  get rating(): number {
    return this._rating;
  }

  get keywords(): string[] {
    return this._keywords;
  }

  get items(): Array<Item> {
    return this._items;
  }

  static fromJSON(json: {recommendationId: number, rating: number, items: Array<Item>, keywords: string[]}): Recommendation {
    return new Recommendation(json.keywords, json.items, json.recommendationId, json.rating,);
  }
  toJSON(): RecommendationJson {
    return <RecommendationJson>{
      rating: this.rating,
      keywords: this.keywords,
      items: this.items.map(i => i.toJSON()),
    };
  }
}
