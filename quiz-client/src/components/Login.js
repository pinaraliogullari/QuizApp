import React, { useState } from 'react';
import { Button, TextField, Box, CardContent, Card, Typography } from '@mui/material';
import { Link, useNavigate } from 'react-router-dom';
import HttpClientService from '../services/HttpClientService'; 
import { useContext } from 'react';
import { AppContext } from '../context/AppContext'; 
import { RequestParameters } from '../models/RequestParameters';

const Login = () => {
  const [usernameOrEmail, setUsernameOrEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();
  const { baseUrl } = useContext(AppContext); 

  const handleLogin = async (e) => {
    e.preventDefault();

    const payload = {
      usernameOrEmail,
      password,
    };
    const requestParameters = new RequestParameters(
      'users', // controller
      'login', // action
      '', // queryString
      {}, // headers
      baseUrl // baseUrl
    );


    try {
     const response = await HttpClientService.post(requestParameters, payload); 
      console.log('API Response:', response);

      if (response?.token?.accessToken) {
        const tokenData = {
          accessToken: response.token.accessToken,
          expiration: response.token.expiration,
        };

        localStorage.setItem('token', JSON.stringify(tokenData)); 
        navigate('/quiz'); 
      } else {
        setError('Invalid login credentials');
      }
    } catch (error) {
      setError('An error occurred. Please check your information.');
      console.error(error);
    }
  };

  return (
    <Card sx={{ width: 400, mx: 'auto', mt: 5 }}>
      <CardContent sx={{ textAlign: 'center' }}>
        <Typography variant="h3" sx={{ my: 3 }}>
          Quiz App
        </Typography>
        <Box sx={{ '& .MuiTextField-root': { m: 1, width: '90%' }, '& .MuiButton-root': { width: '90%', my: 1 } }}>
          <form noValidate autoComplete="off" onSubmit={handleLogin}>
            <TextField
              variant="outlined"
              label="Username or Email"
              name="usernameOrEmail"
              value={usernameOrEmail}
              onChange={(e) => setUsernameOrEmail(e.target.value)}
              fullWidth
            />
            <TextField
              variant="outlined"
              label="Password"
              name="password"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              fullWidth
            />
            {error && <Typography color="error">{error}</Typography>}
            <Button variant="contained" color="primary" type="submit">
              Login
            </Button>
            <p>
              Don't have an account? <Link to="/register">Register</Link>
            </p>
          </form>
        </Box>
      </CardContent>
    </Card>
  );
};

export default Login;
