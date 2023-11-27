import React from 'react'
import styled from 'styled-components'
import { PageTitle } from '../atoms/texts/PageTitle'

const Contaier = styled.div`

`

type Props = {}

export const Profile = (props: Props) => {
  return (
    <Contaier>
      <PageTitle text='Profile' />
    </Contaier>
  )
}