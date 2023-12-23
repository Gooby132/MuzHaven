import styled from "styled-components";
import {ReactComponent as Send} from 'assets/icons/send-line.svg'

const Container = styled.div`
  position: absolute;
  background: ${({ theme }) => theme.secondary};
  width: calc(50% - .5em);
  min-height: 1.5em;
  border-radius: 15px;
  top: -.75em;
  left: .5em;
  >input{
    position: absolute;
    top: .75em;
    left: .5em;
    font-size: .5em;
    width: 80%;
    color: ${({ theme }) => theme.text};
    background: ${({ theme }) => theme.secondary};
    border: none;
  }
  > svg {
    position: absolute;
    top: .2rem;
    right: .5rem;
    width: 1rem;
    height: 1rem;
  }
`;

type Props = {
  onChange: (val: string) => void;
  onSubmit: () => void;
};

export const CommentRunway = (props: Props) => {
  return (
  <Container>
    <input type="text" onChange={e => props.onChange(e.target.value)} />
    <Send onClick={props.onSubmit} />
  </Container>
  );
};
