import React from 'react'
import styled from 'styled-components'

const Containter = styled.div`
  width: 100%;
  text-align: center;
`

type Props = {
  text: string,
}

export const GroupTitle = ({text}: Props) => {
  return (
    <Containter>
      {text}
    </Containter>
  )
}
