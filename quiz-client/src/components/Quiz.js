import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router';
import { useContext } from 'react';
import { AppContext } from '../context/AppContext';
import HttpClientService from '../services/HttpClientService'; 
import {RequestParameters} from '../models/RequestParameters'; 
import { Card, CardContent, CardMedia, CardHeader, List, ListItemButton, Typography, Box, LinearProgress } from '@mui/material';
import { getFormatedTime } from '../helper';

export default function Quiz() {
    const [qns, setQns] = useState([]); 
    const [qnIndex, setQnIndex] = useState(0); 
    const [timeTaken, setTimeTaken] = useState(0); 
    const [selectedOptions, setSelectedOptions] = useState([]); 
    const { baseUrl } =useContext(AppContext);
    const navigate = useNavigate();

    let timer;

    const startTimer = () => {
        timer = setInterval(() => {
            setTimeTaken(prev => prev + 1);
        }, 1000);
    };
    useEffect(() => {
        const token = localStorage.getItem('token'); 
        const requestParameters = new RequestParameters(
            'questions',          // controller 
            'getQuestions',       // action 
            '',                   // queryString
            { 'Authorization': `Bearer ${token}` }, // headers 
            baseUrl,              // baseUrl
            'quiz/getQuestions'   // fullEndPoint
        );

        HttpClientService.get(requestParameters)
            .then((res) => {
                setQns(res.data); 
                startTimer(); 
            })
            .catch((err) => console.error('Error fetching questions', err));

        return () => clearInterval(timer);
    }, []);

    const updateAnswer = (qnId, optionIdx) => {
        const newSelectedOption = {
            qnId,
            selected: optionIdx,
        };

        setSelectedOptions(prevOptions => [...prevOptions, newSelectedOption]);

        if (qnIndex < qns.length - 1) {
            setQnIndex(qnIndex + 1); 
        } else {
            const payload = {
                selectedOptions: selectedOptions, 
                timeTaken: timeTaken 
            };
           
        }
    };

    return (
        qns.length !== 0 ? (
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
                        image={`https://localhost:7291/api/images/${qns[qnIndex].imageName}`}
                        sx={{ width: 'auto', m: '10px auto' }}
                    />
                )}
                <CardContent>
                    <Typography variant="h6">{qns[qnIndex].InWords}</Typography>
                    <List>
                        {qns[qnIndex].options.map((item, idx) => (
                            <ListItemButton
                                disableRipple
                                key={idx}
                                onClick={() => updateAnswer(qns[qnIndex].qnId, idx)}
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
}
