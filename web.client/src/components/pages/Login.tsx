import React from 'react'
import styled from 'styled-components'
import { PageTitle } from '../atoms/texts/PageTitle'

const Container = styled.div`

`

type Props = {}

export const Login = (props: Props) => {
  return (
    <Container>
      <PageTitle text='Login' />
    </Container>
  )
}