import { Accordion, AccordionDetails, AccordionSummary, Box, CardMedia, List, ListItem, Typography } from '@mui/material'
import ExpandCircleDownIcon from '@mui/icons-material/ExpandCircleDown';
import { red, green,grey } from '@mui/material/colors';
import React, { useContext } from 'react'
import { AppContext } from '../context/AppContext';

const Answer = ({qnAnswers}) => {
const[expanded,setExpanded]=React.useState(false)
const {baseUrl}=useContext(AppContext)
  console.log("qnAnswers Data:", qnAnswers);

const handleChange=(panel)=>(event,isExpanded)=>{
    setExpanded(isExpanded?panel:false)
}

    const markCorrectOrNot = (qna, idx) => {
        if ([qna.answer, qna.selected].includes(idx)) {
            return { sx: { color: qna.answer == idx ? green[500] : red[500] } }
        }
    } 

  return (
  
   <Box sx={{mt:5,width:'100%',maxWidth:640,mx:'auto'}}>
{
    qnAnswers.map((item,j)=>(<Accordion 
            disableGutters key={j} 
            expanded={expanded===j} 
            onChange={handleChange(j)}>

                   <AccordionSummary expandIcon={<ExpandCircleDownIcon
                        sx={{
                            color: item.answer == item.selected ? green[500] : red[500]
                        }}
                    />}>
                        <Typography
                            sx={{ width: '90%', flexShrink: 0 }}>
                           {item.inWords }
                         
                        </Typography>
                    </AccordionSummary>
            <AccordionDetails sx={{ backgroundColor: grey[900] }}>
                {item.imageName?
                <CardMedia 
                component={'img'} 
                sx={{m:'10px auto',width:'auto'}} 
                image={baseUrl + '/images/' + item.imageName} />
                :null}
                       <List>
                            {item.options.map((x, i) =>
                                <ListItem key={i}
                                >
                                    <Typography {...markCorrectOrNot(item, i)}>
                                        <b>
                                            {String.fromCharCode(65 + i) + ". "}
                                        </b>{x}
                                    </Typography>
                                </ListItem>
                            )}
                        </List>
            </AccordionDetails>
        </Accordion>
    ))
}
  
   </Box>
    )
}

export default Answer