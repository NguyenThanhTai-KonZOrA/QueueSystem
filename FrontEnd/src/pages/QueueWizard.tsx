import { Container, Box, Button, Typography, TextField } from "@mui/material";
import { useState } from "react";
import SwipeableViews from 'react-swipeable-views-react-18-fix';
import Step3Result from "../components/Step3Result";

export default function QueueWizard() {
  const [step, setStep] = useState(0);
  const [mode, setMode] = useState<"new" | "member" | null>(null);
  const [ticket, setTicket] = useState<any>(null);
  const [memberId, setMemberId] = useState("");
  const [passportId, setPassportId] = useState("");
  const [form, setForm] = useState({ name: "", phone: "" });

  // Giả lập gọi API lấy ticket
  const handleGetTicket = (data: any) => {
    setTicket({
      ticketNumber: Math.floor(Math.random() * 1000),
      qrCodeUrl: "https://your-server.com/qr/" + Math.random().toString(36).slice(2)
    });
    setStep(2);
  };

  return (
    <Container
      maxWidth="sm"
      sx={{
        height: "100vh",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        p: 0
      }}
    >
      <Box width="100%">
        <SwipeableViews index={step} onChangeIndex={setStep} enableMouseEvents>
          {/* Step 1 */}
          <Box textAlign="center" py={8}>
            <Typography variant="h5" mb={4}>Chào mừng bạn đến quầy lấy số</Typography>
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
            <Typography variant="h6" mb={3}>Bạn muốn?</Typography>
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
                Đã có memberID
              </Button>
            </Box>
            <Button onClick={() => setStep(0)}>⬅️ Quay lại</Button>
          </Box>

          {/* Step 2.1: Form đăng ký mới */}
          <Box textAlign="center" py={8}>
            {mode === "new" && (
              <Box component="form"
                onSubmit={e => {
                  e.preventDefault();
                  handleGetTicket(form);
                }}
                maxWidth={350}
                mx="auto"
              >
                <Typography variant="h6" mb={2}>Đăng ký mới</Typography>
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
                <Typography variant="h6" mb={2}>Nhập memberID hoặc passportID</Typography>
                <TextField
                  label="Member ID"
                  fullWidth
                  margin="normal"
                  value={memberId}
                  onChange={e => setMemberId(e.target.value)}
                />
                <Typography variant="body2" my={1}>Hoặc</Typography>
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
                  setForm({ name: "", phone: "" });
                  setMemberId("");
                  setPassportId("");
                }}
              />
            )}
          </Box>
        </SwipeableViews>
      </Box>
    </Container>
  );
}