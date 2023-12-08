import React from 'react'
import styled from 'styled-components'

type Props = {
  text: string;
}

const Container = styled.div`
  display: flex;
  justify-content: center;
  letter-spacing: 0.24em;
  align-items: center;
  min-height: 100px;
  font-size: 1.2em;
  font-weight: bold;
`

export const WordMark = ({text}: Props) => {
  return (
    <Container>
      {text}
    </Container>
  )
}