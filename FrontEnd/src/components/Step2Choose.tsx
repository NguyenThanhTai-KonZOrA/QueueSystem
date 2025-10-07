import { Box, Button, Typography } from "@mui/material";

interface Step2ChooseProps {
  onBack: () => void;
  onChooseNew: () => void;
  onChooseMember: () => void;
}

export default function Step2Choose({ onBack, onChooseNew, onChooseMember }: Step2ChooseProps) {
  return (
    <Box textAlign="center" mt={8}>
      <Typography variant="h6" gutterBottom>
        Vui lòng chọn phương thức
      </Typography>
      <Button sx={{ mt: 2, width: "80%" }} variant="contained" onClick={onChooseNew}>
        Đăng ký mới
      </Button>
      <Button sx={{ mt: 2, width: "80%" }} variant="outlined" onClick={onChooseMember}>
        Đã có MemberID
      </Button>
      <Button sx={{ mt: 4 }} onClick={onBack}>
        ⬅️ Quay lại
      </Button>
    </Box>
  );
}
