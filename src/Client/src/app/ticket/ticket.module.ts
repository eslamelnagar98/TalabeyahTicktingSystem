import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TicketRoutingModule } from './ticket-routing.module';
import { SharedModule } from '../shared/shared.module';
import { TicketComponent } from './ticket.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    TicketComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    TicketRoutingModule,
    FormsModule
  ]
})
export class TicketModule { }
