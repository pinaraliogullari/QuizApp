import React, { useState, useContext } from 'react';
import { Button, TextField, Box, CardContent, Card, Typography } from '@mui/material';
import { Link, useNavigate } from 'react-router-dom';
import HttpClientService from '../services/HttpClientService'; 
import { AppContext } from '../context/AppContext'; 
import { RequestParameters } from '../models/RequestParameters'; 

const Register = () => {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState(''); 
  const navigate = useNavigate();
  const { baseUrl } = useContext(AppContext); 

  const handleRegister = async (e) => {
    e.preventDefault();

    const payload = {
      username,
      email,
      password,
    };

    const requestParameters = new RequestParameters(
      'users',             // controller
      '',          // action
      '',                  // queryString 
      {},                  // headers 
      baseUrl,             // baseUrl
      ``     // fullEndPoint
    );

    try {
      const response = await HttpClientService.post(requestParameters, payload); 

      if (response?.success) {
        setSuccess('Registration successful! Please log in to continue.');
        setError(''); 
        setTimeout(() => {
          navigate('/login'); 
        }, 2000); 
      } else {
        setError('Registration failed. Please try again.');
        setSuccess(''); 
      }
    } catch (error) {
      setError('An error occurred during registration.');
      setSuccess(''); 
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
          <form noValidate autoComplete="off" onSubmit={handleRegister}>
            <TextField
              variant="outlined"
              label="Username"
              name="username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              fullWidth
            />
            <TextField
              variant="outlined"
              label="Email"
              name="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
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
            {success && <Typography color="success">{success}</Typography>} {/* Başarı mesajı */}
            <Button variant="contained" color="primary" type="submit">
              Register
            </Button>
            <p>
              Already have an account? <Link to="/login">Login</Link>
            </p>
          </form>
        </Box>
      </CardContent>
    </Card>
  );
};

export default Register;
