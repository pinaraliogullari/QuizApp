import React from 'react';
import { Navigate } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';


function PrivateRoute({ children }) {
  const tokenString = localStorage.getItem('token');
  let token = null;

  if (tokenString) {
    token = JSON.parse(tokenString);
  }

  if (!token || !token.accessToken) {
    return <Navigate to="/login" />;
  }

  let payload;
  try {
    payload = jwtDecode(token.accessToken);
  } catch (error) {
    console.error('Token decode error:', error);
    return <Navigate to="/login" />;
  }

  if (!payload || !payload.exp || payload.exp * 1000 < Date.now()) {
    return <Navigate to="/login" />;
  }

  return children;
}

export default PrivateRoute;
