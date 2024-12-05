import React, { useEffect, useState, useContext } from 'react';
import { Alert, Button, Card, CardContent, CardMedia, Typography } from '@mui/material';
import { Box } from '@mui/system';
import { useNavigate } from 'react-router-dom';
import { getFormatedTime } from '../helper';
import { green } from '@mui/material/colors';
import { AppContext } from '../context/AppContext'; 
import HttpClientService from '../services/HttpClientService'; 
import { RequestParameters } from '../models/RequestParameters'; 

const Result = () => {
  const [score, setScore] = useState(0);
  const [qnAnswers, setQnAnswers] = useState([]);
  const [showAlert, setShowAlert] = useState(false);
  const { selectedOptions, timeTaken, setContext, baseUrl } = useContext(AppContext); 
  const navigate = useNavigate();

  useEffect(() => {
    const ids = selectedOptions.map(x => x.qnId);
    const requestParameters = new RequestParameters(
      'questions', // 
      '', // Action 
      '', // Query string 
      {}, // Headers
      baseUrl, 
      '', // Full endpoint 
    );

    HttpClientService.post(requestParameters, ids)
      .then(res => {
        const qna = selectedOptions.map(x => ({
          ...x,
          ...(res.data.find(y => y.qnId === x.qnId))
        }));
        setQnAnswers(qna);
        calculateScore(qna);
      })
      .catch(err => console.log(err));
  }, [selectedOptions, baseUrl]); 

  const calculateScore = qna => {
    const tempScore = qna.reduce((acc, curr) => {
      return curr.answer === curr.selected ? acc + 1 : acc;
    }, 0);
    setScore(tempScore);
  };

  const restart = () => {
    setContext({
      timeTaken: 0,
      selectedOptions: []
    });
    navigate('/quiz');
  };

  // const submitScore = () => {
  //   const requestParameters = new RequestParameters(
  //     'users', 
  //     'submit', 
  //     '', // Query string
  //     {}, // Headers
  //     baseUrl, 
  //     '' // Full endpoint
  //   );

  //   const payload = {
  //     participantId: context.participantId,
  //     score: score,
  //     timeTaken: timeTaken
  //   };

  //   HttpClientService.put(requestParameters, payload)
  //     .then(res => {
  //       setShowAlert(true);
  //       setTimeout(() => {
  //         setShowAlert(false);
  //       }, 4000);
  //     })
  //     .catch(err => console.log(err));
  // };

  return (
    <>
      <Card sx={{ mt: 5, display: 'flex', width: '100%', maxWidth: 640, mx: 'auto' }}>
        <Box sx={{ display: 'flex', flexDirection: 'column', flexGrow: 1 }}>
          <CardContent sx={{ flex: '1 0 auto', textAlign: 'center' }}>
            <Typography variant="h4">Congratulations!</Typography>
            <Typography variant="h6">YOUR SCORE</Typography>
            <Typography variant="h5" sx={{ fontWeight: 600 }}>
              <Typography variant="span" color={green[500]}>
                {score}
              </Typography>/5
            </Typography>
            <Typography variant="h6">
              Took {getFormatedTime(timeTaken)} mins
            </Typography>
            <Button variant="contained" sx={{ mx: 1 }} size="small" >
              Submit
            </Button>
            <Button variant="contained" sx={{ mx: 1 }} size="small" onClick={restart}>
              Re-try
            </Button>
            <Alert
              severity="success"
              variant="filled"
              sx={{
                width: '60%',
                m: 'auto',
                visibility: showAlert ? 'visible' : 'hidden'
              }}
            >
              Score Updated.
            </Alert>
          </CardContent>
        </Box>
        <CardMedia component="img" sx={{ width: 220 }} image="./result.png" />
      </Card>
    </>
  );
};

export default Result;
