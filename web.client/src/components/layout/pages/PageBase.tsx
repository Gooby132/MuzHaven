import { PropsWithChildren } from "react"
import styled from "styled-components"

const Container = styled.div`
  display: flex;
  justify-content: center;
  > div {
    min-width:90%;
  }
`

type Props = {}

export const PageBase = (props: PropsWithChildren<Props>) => {
  return (
    <Container>
      {props.children}
    </Container>
  )
}