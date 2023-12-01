import React, { ReactNode } from "react";
import styled from "styled-components";
import { Sidebar } from "../organizem/Sidebar";
import { CoreLink } from "../atoms/links/CoreLink";
import { LinkGroup } from "../molecules/LinkGroup";
import { useSelector } from "react-redux";
import { RootState } from "../../redux/store";
import { Footer } from "../organizem/Footer";
import { Outlet } from "react-router-dom";
import { Header } from "../organizem/Header";
import { WordMark } from "../atoms/texts/WordMark";
import { HeaderLayout } from "./HeaderLayout";

type Props = {};

const Container = styled.div`
  > header {
  }
  .main-container {
    display: flex;
    > nav {
      min-width: 30%;
    }
    > .content-container {
      display:flex;
      flex-direction: column;
      min-width: 70%;
    }
  }
`;

export const Main = (props: Props) => {
  const isLoggedIn = useSelector((state: RootState) => state.user.loggedIn);

  const loggedInRoutes = [<CoreLink key={0} text="profile" to="profile" />];

  const logInRoutes = [
    <CoreLink key={1} text="login" to="login" />,
    <CoreLink key={2} text="register" to="register" />,
  ];

  return (
    <Container>
      <Header layout={
        <HeaderLayout 
          logo={<WordMark text="MuzHaven" />}
          search={<div></div>}
          userIcon={<div></div>}
        />
      } />
      <section className="main-container">
        <Sidebar
          links={[
            <CoreLink key={3} text="home" to="/" />,
            <LinkGroup
              key={0}
              header="user"
              links={isLoggedIn ? loggedInRoutes : logInRoutes}
            />,
          ]}
        />
        <div className="content-container">
          <Outlet />
          <Footer />
        </div>
      </section>
    </Container>
  );
};
