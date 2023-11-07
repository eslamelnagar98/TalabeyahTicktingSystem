import { ITicket } from "./ticket";
export interface IPagination {
  pageIndex: number;
  pageSize: number;
  count: number;
  tickets: Array<ITicket>;
}
