import React, { useState } from "react";
import styled, { ThemeProvider } from "styled-components";
import { Routes, Route, Outlet, Navigate } from "react-router-dom";
import { Login } from "../pages/Login";
import { Register } from "../pages/Register";
import { Error } from "../pages/Error";
import { Profile } from "../pages/Profile";
import { Home } from "../pages/Home";
import { useSelector } from "react-redux";
import { RootState } from "../../redux/store";
import Projects from "../pages/Projects";
// @ts-ignore
import Modal from "react-modal";
import { MainVer2 } from "components/layout/app/MainVer2";
import { MuzHavenTheme } from "themes/theme";
import { darkTheme } from "themes/darkTheme";

const Container = styled.div``;

function App() {
  const isLoggedIn = useSelector((state: RootState) => state.user.token);
  const [theme, setTheme] = useState<MuzHavenTheme>(darkTheme);

  return (
    <Container>
      <ThemeProvider theme={theme}>
        <Routes>
          <Route path="/" element={<MainVer2 />}>
            <Route index Component={Home} />
            <Route
              path="/login"
              errorElement={<Error />}
              element={
                isLoggedIn ? <Navigate to="/profile" replace /> : <Login />
              }
            />
            <Route
              path="/register"
              errorElement={<Error />}
              element={
                isLoggedIn ? <Navigate to="/profile" replace /> : <Register />
              }
            />
            <Route
              path="/profile"
              errorElement={<Error />}
              element={
                !isLoggedIn ? <Navigate to="/login" replace /> : <Profile />
              }
            />
            <Route
              path="/projects"
              errorElement={<Error />}
              element={
                !isLoggedIn ? <Navigate to="/login" replace /> : <Projects />
              }
            />
            <Route path="/" errorElement={<Error />} />
          </Route>
        </Routes>
      </ThemeProvider>
    </Container>
  );
}

Modal.setAppElement("#root");

export default App;
