import { PropsWithChildren } from 'react'
import styled from 'styled-components'

export const BasicButton = styled.button`
  background: ${({theme}) => theme.secondary};
  border: none;
  color: ${({theme}) => theme.text};
`