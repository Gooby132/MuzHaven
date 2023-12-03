import { PropsWithChildren } from 'react'
import styled from 'styled-components'

const Container = styled.section`
  display: flex;
  width: 100%;
  flex-direction: row;
  justify-content: space-between;
`

type Props = {}

export const SprededRow = ({children}: PropsWithChildren<Props>) => {
  return (
    <Container>
      {children}
    </Container>
  )
}
