import React from 'react'
import styled from 'styled-components'
import { PageTitle } from '../atoms/texts/PageTitle'
import { useSelector } from 'react-redux'
import { RootState } from '../../redux/store'
import { DisplayLabel } from '../atoms/texts/DisplayLabel'

const Contaier = styled.div`

`

type Props = {}

export const Profile = (props: Props) => {
  const user = useSelector((state: RootState) => state.user)

  return (
    <Contaier>
      <PageTitle text={user.stageName} />
      <DisplayLabel header="First name" text={user.firstName} />
      <DisplayLabel header="Last name" text={user.lastName} />
    </Contaier>
  )
}