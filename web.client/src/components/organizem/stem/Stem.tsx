import { CardTitle } from 'components/atoms/texts/CardTitle'
import { CompleteStemDto } from 'services/stem/contracts'
import { CommentSection } from './CommentSection'
import styled from 'styled-components'
import { useState } from 'react'
import { GET_STREAM, STEM_SERVICE_BASE, createComment } from 'services/stem/stemServiceClient'
import { useSelector } from 'react-redux'
import { RootState } from 'redux/store'
import { ReactComponent as PlayButton } from 'assets/icons/play-circle.svg'
import { ReactComponent as PauseButton } from 'assets/icons/pause-circle.svg'
import { useStemPlayer } from 'hooks/usePlayAudio'

const Container = styled.div`
  .header{
    display: flex;
    gap:.5em;
    max-height: 1em;
    >svg{
      cursor: pointer;
      width: 24px;
      height: 24px;
    }
  }
`

type Props = {
  stem: CompleteStemDto
}

export function Stem({stem}: Props) {
  const user = useSelector((state: RootState) => state.user)

  const audioPlayer = useStemPlayer({
    id: stem.id,
    format: stem.musicFile!.format,
    url: `${STEM_SERVICE_BASE}${GET_STREAM}?stemId=${stem.id}`
  });
  
  const [comment, setCommnet] = useState<string>("")
  const [, forceRender] = useState(false);

  const createCommentSubmit = async () => {
    const res = await createComment({
      stemId: stem.id,
      commenterId: user.id!,
      stageName: user.stageName!,
      text: comment,
    })

    if(!res.isError)
      forceRender((prev) => !prev)
  }

  return (
    <Container>
      <div className='header'>
        <h4>{stem.name}</h4>
        {
        !audioPlayer.playing ? 
          <PlayButton onClick={() => audioPlayer.play()} /> :
          <PauseButton onClick={() => audioPlayer.pause()} />  
      }
      </div>
      <p>{stem.instrument}</p>
      <p>{stem.description}</p>
      <CommentSection comments={stem.comments} onCommentChange={val => setCommnet(val)} onSubmit={() => createCommentSubmit()} />
    </Container>
  )
}