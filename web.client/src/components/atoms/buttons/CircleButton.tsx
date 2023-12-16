import { PropsWithChildren } from "react";
import styled from "styled-components";

const Container = styled.div<StyleProps>`
  border-radius: 50%;
  background: ${({ theme }) => theme.light};
  width: ${({ radius }) => radius};
  height: ${({ radius }) => radius};

  > button {
    display: block;
    width: 100%;
    height: 100%;
    background-color: transparent;
    border: none;

    > *{
      margin: auto;
      display: block;
    }
  }
`;

type StyleProps = {
  radius: string;
};

interface Props extends StyleProps {
  onClick: () => void;
}

export const CircleButton = ({
  onClick,
  children,
  radius,
}: PropsWithChildren<Props>) => {
  return (
    <Container radius={radius}>
      <button onClick={onClick}>{children}</button>
    </Container>
  );
};
