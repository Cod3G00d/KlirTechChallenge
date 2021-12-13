import { Component, OnInit, ViewChild } from '@angular/core';
import { PromotionService } from '../../promotion.service';
import { faPlusCircle } from '@fortawesome/free-solid-svg-icons';
import { CartComponent } from '../cart/cart.component';
import { OrderService } from '../../order.service';
import { Router } from '@angular/router';
import { Promotion } from 'src/app/core/models/Promotion';
import { AuthService } from 'src/app/core/services/auth.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';
import { CurrencyNotificationService } from 'src/app/core/services/currency-notification.service';
import { appConstants } from 'src/app/core/constants/appConstants';
import { Quote, QuoteItem } from 'src/app/core/models/Quote';
import { Console } from 'console';
import { ChangePromotionRequest } from 'src/app/core/models/requests/ChangeQuoteRequest copy';

@Component({
  selector: 'app-promotions',
  templateUrl: './promotion-selection.component.html',
  styleUrls: ['./promotion-selection.component.scss'],
})
export class PromotionSelectionComponent implements OnInit {

  @ViewChild('cart') cartDetails!: CartComponent;
  customerId!: string;
  promotions!: Promotion[];
  faPlusCircle = faPlusCircle;

  constructor(
    private authService: AuthService,
    private promotionService: PromotionService,
    private localStorageService: LocalStorageService,
  ) {}

  ngOnInit() {

    if(this.authService.currentCustomer) {
      this.customerId = this.authService.currentCustomer.id;
      this.loadPromotions();
    }

  }

  async loadPromotions() {
    this.promotionService
    .getPromotions()
    .then(
      (result: any) => {
        this.promotions = result.data;
     
      },
    );
  }

  async changePromotion(promotion : Promotion, e : any) {
    let request = new ChangePromotionRequest(promotion.id,promotion.name,e.target.checked)
    this.promotionService.changePromotion(request).then(
      async (result: any) => {
        await this.promotionService.getPromotions()
        .then(
          (result: any) => {
            this.promotions = result.data;
          }
        );
      }
    );

  }
}
 
    
  

