import { PropsWithChildren } from 'react'
import styled from 'styled-components'
import { BasicButton } from './BasicButton'

const Contianer = styled(BasicButton)`
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
