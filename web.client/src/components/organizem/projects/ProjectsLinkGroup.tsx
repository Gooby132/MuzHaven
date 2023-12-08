import { Searchbar } from 'components/atoms/form/Searchbar'
import { GroupTitle } from 'components/atoms/texts/GroupTitle'
import { ProjectLink } from 'components/molecules/projects/ProjectLink'
import React, { ReactElement } from 'react'

type Props = {
  projects?: ReactElement<typeof ProjectLink>[]
}

const ProjectsLinkGroup = ({ projects }: Props) => {
  return (
    <div>
      <GroupTitle text='Projects' />
      <Searchbar onChange={e => {}} />
      {projects}
    </div>
  )
}

export default ProjectsLinkGroup