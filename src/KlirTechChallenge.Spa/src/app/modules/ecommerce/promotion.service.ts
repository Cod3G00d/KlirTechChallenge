import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RestService } from 'src/app/core/services/http/rest.service';
import { Promotion } from 'src/app/core/models/Promotion';
import { ChangePromotionRequest } from 'src/app/core/models/requests/ChangeQuoteRequest copy';


@Injectable({
  providedIn: 'root'
})
export class PromotionService extends RestService {

  controllerName = "promotions";

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  public getPromotions(): Promise<Promotion[]>{
    return this.get(this.controllerName)
    .toPromise();
  }

  public changePromotion(request: ChangePromotionRequest): Promise<any>{
    return this.put(this.controllerName, request)
    .toPromise();
  }

}
