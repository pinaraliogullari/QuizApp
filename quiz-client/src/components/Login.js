import React from "react";
import useForm from "../hooks/useForm";
import { Button, TextField, Box, CardContent, Card, Typography } from "@mui/material";
import { Link } from "react-router-dom";
import Center from "./Center";

const getFreshModelObject = () => ({
  email: "",
  username: "",
});

const Login = () => {
  const { values, setValues, errors, setErrors, handleInputChange } = useForm(getFreshModelObject);

  const validate = () => {
    let temp = {};
    temp.email = (/\S+@\S+\.\S+/).test(values.email) ? "" : "Email is not valid.";
    temp.username = values.username !== "" ? "" : "This field is required.";
    setErrors(temp);
    return Object.values(temp).every(x => x === "");
  };

  const login = (e) => {
    e.preventDefault();
    if (validate()) {
      console.log(values);
    }
  };


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
                  label="Password"
                  name="password"
                  value={values.password}
                  {...(errors.password && { error: true, helperText: errors.password })}
                />
                <Button variant="contained" type="submit" color="primary">Start</Button>
                <p>Don't you have an account.
                <Link  to="/register">Register Now</Link>
                </p>
              
              </form>
            </Box>
          </CardContent>
        </Card>
      </Center>
    </>
  );
};

export default Login;
