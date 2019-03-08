export interface AppointmentInterface {
    id: string;
    appointmentTypeId: string;
    clientId: string;
    therapistId: string;
    appointmentDate: Date;
    appointmentTime: string;
    editing: boolean;
}
