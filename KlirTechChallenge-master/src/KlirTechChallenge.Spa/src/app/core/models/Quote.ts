
export class Quote {
  quoteId: string;
  quoteItems: QuoteItem[] = [];
  totalPrice: number;

  constructor(quoteId: string, totalPrice: number) {
    this.quoteId = quoteId;
    this.totalPrice = totalPrice;
  }
}

export class QuoteItem{
  productId: string;
  productName: string;
  productPrice: number;
  productQuantity: number;
  currencySymbol: string;
  totalProductPrice : number;
  promotionName : string;

  constructor(productId: string, productName: string, productPrice: number, productQuantity: number, currencySymbol: string, totalProductPrice : number, promotionName : string) {
    this.productId = productId;
    this.productName = productName;
    this.productQuantity = productQuantity;
    this.productPrice = productPrice;
    this.currencySymbol = currencySymbol;
    this.totalProductPrice = totalProductPrice;
    this.promotionName = promotionName;
  }
}
