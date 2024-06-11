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
    }

    > label {
      white-space: nowrap;
      min-width: 5em;
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
  onChange: (value?: FileList) => void;
  error?: string;
};

export const FileInput = ({
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
          type="file"
          accept="audio/*"
          value={initialValue}
          onChange={(e) =>
            onChange(e.target.files === null ? undefined : e.target.files)
          }
        />
      </div>
      {error && <p>{error}</p>}
    </Container>
  );
};
