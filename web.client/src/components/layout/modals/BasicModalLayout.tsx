import React, { PropsWithChildren, ReactNode } from "react";
import styled from "styled-components";

const Container = styled.div`
  width: 100%;
  height: 100%;
  display: grid;
  grid-template-rows: 1fr 8fr 1fr;
`;

const Header = styled.div``;

const Main = styled.div``;

const Footer = styled.div`
  display: flex;
  justify-content: center;
`;

type Props = {
  headerChildren: ReactNode[];
  footerChildren: ReactNode[];
};

export const BasicModalLayout = ({
  headerChildren,
  children,
  footerChildren,
}: PropsWithChildren<Props>) => {
  return (
    <Container>
      <Header>{headerChildren}</Header>
      <Main>{children}</Main>
      <Footer>{footerChildren}</Footer>
    </Container>
  );
};
