import { Box, Button, Typography, Paper } from "@mui/material";
import { QRCodeCanvas } from "qrcode.react";
import { useEffect, useState } from "react";
import MainLayout from "../layout/MainLayout";
interface Step3ResultProps {
  ticketNumber: number;
  qrCodeUrl: string;
  onRestart: () => void;
}

export default function Step3Result({ ticketNumber, qrCodeUrl, onRestart }: Step3ResultProps) {
  const [countdown, setCountdown] = useState(200);

  useEffect(() => {
    if (countdown === 0) {
      onRestart();
      return;
    }
    const timer = setTimeout(() => setCountdown(c => c - 1), 1000);
    return () => clearTimeout(timer);
  }, [countdown, onRestart]);

  return (
      <Box display="flex" flexDirection="column" alignItems="center" mt={4}>
        {/* Ticket Card */}
        <Paper elevation={3} sx={{ px: 4, py: 2, borderRadius: 3, mb: 2 }}>
          <Typography variant="subtitle1" color="text.secondary" textAlign="center">
            Your Ticket Number
          </Typography>
          <Typography variant="h2" color="primary" fontWeight="bold" textAlign="center">
            {ticketNumber.toString().padStart(3, "0")}
          </Typography>
        </Paper>

        {/* Notice */}
        <Box
          bgcolor="#d8a899"
          color="#234043"
          px={3}
          py={1.5}
          borderRadius={2}
          mb={3}
          width="100%"
          maxWidth={340}
          textAlign="center"
        >
          <Typography fontWeight={500}>
            Please collect your ticket from the printer.
          </Typography>
        </Box>

        {/* QR Code */}
        <Typography mb={1} fontWeight={500}>Scan for ticket status</Typography>
        <QRCodeCanvas value={qrCodeUrl} size={140} />

        {/* Countdown */}
        <Typography mt={3} color="text.secondary" fontSize={16}>
          Return to home in <span style={{ color: "#d9534f", fontWeight: 600 }}>{countdown}</span> seconds
        </Typography>
        <Typography mt={1} color="text.secondary" fontSize={15}>
          Thank You
        </Typography>
      </Box>
  );
}