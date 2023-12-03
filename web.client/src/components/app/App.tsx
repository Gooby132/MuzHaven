import React from "react";
import styled from "styled-components";
import { Routes, Route, Outlet, Navigate } from "react-router-dom";
import { Main } from "../layout/Main";
import { Login } from "../pages/Login";
import { Register } from "../pages/Register";
import { Error } from "../pages/Error";
import { Profile } from "../pages/Profile";
import { Home } from "../pages/Home";
import { useSelector } from "react-redux";
import { RootState } from "../../redux/store";
import Projects from "../pages/Projects";
// @ts-ignore
import Modal from 'react-modal'

const Container = styled.div``;

function App() {

  const isLoggedIn = useSelector((state: RootState) => state.user.token);

  return (
    <Container>
      <Routes>
        <Route path="/" element={<Main />}>
          <Route index Component={Home} />
          <Route path="/login" errorElement={<Error />} element={ isLoggedIn ? <Navigate to="/profile" replace /> : <Login />} />
          <Route path="/register" errorElement={<Error />} element={ isLoggedIn ? <Navigate to="/profile" replace /> : <Register />} />
          <Route path="/profile" errorElement={<Error />} element={ !isLoggedIn ? <Navigate to="/login" replace /> : <Profile />} />
          <Route path="/projects" errorElement={<Error />} element={ !isLoggedIn ? <Navigate to="/login" replace /> : <Projects/>} />
          <Route path="/"  errorElement={<Error />} />
        </Route>
      </Routes>
    </Container>
  );
}

Modal.setAppElement("#root")

export default App;
