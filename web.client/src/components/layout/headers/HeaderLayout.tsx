import { ReactNode } from "react";
import styled, { useTheme } from "styled-components";

const Container = styled.div`
  display: grid;
  row-gap: 0.5em;
  column-gap: 0.5em;
  grid-template-columns: auto 40px;
  min-height: 100px;

  > * {
    overflow: hidden;
  }
`;

type Props = {
  search: ReactNode;
  userIcon: ReactNode;
};

export function HeaderLayout({ search, userIcon }: Props) {

  return (
    <Container>
      <>{search}</>
      <>{userIcon}</>
    </Container>
  );
}
