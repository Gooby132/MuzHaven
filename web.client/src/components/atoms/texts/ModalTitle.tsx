import React from 'react'
import styled from "styled-components";

const Container = styled.div`
  font-size: 1.2em;
  margin-bottom: 1em;
`;

type Props = {
  text: string
}

export const ModalTitle = (props: Props) => {
  return (
    <Container>
      {props.text}
    </Container>
  )
}

