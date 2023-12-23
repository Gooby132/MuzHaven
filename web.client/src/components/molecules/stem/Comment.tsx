import React from "react";
import { DateTime } from "luxon";
import { CommentDto } from "services/stem/contracts";
import styled from "styled-components";

const Container = styled.div`
  display: flex;

  > .date {
    font-size: 0.7em;
    color: ${({ theme }) => theme.secondary};
  }
  
  > .text {
    margin-left: 1em;
    color: ${({ theme }) => theme.primary};
  }
`;

type Props = {
  comment: CommentDto;
};

export const Comment = ({ comment }: Props) => {
  const createdOn = DateTime.fromISO(comment.createdOnUtc);

  return (
    <Container>
      <div className="date">
        {createdOn.month}/{createdOn.day}
      </div>
      <div className="text">{comment.text}</div>
    </Container>
  );
};
