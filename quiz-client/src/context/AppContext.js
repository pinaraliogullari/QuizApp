import { createContext, useState } from 'react';
import { jwtDecode } from 'jwt-decode';
import { useEffect } from 'react';

export const AppContext = createContext();

export const AppProvider = ({ children }) => {
  const baseUrl = 'https://localhost:7291/api';
  const [selectedOptions, setSelectedOptions] = useState([]);
  const [timeTaken, setTimeTaken] = useState(0);
  const [userId, setUserId] = useState(null);

 useEffect(() => {
    const tokenString = localStorage.getItem('token');
    if (tokenString) {
      try {
        const { accessToken } = JSON.parse(tokenString);
        if (accessToken) {
          const decodedToken = jwtDecode(accessToken);
          const id = decodedToken.userId;
          setUserId(id); 
        }
      } catch (error) {
        console.error('Token decode error:', error);
      }
    }
  }, []); 


  const updateSelectedOptions = (newOption) => {
    if (newOption && newOption.qnId !== undefined && newOption.selected !== undefined) {
      setSelectedOptions(prev => {
        const optionExists = prev.some(option => option.qnId === newOption.qnId);
        if (optionExists) return prev; 
        return [...prev, newOption];
      });
    }
  };

  const updateTimeTaken = (newTime) => {
    setTimeTaken(newTime);
  };

  return (
    <AppContext.Provider value={{
      baseUrl,
      userId,
      selectedOptions,
      timeTaken,
      setTimeTaken,
      setSelectedOptions,
      updateSelectedOptions,
      updateTimeTaken
    }}>
      {children}
    </AppContext.Provider>
  );
};
