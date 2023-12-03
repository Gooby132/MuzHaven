import React from "react";
import styled from "styled-components";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  padding: 0.2em;

  >div{
    display: flex;
    flex-direction: row;
    > input {
      margin: 0 1em;
    }
    
    > label {
      white-space: nowrap;
    }
  }
  > p {
    color:red;
  }
`;

type Props = {
  name: string;
  text: string;
  onChange: (value: number) => void;
  error?: string;
};

export const NumberInput = ({ text, onChange, name, error }: Props) => {
  return (
    <Container>
      <div>
        <label htmlFor={name}>{text}</label>
        <input id={name} type="number" onChange={(e) => onChange(e.target.valueAsNumber)} />
      </div>
      {error && <p>{error}</p>}
    </Container>
  );
};
