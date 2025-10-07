import axios from "axios";
import type { RegisterMemberRequest, RegisterNewUserRequest, TicketResponse } from "../type";

const api = axios.create({
  baseURL: "https://localhost:7212/api/Queue", // đổi thành URL backend của bạn
  headers: { "Content-Type": "application/json" }
});

type ApiEnvelope<T> = {
  status: number;
  data: T;
  success: boolean;
};

function unwrapApiEnvelope<T>(response: { data: ApiEnvelope<T> }): T {
  if (!response.data.success) {
    throw new Error("API call failed");
  }
  return response.data.data;
}

export const queueService = {
  registerNewUser: async (data: RegisterNewUserRequest): Promise<TicketResponse> => {
    const res = await api.post("/register", data);
    return unwrapApiEnvelope(res);
  },
  registerByMember: async (data: RegisterMemberRequest): Promise<TicketResponse> => {
    const res = await api.post("/register-member", data);
    return unwrapApiEnvelope(res);
  }
};
