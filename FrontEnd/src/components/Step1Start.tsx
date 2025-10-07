import { Box, Button, Typography } from "@mui/material";
import MainLayout from "../layout/MainLayout";
interface Step1StartProps {
  onNext: () => void;
}

export default function Step1Start({ onNext }: Step1StartProps) {
  return (
      <Box textAlign="center" mt={10}>
        <Typography variant="h5" gutterBottom>
          Hệ thống lấy số thứ tự
        </Typography>
        <Button variant="contained" size="large" onClick={onNext}>
          Bắt đầu
        </Button>
      </Box>
  );
}
