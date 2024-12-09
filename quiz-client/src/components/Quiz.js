import React, { useEffect, useState, useContext } from 'react';
import { AppContext } from '../context/AppContext';
import HttpClientService from '../services/HttpClientService'; 
import { RequestParameters } from '../models/RequestParameters'; 
import { Card, CardContent, CardMedia, CardHeader, List, ListItemButton, Typography, Box, LinearProgress } from '@mui/material';
import { getFormatedTime } from '../helper';
import { useNavigate } from 'react-router-dom';

const Quiz = () => {
    const [qns, setQns] = useState([]);  
    const [qnIndex, setQnIndex] = useState(0);  
    const { baseUrl, selectedOptions, timeTaken, updateSelectedOptions, updateTimeTaken } = useContext(AppContext);  
    const navigate = useNavigate();  

    let timer;
    const startTimer = () => {
        timer = setInterval(() => {
            updateTimeTaken(prev => prev + 1);
        }, 1000);
    };

    useEffect(() => {
            startTimer(); 
        const requestParameters = new RequestParameters(
            'questions',  
            '',  
            '',  
            {},  
            baseUrl,  
            ''  
        );

        HttpClientService.get(requestParameters)
            .then((res) => {
                setQns(res);  
            })
            .catch((err) => {
                console.error('Error fetching questions', err);
            });

        return () => clearInterval(timer);
    }, [baseUrl]);

  const updateAnswer = (qnId, optionIdx) => {
    const newSelectedOption = {
        qnId,
        selected: optionIdx,
    };

    updateSelectedOptions(newSelectedOption);

    if (qnIndex < qns.length - 1) {
        setQnIndex(prevIndex => prevIndex + 1);
    } else {
        updateSelectedOptions(selectedOptions);  
        navigate('/result');  
    }
};

    return (
        qns && qns.length > 0 ? (
            <Card sx={{ maxWidth: 640, mx: 'auto', mt: 5, '& .MuiCardHeader-action': { m: 0, alignSelf: 'center' } }}>
                <CardHeader
                    title={`Question ${qnIndex + 1} of ${qns.length}`}
                    action={<Typography>{getFormatedTime(timeTaken)}</Typography>} 
                />
                <Box>
                    <LinearProgress variant="determinate" value={(qnIndex + 1) * 100 / qns.length} />
                </Box>
                {qns[qnIndex].imageName && (
                    <CardMedia
                        component="img"
                        image={baseUrl + '/images/' + qns[qnIndex].imageName}
                        sx={{ width: 'auto', m: '10px auto' }}
                    />
                )}
                <CardContent>
                    <Typography variant="h6">{qns[qnIndex].inWords}</Typography>
                    <List>
                        {qns[qnIndex].options.map((item, idx) => (
                            <ListItemButton
                                disableRipple
                                key={idx}
                                onClick={() => updateAnswer(qns[qnIndex].id, idx)}
                                sx={{
                                    backgroundColor: selectedOptions.find(option => option.qnId === qns[qnIndex].id)?.selected === idx 
                                        ? 'lightblue' 
                                        : 'transparent',
                                    '&:hover': { backgroundColor: 'lightgray' }
                                }}
                            >
                                <div>
                                    <b>{String.fromCharCode(65 + idx)} . </b>{item}
                                </div>
                            </ListItemButton>
                        ))}
                    </List>
                </CardContent>
            </Card>
        ) : (
            <Typography variant="h6" align="center">Loading Questions...</Typography>
        )
    );
};

export default Quiz;
