import { ReactNode } from "react";
import styled from "styled-components";

interface Props {
  logo: ReactNode
}

const Container = styled.header`
  display: flex;
  justify-content: space-between;
  min-height: 10vh;
`;

export const Header = (props: Props) => {

  return (
    <Container>
      {props.logo}
    </Container>
  );
};
