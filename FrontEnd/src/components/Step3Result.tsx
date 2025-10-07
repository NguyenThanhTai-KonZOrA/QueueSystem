import { Box, Button, Typography, Paper } from "@mui/material";
import { QRCodeCanvas } from "qrcode.react";
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
interface Step3ResultProps {
  ticketNumber: number;
  qrCodeUrl: string;
  onRestart: () => void;
}

export default function Step3Result({ ticketNumber, qrCodeUrl, onRestart }: Step3ResultProps) {
  const [countdown, setCountdown] = useState(200);
  const { t } = useTranslation();
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
          {t("YourTicketNumber")}
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
          {t("please_collect")}
        </Typography>
      </Box>

      {/* QR Code */}
      <Typography mb={1} fontWeight={500}>{t("scan_status")}</Typography>
      <QRCodeCanvas value={qrCodeUrl} size={140} />

      {/* Countdown */}
      <Typography mt={3} color="text.secondary" fontSize={16}>
        {t("Return to home in")} <span style={{ color: "#d9534f", fontWeight: 600 }}>{countdown}</span> {t("seconds")}
      </Typography>
      <Typography mt={1} color="text.secondary" fontSize={15}>
        {t("thank_you")}
      </Typography>
    </Box>
  );
}