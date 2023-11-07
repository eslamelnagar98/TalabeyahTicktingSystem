import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { TicketParams } from '../shared/models/ticketParams';
import { Utilities } from '../shared/Helpers/Utilities';
import { IPagination } from '../shared/models/pagination';
import { map } from 'rxjs/operators';
import { ITicketAddress } from '../shared/models/ticketAddress';
import { ITicket } from '../shared/models/ticket';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  baseUrl = environment.apiUrl;
  pageSize = 5;
  constructor(private http: HttpClient) { }

  getTickets(ticketParams: TicketParams) {
    ticketParams.pageSize = this.pageSize;
    let utilities = new Utilities();
    let params = utilities.concatQueryParams(ticketParams);
    return this.http.get<IPagination>(`${this.baseUrl}Ticket/Tickets`, { observe: 'response', params })
      .pipe(
        map(response => {
          return response.body;
        }))
  }

  getTicketAddresses() {
    return this.http.get<ITicketAddress>(`${this.baseUrl}Ticket/TicketsAddress`)
  }

  createTicket(ticket: ITicket) {
    return this.http.post(`${this.baseUrl}Ticket`, ticket);
  }
}
