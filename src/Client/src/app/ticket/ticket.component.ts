import { Component, OnInit } from '@angular/core';
import { TicketParams } from '../shared/models/ticketParams';
import { ITicket } from '../shared/models/ticket';
import { TicketService } from './ticket.service';
import { IPagination } from '../shared/models/pagination';
import { ITicketAddress } from '../shared/models/ticketAddress';
@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent implements OnInit {
  totalCount: number = 0;
  ticketParams = new TicketParams();
  tickets: Array<ITicket> = [];
  governorate: Array<string> = [];
  cities: Array<string> = [];
  districts: Array<string> = [];
  governorateSelectedOption: string = '';
  citiesSelectedOption: string = '';
  districtsSelectedOption: string = '';
  phoneNumber: string = '';
  constructor(private ticketService: TicketService) { }

  ngOnInit(): void {
    this.getTicketAddresses();
    this.getTickets();
  }

  getTickets() {
    this.ticketService.getTickets(this.ticketParams).subscribe({
      next: (response: IPagination | null) => {
        if (response !== null) {
          this.tickets = response.tickets;
          this.totalCount = response.count;
        }
      },
      error: (error) => console.error(error)
    });
  }

  getTicketAddresses() {
    this.ticketService.getTicketAddresses().subscribe({
      next: (response: ITicketAddress) => {
        this.governorate = response.governorates;
        this.cities = response.cities;
        this.districts = response.districts
        this.governorateSelectedOption = this.governorate[0];
        this.citiesSelectedOption = this.cities[0];
        this.districtsSelectedOption = this.districts[0];
        console.log(this.districts[0]);
      }
    })
  }
  onPageChanged(event: any) {
    if (this.ticketParams.pageIndex != event) {
      this.ticketParams.pageIndex = event;
      this.getTickets();
    }
  }
  createTicket() {

    const ticket: ITicket = {
      id: 0,
      city: this.citiesSelectedOption,
      governorate: this.governorateSelectedOption,
      district: this.districtsSelectedOption,
      phoneNumber: this.phoneNumber,
      isHandled: false,
      creationDate: new Date()
    };

    this.ticketService.createTicket(ticket).subscribe({
      next: () => console.log('ticket added successfully')
    });

  }
}
