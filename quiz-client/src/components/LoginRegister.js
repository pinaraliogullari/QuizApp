import React from "react";
import useForm from "../hooks/useForm";
import { Button, TextField, Box, CardContent, Card, Typography } from "@mui/material";
import Center from "./Center";

const getFreshModelObject = () => ({
  email: "",
  name: "",
});

const LoginRegister = () => {
  const { values, setValues, errors, setErrors, handleInputChange } = useForm(getFreshModelObject);

  const validate = () => {
    let temp = {};
    temp.email = (/\S+@\S+\.\S+/).test(values.email) ? "" : "Email is not valid.";
    temp.name = values.name !== "" ? "" : "This field is required.";
    setErrors(temp);
    return Object.values(temp).every(x => x === "");
  };

  const login = (e) => {
    e.preventDefault();
    if (validate()) {
      console.log(values);
    }
  };
  const register = (e) => {
    e.preventDefault();
    if (validate()) {
      console.log(values);
    }
  }

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
                "& .MuiButton-root": { width: "90%",my:1 },
              }}
            >
              <form noValidate autoComplete="off" onSubmit={login}>
                <TextField
                  onChange={handleInputChange}
                  variant="outlined"
                  label="Email"
                  name="email"
                  value={values.email}
                  {...(errors.email && { error: true, helperText: errors.email })}
                />
                <TextField
                  onChange={handleInputChange}
                  variant="outlined"
                  label="Name"
                  name="name"
                  value={values.name}
                  {...(errors.name && { error: true, helperText: errors.name })}
                />
                <Button variant="contained" type="submit" color="primary">Start</Button>
                <Button variant="outlined" onClick={register}  color="primary">Register</Button>
              </form>
            </Box>
          </CardContent>
        </Card>
      </Center>
    </>
  );
};

export default LoginRegister;
