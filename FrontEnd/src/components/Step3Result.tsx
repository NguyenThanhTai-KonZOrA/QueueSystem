import { Box, Button, Typography } from "@mui/material";
import { QRCodeCanvas } from "qrcode.react";

interface Step3ResultProps {
  ticketNumber: number;
  qrCodeUrl: string;
  onRestart: () => void;
}

export default function Step3Result({ ticketNumber, qrCodeUrl, onRestart }: Step3ResultProps) {
  return (
    <Box textAlign="center" mt={8}>
      <Typography variant="h5">🎟️ Số thứ tự của bạn</Typography>
      <Typography variant="h2" color="primary" fontWeight="bold" mt={1}>
        {ticketNumber.toString().padStart(3, "0")}
      </Typography>
      <Box mt={3}>
        <QRCodeCanvas value={qrCodeUrl} size={180} />
      </Box>
      <Button sx={{ mt: 4 }} onClick={onRestart}>
        🔄 Lấy số mới
      </Button>
    </Box>
  );
}