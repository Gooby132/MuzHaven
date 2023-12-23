import { CardTitle } from 'components/atoms/texts/CardTitle'
import { CompleteStemDto } from 'services/stem/contracts'
import { CommentSection } from './CommentSection'
import styled from 'styled-components'
import { useState } from 'react'
import { createComment } from 'services/stem/stemServiceClient'
import { useSelector } from 'react-redux'
import { RootState } from 'redux/store'

const Container = styled.div`

`

type Props = {
  stem: CompleteStemDto
}

export function Stem({stem}: Props) {
  const user = useSelector((state: RootState) => state.user)
  const [comment, setCommnet] = useState<string>("")
  
  const createCommentSubmit = async () => {
    const res = await createComment({
      stemId: stem.id,
      commenterId: user.id!,
      text: comment,
    })

    console.log(res)
  }

  return (
    <Container>
      <CardTitle text={stem.name} />
      <p>{stem.instrument}</p>
      <p>{stem.description}</p>
      <CommentSection comments={stem.comments} onCommentChange={val => setCommnet(val)} onSubmit={() => createCommentSubmit()} />
    </Container>
  )
}