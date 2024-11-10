import React from "react";
import useForm from "../hooks/useForm";
import { Button, TextField, Box, CardContent, Card, Typography } from "@mui/material";
import { Link ,useNavigate} from "react-router-dom";
import Center from "./Center";
import { createAPIEndpoint, ENDPOINTS } from "../api";

const getFreshModelObject = () => ({
  userNameOrEmail: "",
  password: "",
});

const Login = () => {
  const { values, setValues, errors, setErrors, handleInputChange } = useForm(getFreshModelObject);
  const navigate = useNavigate();

  const validate = () => {
    let temp = {};
  temp.userNameOrEmail = values.userNameOrEmail ? "" : "UserName or Email is required.";

  if (values.userNameOrEmail && /\S+@\S+\.\S+/.test(values.userNameOrEmail)) {
    temp.userNameOrEmail = ""; 
  } else if (values.userNameOrEmail && values.userNameOrEmail.length < 4) {
    temp.userNameOrEmail = "UserName must be at least 4 characters";
  }
    temp.password = values.password !== "" ? "" : "This field is required.";
    setErrors(temp);
    return Object.values(temp).every(x => x === "");
  };

  const login = (e) => {
    e.preventDefault();
    if (validate()) {
      createAPIEndpoint(ENDPOINTS.users,"login")
      .post(values)
      .then((res) => {
        console.log(res);
        alert(res.data.message);
        navigate('/quiz');
    }
      )
  };
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
                  type="text"
                  label="USerName or Email"
                  name="userNameOrEmail"
                  value={values.userNameOrEmail}
                  {...(errors.userNameOrEmail && { error: true, helperText: errors.userNameOrEmail })}
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

}


export default Login;
