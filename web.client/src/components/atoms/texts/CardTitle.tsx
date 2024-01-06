import React from "react"
import styled from "styled-components"

interface StyledProps {
}

const Container = styled.div<StyledProps>`
  
`

export interface Props{
  text: string
}

export const CardTitle: React.FC<Props> = (props) => {
  return(
    <Container {...props} >
      {props.text}
    </Container>
  )
}