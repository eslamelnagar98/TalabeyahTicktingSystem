export interface ITicket {
  id: number;
  phoneNumber: string;
  governorate: string;
  city: string;
  district: string;
  isHandled: boolean;
  creationDate: Date;
}
