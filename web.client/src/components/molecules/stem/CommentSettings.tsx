import React from "react";
import styled from "styled-components";
import { ReactComponent as Comments } from "assets/icons/comments-2-line.svg";
import { ReactComponent as Clog } from "assets/icons/settings-cog.svg";
import { ReactComponent as Share } from "assets/icons/share-line.svg";

const Container = styled.div`
  position: absolute;
  background: ${({ theme }) => theme.light};
  width: calc(50% - 0.5em);
  min-height: 1.5em;
  border-radius: 15px 15px 0px 0px;
  top: -0.75em;
  right: 0;
  > .group {
    color: ${({ theme }) => theme.secondary};
    position: absolute;
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    gap: .2rem;
    top: .25rem;
    right: 0.5em;
    width: 2.5em;
  }
`;

type Props = {};

export const CommentSettings = (props: Props) => {
  return (
    <Container>
      <div className="group">
        <Comments />
        <Share />
        <Clog />
      </div>
    </Container>
  );
};
