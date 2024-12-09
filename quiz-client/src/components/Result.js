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
  const { selectedOptions, timeTaken, baseUrl,userId, updateTimeTaken, setSelectedOptions } = useContext(AppContext);
  const navigate = useNavigate();

  useEffect(() => {
    console.log('Selected Options:', selectedOptions);  
    const uniqueSelectedOptions = selectedOptions.filter(
      (item, index, self) => index === self.findIndex((t) => t.qnId === item.qnId)
    );
    console.log('Unique Selected Options:', uniqueSelectedOptions);

    const questionIds = uniqueSelectedOptions.slice(0, 5).map((x) => x.qnId);
    console.log('Question IDs:', questionIds);

    const payload = { questionIds };
    const requestParameters = new RequestParameters(
      'questions',
      '', // Action
      '', // Query string
      {}, // Headers
      baseUrl,
      '' // Full endpoint
    );

    HttpClientService.post(requestParameters, payload)
      .then((res) => {
        console.log('Fetched data:', res);
        const qna = uniqueSelectedOptions.slice(0, 5).map((x) => ({
          ...x,
          ...(res.find((y) => y.id === x.qnId)),  
        }));
        console.log('QnA:', qna);  
        setQnAnswers(qna);
        calculateScore(qna);
      })
      .catch((err) => console.error('Error fetching data:', err));
  }, [baseUrl, selectedOptions]);

  const calculateScore = (qna) => {
    const tempScore = qna.reduce((acc, curr) => {
      console.log(`Answer: ${curr.answer}, Selected: ${curr.selected}`); 
      return curr.answer === curr.selected ? acc + 1 : acc;
    }, 0);
    console.log('Score:', tempScore);
    setScore(tempScore);
  };

  const restart = () => {
    setSelectedOptions([]); 
    updateTimeTaken(0);
    navigate('/quiz');
  };

const submitScore = (userId) => {
console.log('userId',userId)
  const payload = {
    Id: userId,     
    Score: score,   
    TimeTaken: timeTaken, 
  };

  const requestParameters = {
    controller: 'Users',          
    action: '',        
    queryString: '',           
    headers: {},                
    baseUrl: baseUrl,         
    fullEndPoint: '',            
  };

 
  HttpClientService.put(requestParameters, payload)
    .then((res) => {
      console.log('Score submitted:', res);
      setShowAlert(true);
       setTimeout(() => {
          setShowAlert(false)
        }, 4000);
    })
    .catch((err) => console.error('Error submitting score:', err));
};


  return (
    <Card sx={{ mt: 5, display: 'flex', width: '100%', maxWidth: 640, mx: 'auto' }}>
      <Box sx={{ display: 'flex', flexDirection: 'column', flexGrow: 1 }}>
        <CardContent sx={{ flex: '1 0 auto', textAlign: 'center' }}>
          <Typography variant="h4">Congratulations!</Typography>
          <Typography variant="h6">YOUR SCORE</Typography>
          <Typography variant="h5" sx={{ fontWeight: 600 }}>
            <span style={{ color: green[500] }}>
              {score}
            </span>
            /5
          </Typography>
          <Typography variant="h6">
            Took {getFormatedTime(timeTaken)} mins
          </Typography>
          <Button variant="contained"  sx={{ mx: 1, mb: 2 }} size="small" onClick={()=>submitScore(userId)}>
            Submit
          </Button>
          <Button variant="contained"  sx={{ mx: 1, mb: 2 }}size="small" onClick={restart}>
            Re-try
          </Button>
          <Alert
            severity="success"
            variant="filled"
            sx={{
              width: '60%',
              m: 'auto',
              visibility: showAlert ? 'visible' : 'hidden',
            }}
          >
            Score Updated.
          </Alert>
        </CardContent>
      </Box>
      <CardMedia component="img" sx={{ width: 220 }} image="./result.png" alt="Result" />
    </Card>
  );
};

export default Result;
