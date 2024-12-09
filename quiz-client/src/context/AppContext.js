import { createContext, useState } from 'react';

export const AppContext = createContext();

export const AppProvider = ({ children }) => {
  const baseUrl = 'https://localhost:7291/api';
  const [selectedOptions, setSelectedOptions] = useState([]);
  const [timeTaken, setTimeTaken] = useState(0);

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
