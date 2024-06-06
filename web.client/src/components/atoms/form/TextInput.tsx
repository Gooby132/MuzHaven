import React from "react";
import styled from "styled-components";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  padding: 0.2em;

  > div {
    display: flex;
    flex-direction: row;
    > input {
      margin: 0 1em;
      background: ${({theme}) => theme.primary};
      border: none;
      color: ${({theme}) => theme.text};
      padding: .3em;
    }

    > label {
      white-space: nowrap;
      min-width:5em;
    }
  }
  > p {
    color: red;
  }
`;

type Props = {
  name: string;
  text: string;
  initialValue?: string;
  onChange: (value: string) => void;
  error?: string;
};

export const TextInput = ({
  text,
  onChange,
  name,
  error,
  initialValue,
}: Props) => {
  return (
    <Container>
      <div>
        <label htmlFor={name}>{text}</label>
        <input
          id={name}
          type="text"
          value={initialValue}
          onChange={(e) => onChange(e.target.value)}
        />
      </div>
      {error && <p>{error}</p>}
    </Container>
  );
};
