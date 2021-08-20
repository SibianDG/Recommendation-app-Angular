
export interface ItemJson {
  rating: string;
  keyword: string[];
  items: Item[];
}
export class Item {

  // tslint:disable-next-line:variable-name
  private _itemId: number;
  // tslint:disable-next-line:variable-name
  private _title: string;
  // tslint:disable-next-line:variable-name
  private _summary: string;
  // tslint:disable-next-line:variable-name
  private _image: string;
  // tslint:disable-next-line:variable-name
  private _url: string;
  // tslint:disable-next-line:variable-name
  private _pages: number;
  // tslint:disable-next-line:variable-name
  private _duration: number;
  // tslint:disable-next-line:variable-name
  private _numberOfEpisodes: number;
  // tslint:disable-next-line:variable-name
  private _typeName: string;

  // tslint:disable-next-line:max-line-length
  constructor(title: string, summary: string, image: string, url: string, pages: number, duration: number, numberOfEpisodes: number, typeName: string, itemId?: number, ) {
    this._title = title;
    this._summary = summary;
    this._image = image;
    this._url = url;
    this._pages = pages;
    this._duration = duration;
    this._numberOfEpisodes = numberOfEpisodes;
    this._typeName = typeName;
    this._itemId = itemId;
  }

  get itemId(): number {
    return this._itemId;
  }

  get title(): string {
    return this._title;
  }

  get summary(): string {
    return this._summary;
  }

  get image(): string {
    return this._image;
  }

  get url(): string {
    return this._url;
  }

  get pages(): number {
    return this._pages;
  }

  get duration(): number {
    return this._duration;
  }

  get numberOfEpisodes(): number {
    return this._numberOfEpisodes;
  }

  get typeName(): string {
    return this._typeName;
  }

  static fromJSON(json: any): Item {
    // TODO type/type?
    return new Item(json.title, json.summary, json.image, json.url, json.pages, json.duration, json.numberOfEpisodes, json.typeName, json.itemId);
  }
  toJSON(): any {
    return {
      title: this.title,
      summary: this.summary,
      image: this.image,
      url: this.url,
      typeName: this.typeName,
      numberOfEpisodes: this.numberOfEpisodes,
      pages: this.pages,
      duration: this.duration,
    }
  }
  toJSONForEdit(): any {
    return {
      itemId: this.itemId,
      title: this.title,
      summary: this.summary,
      image: this.image,
      url: this.url,
      typeName: this.typeName,
      numberOfEpisodes: this.numberOfEpisodes,
      pages: this.pages,
      duration: this.duration,
    }
  }
}
