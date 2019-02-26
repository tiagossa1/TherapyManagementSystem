import { AppointmentType } from "./AppointmentType";
import { Client } from "./Client";
import { Therapist } from "./Therapist";

export class Appointment {
    id: string;
    appointmentType: AppointmentType;
    client: Client;
    therapist: Therapist
    appointmentDate: Date;
}
