
export class ChangePromotionRequest {
  promotionId: string;
  name: string;
  active: boolean;

  constructor(promotionId: string,  name: string, active: boolean) {
    this.promotionId = promotionId;
    this.name = name;
    this.active = active;
  }
}
