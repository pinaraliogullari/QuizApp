import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";
import Login from "./components/Login";
import Register from "./components/Register";
import Quiz from "./components/Quiz";
import Result from "./components/Result";
import { AppContext } from "./context/AppContext";
import PrivateRoute from "./guard/PrivateRoute";
import Layout from "./components/Layout";


function App() {
 const baseUrl = 'https://localhost:7291/api';
  return (
    <>
   <AppContext.Provider value={{ baseUrl }}>
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
    </AppContext.Provider>

    </>
  );
}

export default App;
