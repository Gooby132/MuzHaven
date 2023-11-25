import React from 'react'
import { LinkGroup } from '../molecules/LinkGroup'
import styled from 'styled-components'

type Props = {
  links: React.ReactElement<typeof LinkGroup>[]
}

const Container = styled.div`

`

export const Sidebar = ({links}: Props) => {
  return (
    <Container>
      {links}
    </Container>
  )
}