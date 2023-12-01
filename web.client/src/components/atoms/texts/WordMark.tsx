import React from 'react'
import styled from 'styled-components'

type Props = {
  text: string;
}

const Container = styled.div`
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