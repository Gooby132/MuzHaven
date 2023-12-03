import { PropsWithChildren, ReactNode } from 'react'
import styled from 'styled-components'

const Container = styled.button`

`

export type Props = { 
  onClick: () => void
}

export const BasicButton = ({children, onClick}: PropsWithChildren<Props>) => {
  return (
    <Container onClick={onClick} >{children}</Container>
  )
}
