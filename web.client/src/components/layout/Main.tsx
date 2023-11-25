import React, { ReactNode } from "react";
import styled from "styled-components";
import { Home } from "../pages/Home";

type Props = {
  sidebar: ReactNode;
  main: ReactNode;
  footer: ReactNode;
};

const Container = styled.div`
  display: flex;

  .sidebar-container {
    min-width: 30%;
  }

  .main-container {
    min-width: 70%;
    
    .content-container{
      width: 100%;
    }
    .footer-container{
      width: 100%;
      display: flex;
      justify-content: center;
    }
  }
`;

export const Main = (props: Props) => {
  return (
    <Container>
        <nav className="sidebar-container">{props.sidebar}</nav>
        <section className="main-container">
          <div className="content-container">{props.main}</div>
          <div className="footer-container">{props.footer}</div>
        </section>
    </Container>
  );
};
