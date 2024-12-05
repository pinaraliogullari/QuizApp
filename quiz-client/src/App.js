import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";
import Login from "./components/Login";
import Register from "./components/Register";
import Quiz from "./components/Quiz";
import Result from "./components/Result";
import {AppProvider } from "./context/AppContext";
import PrivateRoute from "./guard/PrivateRoute";
import Layout from "./components/Layout";


function App() {
 
  return (
    <>
   <AppProvider>
      <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/" element={<Layout />} >
        <Route path="/quiz" element={<PrivateRoute><Quiz /></PrivateRoute>} />
        <Route path="/result" element={<PrivateRoute><Result /></PrivateRoute>} />
        </Route>
      </Routes>
      </BrowserRouter>
 </AppProvider>

    </>
  );
}

export default App;
