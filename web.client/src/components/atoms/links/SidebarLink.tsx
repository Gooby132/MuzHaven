import React, { PropsWithChildren } from "react";
import {  NavLink, useMatches } from "react-router-dom";
import styled from "styled-components";

const Container = styled(NavLink)<{ isActive: boolean }>`
  background-color: ${({ theme, isActive }) =>
    !isActive ? theme.primary : theme.lightAccent};
  width: 80%;
  min-height: 5.5em;
  text-decoration:none;
  
  &.active {
    background-color: ${({ theme }) => theme.lightAccent};
  }
`;

type Props = {
  projectId: string;
  to: string;
  isActive: boolean;
};

export const SidebarLink = ({
  projectId,
  to,
  isActive,
  children,
}: PropsWithChildren<Props>) => {
  const matches = useMatches()
console.log(matches)
  return (
    <Container to={to} isActive={isActive} key={projectId}>
      {children}
    </Container>
  );
};
