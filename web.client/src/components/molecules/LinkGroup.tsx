import React, { ReactNode } from 'react'
import styled from 'styled-components'
import { CoreLink } from '../atoms/links/CoreLink';

type Props = {
  header: string;
  links: React.ReactElement<typeof CoreLink>[];
}

const Container = styled.div`
  display: flex;
  flex-direction: column;
`

export const LinkGroup = ({header, links}: Props) => {
  return (
    <Container>
      {header}
      {links}
    </Container>
  )
}