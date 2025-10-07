import { Box, Button, Typography } from "@mui/material";
import { useTranslation } from "react-i18next";
interface Step1StartProps {
  onNext: () => void;
}
const { t } = useTranslation();

export default function Step1Start({ onNext }: Step1StartProps) {
  return (
      <Box textAlign="center" mt={10}>
        <Typography variant="h5" gutterBottom>
          {t("GetTicketTitle")}
        </Typography>
        <Button variant="contained" size="large" onClick={onNext}>
          {t("Start")}
        </Button>
      </Box>
  );
}
