import React, { createContext, useState } from 'react';


export const StateContext = createContext();

const getFreshContext= () => {
    return {
      userId:1,
      timeTaken:0,
      selectedOptions: [],
    };
}

export const ContextProvider = ({ children }) => {
    const [context, setContext] = useState(getFreshContext());

    return (
        <StateContext.Provider value={{ context, setContext }}>
            {children}
        </StateContext.Provider>
    );
};
