import { PropsWithChildren } from 'react'
import styled from 'styled-components'

const Contianer = styled.button`
  padding: 0.5em;
`

type Props = {
  onClick: () => void
}

export const PageSizeButton = ({children, onClick}: PropsWithChildren<Props>) => {
  return (
    <Contianer onClick={onClick}>
      {children}
    </Contianer>
  )
}
