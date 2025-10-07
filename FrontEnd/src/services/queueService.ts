import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:5001/api/queue", // đổi thành URL backend của bạn
  headers: { "Content-Type": "application/json" }
});

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
  ticketNumber: number;
  qrCodeUrl: string;
  message: string;
}

export const queueService = {
  registerNewUser: async (data: RegisterNewUserRequest): Promise<TicketResponse> => {
    const res = await api.post("/register-new", data);
    return res.data;
  },
  registerByMember: async (data: RegisterMemberRequest): Promise<TicketResponse> => {
    const res = await api.post("/register-member", data);
    return res.data;
  }
};
