import React from "react";
import styled from "styled-components";
import { Routes, Route } from "react-router-dom";
import { Header } from "../organizem/Header";
import { Footer } from "../organizem/Footer";
import { Main } from "../layout/Main";
import { Sidebar } from "../organizem/Sidebar";
import { WordMark } from "../atoms/texts/Logo";
import { LinkGroup } from "../molecules/LinkGroup";
import { CoreLink } from "../atoms/links/CoreLink";
import { Home } from "../pages/Home";
import { Login } from "../pages/Login";
import { Register } from "../pages/Register";
import { Error } from "../pages/Error";
import { NotFound } from "../pages/NotFound";
import { Profile } from "../pages/Profile";

const Container = styled.div``;

function App() {
  return (
    <Container>
      <Header logo={<WordMark text="MuzHaven" />} />
      <Main
        sidebar={
          <Sidebar
            links={[
              <CoreLink key={3} text="home" to="/" />,
              <LinkGroup
                key={0}
                header="user"
                links={[
                  <CoreLink key={0} text="profile" to="profile" />,
                  <CoreLink key={1} text="login" to="login" />,
                  <CoreLink key={2} text="register" to="register" />,
                ]}
              />,
            ]}
          />
        }
        main={
          <Routes>
            <Route path="/" Component={Home} />
            <Route path="/login" Component={Login} />
            <Route path="/register" Component={Register} />
            <Route path="/profile" Component={Profile} />
            <Route path="/" 
            Component={Register} 
            errorElement={<Error />} />
          </Routes>
        }
        footer={<Footer />}
      />
    </Container>
  );
}

export default App;
