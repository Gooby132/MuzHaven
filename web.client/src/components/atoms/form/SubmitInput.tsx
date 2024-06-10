import React from 'react'
import styled from 'styled-components'

const Container = styled.input`
  background: ${({theme}) => theme.primary};
  color: ${({theme}) => theme.text};
  border: none;
  padding: 1em;
  cursor: pointer;
  
`

type Props = {
  text: string
}

export const SubmitInput = ({text}: Props) => {
  return (
    <Container type='submit' value={text} />
  )
}