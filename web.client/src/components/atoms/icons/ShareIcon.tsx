import React from "react";
import styled from "styled-components";
import { ReactComponent as Share } from "assets/icons/share-line.svg";

interface StyledProps {}

const Container = styled(Share)<StyledProps>`
  pointer-events: all;
  :hover {
    stroke: ${({ theme }) => theme.lightAccent};
  }
`;

export interface Props {  
  onClick?: () => void;
}

export const ShareIcon: React.FC<Props> = (props) => (
  <Container {...props} onClick={(e) => props.onClick && props.onClick()} />
);
