import { Box, Button } from "@mui/material";

interface MainLayoutProps {
    children: React.ReactNode;
    onHome?: () => void;
}

export default function MainLayout({ children, onHome }: MainLayoutProps) {
    return (
        <Box minHeight="100vh" height="100vh" bgcolor="#f8f8f8" display="flex" flexDirection="column">
            {/* Header */}
            <Box
                bgcolor="#234043"
                color="#fff"
                display="flex"
                alignItems="center"
                justifyContent="center"
                px={2}
                py={1.5}
                minHeight={60}
            >
                <Box display="inherit" alignItems="center" gap={1}>
                    <img src="/TheGrandHoTram.png" alt="Logo" style={{ height: 55 }} />
                </Box>
                {onHome && (
                    <Button
                        variant="contained"
                        color="secondary"
                        size="small"
                        onClick={onHome}
                        sx={{ bgcolor: "#fff", color: "#234043", fontWeight: 600 }}
                    >
                        Home
                    </Button>
                )}
            </Box>
            {/* Content */}
            <Box flex={1} display="flex" alignItems="center" justifyContent="center">
                {children}
            </Box>
        </Box>
    );
}