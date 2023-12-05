import React from 'react'
import styled from 'styled-components'
const Container = styled.div`
  display: flex;
  justify-content: center;

  height: 1px;
  
  >div{
    width:80%;
    background-color: gray;
  }
`

type Props = {}

export const Seperator = (props: Props) => {
  return (
    <Container>
      <div></div>
    </Container>
  )
}

export default Seperator