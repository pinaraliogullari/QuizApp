import React from "react";
import {
  Button,
  TextField,
  Box,
  CardContent,
  Card,
  Typography,
} from "@mui/material";
import Center from "./Center";

const Login = () => {
  return (
    <>
      <Center>
        <Card sx={{ width: 400 }}>
          <CardContent sx={{ textAlign: "center" }}>
            <Typography variant="h3" sx={{ my: 3 }}>
              Quiz App
            </Typography>
            <Box
              sx={{
                "& .MuiTextField-root": { m: 1, width: "90%" },
                "& .MuiButton-root": { width: "90%" },
              }}
            >
              <form noValidate autoComplete="off">
                <TextField variant="outlined" label="Email" name="email" />
                <TextField variant="outlined" label="Name" name="name" />
                <Button variant="contained" type="submit" color="primary">
                  Start
                </Button>
              </form>
            </Box>
          </CardContent>
        </Card>
      </Center>
    </>
  );
};

export default Login;
