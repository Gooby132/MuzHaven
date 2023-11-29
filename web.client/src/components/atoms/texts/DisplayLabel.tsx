import React from 'react'
import styled from 'styled-components'

const Container = styled.div`
  display: flex;
  >label{
    margin: 0 1em;
  }
`

type Props = {
  text?: string,
  header: string
}

export const DisplayLabel = ({text, header}: Props) => {
  return (
    <Container>
      <label>{header}:</label>
      <p>{text}</p>
      </Container>
  )
}