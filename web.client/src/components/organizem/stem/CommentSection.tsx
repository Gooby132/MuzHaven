import { CommentRunway } from "components/molecules/stem/CommentRunway";
import { ReactComponent as Chevi } from "assets/icons/chevron-down.svg";
import styled from "styled-components";
import { CommentSettings } from "components/molecules/stem/CommentSettings";
import { CommentDto } from "services/stem/contracts";
import { Comment } from "components/molecules/stem/Comment";
import { useState } from "react";

const Container = styled.section<{ commentsShown: boolean }>`
  position: relative;
  width: 100%;
  background-color: ${({ theme }) => theme.light};
  border-radius: 15px;
  margin-top: 1.5em;

  > .body {
    padding: 1.5em 1.5em 0 1.5em;
    color: ${({ theme }) => theme.primary};
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
      transform: ${({ commentsShown }) =>
        commentsShown ? "rotate(180)" : "rotate(360)"};
    }
  }
`;

type Props = {
  onSubmit: () => void;
  onCommentChange: (text: string) => void;
  comments: CommentDto[];
};

export const CommentSection = (props: Props) => {
  const [showComment, setShowComment] = useState<boolean>(false);

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
      <div className="footer">
        <Chevi />
      </div>
    </Container>
  );
};
