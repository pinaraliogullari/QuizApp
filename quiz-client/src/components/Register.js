import React from "react";
import useForm from "../hooks/useForm";
import { Button, TextField, Box, CardContent, Card, Typography } from "@mui/material";
import { Link,useNavigate } from "react-router-dom";
import Center from "./Center";
import { createAPIEndpoint, ENDPOINTS } from "../api/index";


const getFreshModelObject = () => ({
  email: "",
  username: "",
  password: "",
});

const Register = () => {
  const { values, setValues, errors, setErrors, handleInputChange } = useForm(getFreshModelObject);
  const navigate = useNavigate();

  const validate = () => {
    let temp = {};
    temp.email = (/\S+@\S+\.\S+/).test(values.email) ? "" : "Email is not valid.";
    temp.username = values.username !== "" ? "" : "This field is required.";
    temp.password = values.password !== "" ? "" : "This field is required.";
    setErrors(temp);
    return Object.values(temp).every(x => x === "");
  };


const register = (e) => {
  e.preventDefault();
  if (validate()) {
    createAPIEndpoint(ENDPOINTS.users)
      .post(values)
      .then((res) => {
        console.log(res);
        alert(res.data.message);
        navigate('/login');

      })
      .catch((err) => console.log(err));
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
              <form noValidate autoComplete="off" onSubmit={register}>
                  <TextField
                  onChange={handleInputChange}
                  type="text"
                  variant="outlined"
                  label="Username"
                  name="username"
                  value={values.username}
                  {...(errors.username && { error: true, helperText: errors.username })}
                />
                <TextField
                  onChange={handleInputChange}
                  type="email"
                  variant="outlined"
                  label="Email"
                  name="email"
                  value={values.email}
                  {...(errors.email && { error: true, helperText: errors.email })}
                />
                <TextField
                  onChange={handleInputChange}
                  type="password"
                  variant="outlined"
                  label="Password"
                  name="password"
                  value={values.password}
                  {...(errors.password && { error: true, helperText: errors.password })}
                />
                <Button variant="contained" type="submit" color="primary">Register</Button>
                  <p>Already you have an account.
                <Link  to="/login">Login</Link>
                </p>
             
              
              </form>
            </Box>
          </CardContent>
        </Card>
      </Center>
    </>
  );
}
export default Register;
