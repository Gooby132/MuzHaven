import { Searchbar } from 'components/atoms/form/Searchbar'
import { SidebarLink } from 'components/atoms/links/SidebarLink'
import { ReactElement } from 'react'
import styled from 'styled-components'

const Container = styled.div`
  color: ${({theme}) => theme.text};
  display: flex;
  align-items: center;
  flex-direction: column;
  gap: 1rem;
`

type Props = {
  projects?: ReactElement<typeof SidebarLink>[]
}

export const ProjectsLinkGroup = ({ projects }: Props) => {
  return (
    <Container>
      <h4>Projects</h4>
      <Searchbar onChange={e => {}} />
      {projects}
    </Container>
  )
}
