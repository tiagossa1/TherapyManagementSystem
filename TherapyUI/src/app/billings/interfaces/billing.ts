export interface BillingInterface {
  id: string;
  appointmentId: string;
  clientId: string;
  therapistId: string;
  price: number;
  editing: boolean;
}
