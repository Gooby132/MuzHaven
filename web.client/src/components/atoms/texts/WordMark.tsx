import React from 'react'
import styled from 'styled-components'

type Props = {
  text: string;
}

const Container = styled.div`
  font-size: 2em;
  font-weight: bold;
`

export const WordMark = (props: Props) => {
  return (
    <Container>
      {props.text}
    </Container>
  )
}