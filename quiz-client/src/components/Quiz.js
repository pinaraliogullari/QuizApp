import React, { useEffect } from 'react';
import { useStateContext } from '../context/useStateContext'; 

const Quiz = () => {
  const { context, setContext } = useStateContext(); 

  useEffect(() => {
    setContext({
      ...context,
      timeTaken: 1
    });
  }, [context, setContext]);

  return (
    <div>Question</div>
  );
}

export default Quiz;
