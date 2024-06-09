import React from "react"
import styled from "styled-components"
import { ReactComponent as Delete } from "assets/icons/delete-bin-line.svg";

interface StyledProps {
}

const Container = styled(Delete)<StyledProps>`
  pointer-events: all;
 :hover{
  stroke: ${({theme}) => theme.red}
 }
`

export interface Props{
  onClick?: () => void;
}

export const DeleteIcon: React.FC<Props> = (props) => {
  return(
    <Container {...props} onClick={(e) => props.onClick && props.onClick()} />
  )
}