import { CommentRunway } from "components/molecules/stem/CommentRunway";
import { ReactComponent as Chevi } from "assets/icons/chevron-down.svg";
import styled from "styled-components";
import { CommentSettings } from "components/molecules/stem/CommentSettings";

const Container = styled.section`
  position: relative;
  width: 100%;
  background-color: ${({ theme }) => theme.light};
  border-radius: 15px;
  margin-top: 1.5em;
  
  > .footer {
    display: flex;
    flex-direction: column;
    min-height: 2em;
    justify-content: end;
    align-items: center;

    >svg{
      width: 24px;
      height:24px;
      color: ${({ theme }) => theme.primary};
    }
  }
`;

type Props = {};

export const CommentSection = (props: Props) => {
  return (
    <Container>
      <CommentRunway />
      <CommentSettings />
      <div className="footer">
        <Chevi />
      </div>
    </Container>
  );
};
