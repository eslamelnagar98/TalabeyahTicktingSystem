import { HttpParams } from "@angular/common/http";
import { TicketParams } from "../models/ticketParams";

export class Utilities {
  concatQueryParams(queryParams: TicketParams): HttpParams {
    let params: HttpParams = new HttpParams();
    Object.keys(queryParams).forEach(key => {
      if (queryParams.hasOwnProperty(key) && queryParams[key]) {
        const value = queryParams[key];
        if (value !== 0) {
          params = params.append(key, value.toString());
        }
      }
    });
    return params

  }
}
