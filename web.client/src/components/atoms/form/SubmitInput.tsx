import React from 'react'
import styled from 'styled-components'

const Container = styled.div`

`

type Props = {
  text: string

}

export const SubmitInput = ({text}: Props) => {
  return (
    <Container>
      <input type='submit' value={text} />
    </Container>
  )
}