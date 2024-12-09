import { AppBar, Toolbar, Typography, Button, Container } from '@mui/material'
import React from 'react'
import { Outlet } from 'react-router-dom'
import { useNavigate } from 'react-router-dom'
import { AppContext } from '../context/AppContext'
import { useContext } from 'react'

const Layout = () => {
    const navigate = useNavigate();
    const { setSelectedOptions, updateTimeTaken } = useContext(AppContext);
    const logout = () => {
        localStorage.removeItem('token');
        setSelectedOptions([]); 
        updateTimeTaken(0);
        navigate('/login');
   
    }
  return (
   <>
   <AppBar position="sticky">
          <Toolbar sx={{ width: 640, m: 'auto' }}>
              <Typography
                  variant='h4'
                  align='center'
                  sx={{flexGrow:1}}>
                  Quiz App
              </Typography>
              <Button onClick={logout}>Logout</Button>
          </Toolbar>
    </AppBar>
    <Container>
        <Outlet />
    </Container>

    </>
  )
}

export default Layout