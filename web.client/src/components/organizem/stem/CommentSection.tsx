import { CommentRunway } from "components/molecules/stem/CommentRunway";
import { ReactComponent as Chevi } from "assets/icons/chevron-down.svg";
import styled from "styled-components";
import { CommentSettings } from "components/molecules/stem/CommentSettings";
import { CommentDto } from "services/stem/contracts";
import { Comment } from "components/molecules/stem/Comment";
import { useState } from "react";

const Container = styled.section<{ commentsShown: number }>`
  position: relative;
  width: 100%;
  background-color: ${({ theme }) => theme.light};
  border-radius: 15px;
  margin-top: 1.5em;

  > .body {
    padding: 1.5em 1.5em 0 1.5em;
    color: ${({ theme }) => theme.primary};
    height: ${({ commentsShown }) => `${commentsShown % 2 === 0  ? "2em" : "9em"}`};
    transition: height 0.8s cubic-bezier(0, 1, 0, 1);
    overflow-y: ${({ commentsShown }) => `${commentsShown % 2 === 0  ? "hidden" : "show"}`};
  }

  > .footer {
    display: flex;
    flex-direction: column;
    min-height: 2em;
    justify-content: end;
    align-items: center;

    > svg {
      width: 24px;
      height: 24px;
      color: ${({ theme }) => theme.primary};
      transform: ${({ commentsShown }) => `rotate(${(commentsShown % 4) * 180}deg)`};
      transition: transform ease-in-out 500ms;
    }
  }
`;

type Props = {
  onSubmit: () => void;
  onCommentChange: (text: string) => void;
  comments: CommentDto[];
};

export const CommentSection = (props: Props) => {
  const [showComment, setShowComment] = useState<number>(0);

  return (
    <Container commentsShown={showComment}>
      <div className="header">
        <CommentRunway
          onChange={(val) => props.onCommentChange(val)}
          onSubmit={props.onSubmit}
        />
        <CommentSettings />
      </div>
      <div className="body">
        {props.comments.map((comment, i) => (
          <Comment key={i} comment={comment} />
        ))}
      </div>
      <div onClick={() => setShowComment((prev) => prev + 1)} className="footer">
        <Chevi />
      </div>
    </Container>
  );
};
