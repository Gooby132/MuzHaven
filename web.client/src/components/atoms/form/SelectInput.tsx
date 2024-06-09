import React from "react";
import styled from "styled-components";

const Container = styled.select`
  background: ${({ theme }) => theme.primary};
  color: ${({ theme }) => theme.text};
  padding: 0.5em 1em;
  min-width: 150px;
  border: none;
`;

type Props = {
  onChange: (key: number) => void;
  keyNames: [number, string][];
};

export const SelectInput = (props: Props) => {
  return (
    <Container onChange={(e) => props.onChange(parseInt(e.target.value))}>
      {props.keyNames.map((keyName) => (
        <option key={keyName[0]} value={keyName[0]}>{keyName[1]}</option>
      ))}
    </Container>
  );
};
