import React, { ReactNode } from "react";
import styled from "styled-components";

const Container = styled.div`
  display: grid;
  row-gap: 0.5em;
  column-gap: 0.5em;
  grid-template-columns: 7em 200px auto 40px;
  
  >* {
    overflow: hidden;
  }

`;

type Props = {
  logo: ReactNode;
  search: ReactNode;
  userIcon: ReactNode;
};

export const HeaderLayout = ({ logo, search, userIcon }: Props) => {
  return (
    <Container>
      <>{logo}</>
      <>{search}</>
      <div></div> {/* buffer */}
      <>{userIcon}</>
    </Container>
  );
};
