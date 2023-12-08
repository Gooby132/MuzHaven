import React from 'react'

type Props = {
  projectId: string,
  songName: string,
  albumName?: string
  isActive: boolean,
}

export const ProjectLink = ({projectId, songName, isActive}: Props) => {
  return (
    <div>
      ProjectLink
    </div>
  )
}