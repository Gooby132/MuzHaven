import { CardTitle } from 'components/atoms/texts/CardTitle'
import { CompleteStemDto } from 'services/stem/contracts'
import { CommentSection } from './CommentSection'
import styled from 'styled-components'

const Container = styled.div`

`

type Props = {
  stem: CompleteStemDto
}

export function Stem({stem}: Props) {
  return (
    <Container>
      <CardTitle text={stem.name} />
      <p>{stem.instrument}</p>
      <p>{stem.description}</p>
      <CommentSection />
    </Container>
  )
}