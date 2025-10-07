import { Box, Button, Paper, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { queueService } from "../services/queueService";

interface Step2MemberFormProps {
  onBack: () => void;
  onSuccess: (ticket: any) => void;
}

export default function Step2MemberForm({ onBack, onSuccess }: Step2MemberFormProps) {
  const [memberId, setMemberId] = useState("");
  const [loading, setLoading] = useState(false);

  const handleSubmit = async () => {
    setLoading(true);
    try {
      const ticket = await queueService.registerByMember({
        memberId,
        counterId: 1
      });
      onSuccess(ticket);
    } catch {
      alert("Không tìm thấy MemberID!");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Box component={Paper} sx={{ p: 3, mt: 5 }}>
      <Typography variant="h6" textAlign="center">
        Nhập MemberID hoặc PassportID
      </Typography>
      <TextField
        fullWidth
        label="MemberID / PassportID"
        margin="normal"
        value={memberId}
        onChange={(e) => setMemberId(e.target.value)}
      />
      <Box display="flex" justifyContent="space-between" mt={2}>
        <Button onClick={onBack}>⬅️ Quay lại</Button>
        <Button variant="contained" onClick={handleSubmit} disabled={loading}>
          {loading ? "Đang xử lý..." : "Xác nhận"}
        </Button>
      </Box>
    </Box>
  );
}
