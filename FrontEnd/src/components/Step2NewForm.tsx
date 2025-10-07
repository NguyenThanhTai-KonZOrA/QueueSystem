import { Box, Button, Paper, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { queueService } from "../services/queueService";
import MainLayout from "../layout/MainLayout";
interface Step2NewFormProps {
  onBack: () => void;
  onSuccess: (ticket: any) => void;
}

export default function Step2NewForm({ onBack, onSuccess }: Step2NewFormProps) {
  const [form, setForm] = useState({ fullName: "", phone: "", email: "" });
  const [loading, setLoading] = useState(false);

  const handleSubmit = async () => {
    setLoading(true);
    try {
      const ticket = await queueService.registerNewUser({
        ...form,
        counterId: 1
      });
      onSuccess(ticket);
    } catch (e) {
      alert("Đăng ký thất bại!");
    } finally {
      setLoading(false);
    }
  };

  return (
      <Box component={Paper} sx={{ p: 3, mt: 5 }}>
        <Typography variant="h6" textAlign="center">
          Nhập thông tin cá nhân
        </Typography>
        <TextField
          fullWidth
          label="Họ và tên"
          margin="normal"
          value={form.fullName}
          onChange={(e) => setForm({ ...form, fullName: e.target.value })}
        />
        <TextField
          fullWidth
          label="Số điện thoại"
          margin="normal"
          value={form.phone}
          onChange={(e) => setForm({ ...form, phone: e.target.value })}
        />
        <TextField
          fullWidth
          label="Email"
          margin="normal"
          value={form.email}
          onChange={(e) => setForm({ ...form, email: e.target.value })}
        />
        <Box display="flex" justifyContent="space-between" mt={2}>
          <Button onClick={onBack}>⬅️ Quay lại</Button>
          <Button variant="contained" onClick={handleSubmit} disabled={loading}>
            {loading ? "Đang xử lý..." : "Lấy số"}
          </Button>
        </Box>
      </Box>
  );
}
