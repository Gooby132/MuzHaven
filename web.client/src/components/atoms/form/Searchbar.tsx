import React from "react";
import styled from "styled-components";

const Container = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;

  > input {
    width: 80%;
    color: ${({ theme }) => theme.text};
    background-color: ${({ theme }) => theme.secondary};
    border: none;
    border-radius: 15px;
    height: 2em;
    padding: 0 .5em;
  }
`;

type Props = {
  onChange: (text: string) => void;
};

export function Searchbar({ onChange }: Props) {
  return (
    <Container>
      <input type="text" onChange={(e) => onChange(e.target.value)} />
    </Container>
  );
}
