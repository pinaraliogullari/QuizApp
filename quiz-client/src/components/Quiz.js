import React from 'react'
import { useContext } from 'react';
import { StateContext } from '../context/StateContext';

const Quiz = () => {
  const { context, setContext } = useContext(StateContext);
  setContext({
    ...context,
    timeTaken: 1
  })
  return (
    <div>Question</div>
  )
}

export default Quiz