import { Container, Box, Button, Typography, TextField } from "@mui/material";
import { useState } from "react";
import SwipeableViews from 'react-swipeable-views-react-18-fix';
import Step3Result from "../components/Step3Result";
import { queueService } from "../services/queueService";
import type { RegisterNewUserRequest } from "../type";
import MainLayout from "../layout/MainLayout";
export default function QueueWizard() {
  const [step, setStep] = useState(0);
  const [mode, setMode] = useState<"new" | "member" | null>(null);
  const [ticket, setTicket] = useState<any>(null);
  const [memberId, setMemberId] = useState("");
  const [passportId, setPassportId] = useState("");
  const [form, setForm] = useState({ name: "", phone: "", email: "" });
  const [error, setError] = useState<string | null>(null);

  // Giả lập gọi API lấy ticket
  const handleGetTicket = (data: any) => {
    setTicket({
      ticketNumber: Math.floor(Math.random() * 1000),
      qrCodeUrl: "https://your-server.com/qr/" + Math.random().toString(36).slice(2)
    });
    setStep(2);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    const data: RegisterNewUserRequest = {
      fullName: form.name,
      phone: form.phone,
      email: form.email,
      counterId: 1, // Thay đổi giá trị này tùy ý
    };
    try {
      const response = await queueService.registerNewUser(data);
      setTicket({
        ticketNumber: response.ticketNumber,
        qrCodeUrl: response.qrCodeUrl,
      });
      setStep(3); // Chuyển sang trang kết quả
    } catch (error: any) {
      setError("Đăng ký thất bại. Vui lòng thử lại!");
    }
  };

  return (
    <MainLayout>
    <Container
      maxWidth="sm"
      sx={{
        height: "10vh",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        p: 0
      }}
    >
      <Box width="100%">
        <SwipeableViews
          index={step}
          onChangeIndex={newStep => {
            // Nếu chuyển qua tab kết quả mà chưa có ticket thì không cho chuyển
            if (newStep === 3 && !ticket) return;
            setStep(newStep);
          }}
          enableMouseEvents
        >
          {/* Step 1 */}
          <Box textAlign="center" py={8}>
            <Typography variant="h5" mb={4}>Hệ thống lấy số tự động</Typography>
            <Button
              variant="contained"
              size="large"
              onClick={() => setStep(1)}
            >
              🎟️ Lấy số thứ tự
            </Button>
          </Box>

          {/* Step 2: Chọn mode */}
          <Box textAlign="center" py={8}>
            <Typography variant="h6" mb={3}>Tôi có thể giúp gì cho bạn?</Typography>
            <Box display="flex" flexDirection={{ xs: "column", sm: "row" }} gap={2} justifyContent="center" mb={4}>
              <Button
                variant="outlined"
                size="large"
                onClick={() => { setMode("new"); setStep(2); }}
                fullWidth
              >
                Đăng ký mới
              </Button>
              <Button
                variant="outlined"
                size="large"
                onClick={() => { setMode("member"); setStep(2); }}
                fullWidth
              >
                Đã có membership
              </Button>
            </Box>
            <Button onClick={() => setStep(0)}>⬅️ Quay lại</Button>
          </Box>

          {/* Step 2.1: Form đăng ký mới */}
          <Box textAlign="center" py={8}>
            {mode === "new" && (
              <Box component="form"
                onSubmit={handleSubmit}
                maxWidth={350}
                mx="auto"
              >
                <Typography variant="h6" mb={2}>Vui lòng điền thông tin bên dưới</Typography>
                <TextField
                  label="Họ tên"
                  fullWidth
                  margin="normal"
                  required
                  value={form.name}
                  onChange={e => setForm(f => ({ ...f, name: e.target.value }))}
                />
                <TextField
                  label="Số điện thoại"
                  fullWidth
                  margin="normal"
                  required
                  value={form.phone}
                  onChange={e => setForm(f => ({ ...f, phone: e.target.value }))}
                />
                <TextField
                  label="Email"
                  fullWidth
                  margin="normal"
                  required
                  value={form.email}
                  onChange={e => setForm(f => ({ ...f, email: e.target.value }))}
                />
                {error && (
                  <Typography color="error" mt={1} mb={1} fontSize={14}>
                    {error}
                  </Typography>
                )}
                <Button
                  type="submit"
                  variant="contained"
                  fullWidth
                  sx={{ mt: 2 }}
                >
                  Lấy số thứ tự
                </Button>
                <Button sx={{ mt: 2 }} onClick={() => setStep(1)}>⬅️ Quay lại</Button>
              </Box>
            )}
            {mode === "member" && (
              <Box component="form"
                onSubmit={e => {
                  e.preventDefault();
                  handleGetTicket({ memberId, passportId });
                }}
                maxWidth={350}
                mx="auto"
              >
                <Typography variant="h6" mb={2}>Vui lòng nhập thông tin bên dưới</Typography>
                <TextField
                  label="Member ID"
                  fullWidth
                  margin="normal"
                  value={memberId}
                  onChange={e => setMemberId(e.target.value)}
                />
                <TextField
                  label="Passport ID"
                  fullWidth
                  margin="normal"
                  value={passportId}
                  onChange={e => setPassportId(e.target.value)}
                />
                <Button
                  type="submit"
                  variant="contained"
                  fullWidth
                  sx={{ mt: 2 }}
                  disabled={!memberId && !passportId}
                >
                  Lấy số thứ tự
                </Button>
                <Button sx={{ mt: 2 }} onClick={() => setStep(1)}>⬅️ Quay lại</Button>
              </Box>
            )}
          </Box>

          {/* Step 3: Kết quả */}
          <Box>
            {ticket && (
              <Step3Result
                ticketNumber={ticket.ticketNumber}
                qrCodeUrl={ticket.qrCodeUrl}
                onRestart={() => {
                  setStep(0);
                  setMode(null);
                  setTicket(null);
                  setForm({ name: "", phone: "", email: "" });
                  setMemberId("");
                  setPassportId("");
                }}
              />
            )}
          </Box>
        </SwipeableViews>
      </Box>
    </Container>
    </MainLayout>
  );
}