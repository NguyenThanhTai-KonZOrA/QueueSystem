export interface RegisterNewUserRequest {
  fullName: string;
  phone: string;
  email: string;
  counterId: number;
}

export interface RegisterMemberRequest {
  memberId: string;
  counterId: number;
}

export interface TicketResponse {
  ticketId: number;
  patronId: number;
  counterId: number;
  ticketNumber: number;
  ticketDate: string;
  status: string;
  qrCodeUrl: string;
  message: string;
}
